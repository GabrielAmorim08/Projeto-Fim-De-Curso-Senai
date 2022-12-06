using System;
using System.Collections.Generic;

namespace back.Model;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string Confsenha { get; set; } = null!;

    public string Matricula { get; set; } = null!;

    public string? Telefone { get; set; }

    public string? Endereco { get; set; }

    public string? Uf { get; set; }

    public string? Estado { get; set; }

    public DateTime? DataNascimento { get; set; }

    public string? Setor { get; set; }

    public string? Cidade { get; set; }

    public string? Empresa { get; set; }

    public virtual ICollection<PostAtividade> PostAtividades { get; } = new List<PostAtividade>();

    public virtual ICollection<Token> Tokens { get; } = new List<Token>();
}
