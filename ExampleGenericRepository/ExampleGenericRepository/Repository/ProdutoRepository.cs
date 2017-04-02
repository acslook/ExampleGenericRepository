using ExampleGenericRepository.Interfaces;
using ExampleGenericRepository.Models;
using SQLite.Net;

namespace ExampleGenericRepository.Repository
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(SQLiteConnection connection) 
            : base(connection)
        {
        }
        
    }
}
