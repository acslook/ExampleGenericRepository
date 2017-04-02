using ExampleGenericRepository.Interfaces;
using ExampleGenericRepository.Models;
using ExampleGenericRepository.Repository;
using SQLite.Net;
using System;
using Xamarin.Forms;

namespace ExampleGenericRepository.UoW
{
    public class UnitOfWork : IDisposable
    {
        protected readonly SQLiteConnection Connection;
        private ProdutoRepository _produtoRepository;
        public UnitOfWork()
        {
            Connection = DependencyService.Get<ISQLite>().GetConnection();
            Connection.CreateTable<Produto>();
        }

        public ProdutoRepository ProdutoRepository
        {
            get
            {

                if (_produtoRepository == null)
                {
                    _produtoRepository = new ProdutoRepository(Connection);
                }
                return _produtoRepository;
            }
        }
    

        public void BeginTransaction()
        {
            Connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            Connection.Commit();
        }

        public void RollBackTransaction()
        {
            Connection.Rollback();
        }

        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Connection.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
