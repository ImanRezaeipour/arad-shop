using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PersianDate;

namespace Shopping.Infrastructure.Asp.Convertor
{
    public class JsonDateTimeConverter : DateTimeConverterBase
    {
       public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var value = reader.Value?.ToString();
                if (objectType == typeof(DateTime?) && (value == null || value.ToLower() == "null"))
                    return null;
                return string.IsNullOrEmpty(value) ? DateTime.MinValue : ConvertGregorianDateTime(value);
            }
            catch (Exception)
            {
                return reader.Value;
            }
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                if (value == null || String.IsNullOrEmpty(value.ToString()))
                    writer.WriteValue(String.Empty);
                writer.WriteValue(((DateTime)value).ToFa("G"));
            }
            catch (Exception e)
            {
                // ignored
            }
        }
        public  DateTime ConvertToPersianDate( string src)
        {
            try
            {
                var date = Convert.ToDateTime(src);
                DateTime dt = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second,
                    new PersianCalendar());
                return dt;
            }
            catch (Exception ex)
            {
                return DateTime.MinValue;
            }
        }
        private DateTime ConvertGregorianDateTime(string date)
        {
            try
            {
                var split = date.Split('/');
                PersianCalendar pc = new PersianCalendar();
                DateTime dt = new DateTime(Int32.Parse(split[0]), Int32.Parse(split[1]), Int32.Parse(split[2]), pc);
                return dt;
            }
            catch (Exception e)
            {
                return DateTime.MinValue;
            }
        }
    }
}
