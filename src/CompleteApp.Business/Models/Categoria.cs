using System.Collections.Generic;

namespace CompleteApp.Business.Models
{
    public class Categoria : Entity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<Produto> Produtos { get; set; }
    }
}
