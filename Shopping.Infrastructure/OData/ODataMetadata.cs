using System.Collections.Generic;

#pragma warning disable 1591

namespace Shopping.Infrastructure.OData
{
    public class ODataMetadata<T> where T : class
    {
        private readonly long? _count;
        private IEnumerable<T> _result;

        public ODataMetadata(IEnumerable<T> result, long? count)
        {
            _count = count;
            _result = result;
        }
        public IEnumerable<T> Results => _result;

        public long? Count => _count;
    }
}