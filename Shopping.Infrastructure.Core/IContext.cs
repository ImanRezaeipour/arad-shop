using System;
using System.Threading.Tasks;

namespace Shopping.Infrastructure.Core
{
    public interface IContext : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}