using System;
using System.Collections.Generic;

namespace back.Model
{
    public partial class Usuario
    {
        public Usuario()
        {
            Cargos = new HashSet<Cargo>();
            PostAtividades = new HashSet<PostAtividade>();
            Tokens = new HashSet<Token>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public string Confsenha { get; set; } = null!;
        public string? Matricula { get; set; }

        public virtual ICollection<Cargo> Cargos { get; set; }
        public virtual ICollection<PostAtividade> PostAtividades { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
    }
}
