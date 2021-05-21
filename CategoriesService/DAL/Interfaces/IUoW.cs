using System;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;

namespace DAL.Interfaces
{
    public interface IUoW : IDisposable
    {
        ICategoryRepository Categories { get; }

        Task<bool> SaveChangesAsync();
    }
}