using System;
using System.Collections.Generic;

namespace back.Model
{
    public partial class PostAtividade
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime Momento { get; set; }
        public string Conteudo { get; set; } = null!;

        public virtual Usuario? User { get; set; }
    }
}
