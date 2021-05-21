using System;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;

namespace DAL.Interfaces
{
    public interface IUoW : IDisposable
    {
        IBrandRepository Brands { get; }
        
        Task<bool> SaveChangesAsync();
    }
}