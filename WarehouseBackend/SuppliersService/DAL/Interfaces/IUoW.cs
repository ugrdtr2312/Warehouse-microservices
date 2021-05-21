using System;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;

namespace DAL.Interfaces
{
    public interface IUoW : IDisposable
    {
        ISupplierRepository Suppliers { get; }

        Task<bool> SaveChangesAsync();
    }
}