using System;
using System.Collections.Generic;

namespace back.Model
{
    public partial class PostAtividade
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime Moment { get; set; }
        public string Content { get; set; } = null!;

        public virtual Usuario? User { get; set; }
    }
}
