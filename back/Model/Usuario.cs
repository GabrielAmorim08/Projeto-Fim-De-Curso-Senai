﻿using System;
using System.Collections.Generic;

namespace back.Model;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string Confsenha { get; set; } = null!;

    public int? Matricula { get; set; }

    public virtual ICollection<Cargo> Cargos { get; } = new List<Cargo>();

    public virtual ICollection<PostAtividade> PostAtividades { get; } = new List<PostAtividade>();

    public virtual ICollection<Token> Tokens { get; } = new List<Token>();
}
