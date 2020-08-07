using Shopping.Infrastructure.Core;
using Shopping.Repository.Write.Context;
using Shopping.Repository.Write.Interface;

namespace Shopping.Repository.Write
{
    public class SeqRepository : ISeqRepository
    {
        private readonly DataContext _context;
        public SeqRepository(IContext context)
        {
            _context = (DataContext)context;
        }
        public long GetNextSequenceValue(string seqName)
        {
            var rawQuery = _context.Database.SqlQuery<long>($"SELECT NEXT VALUE FOR dbo.{seqName};");
            var task = rawQuery.SingleAsync();
            long nextVal = task.Result;
            return nextVal;
        }
    }
}