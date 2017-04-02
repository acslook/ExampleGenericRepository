using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleGenericRepository.Models
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int ProdutoId { get; set; }
        [MaxLength(150)]
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}
