using dto;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace front.Services;

public class UserService
{
    HttpClient client;
    //Deixar padrão o local host que foi declarado no Program.cs
    public UserService(string server)
    {
        client = new HttpClient();
        client.BaseAddress = new Uri(server);
    }
    //função task para realizar o registro
    public async Task Register(
        string matricula,
        string nome,
        string email,
        string senha,
        string confsenha)
    {
        UsuarioDTO user = new UsuarioDTO();
        user.Matricula = matricula;
        user.Nome = nome;
        user.Email = email;
        user.Senha = senha;
        user.Confsenha = confsenha;
        var result = await client.PostAsJsonAsync("user/Register", user);
    }
    //função task para realizar o login
    public async Task<string> Login(
        string matricula,
        string senha
    )
    {
        UsuarioDTO user = new UsuarioDTO();
        user.Matricula = matricula;
        user.Senha = senha;
        var result = await client.PostAsJsonAsync("user/Login", user);

        if (result.StatusCode != HttpStatusCode.OK)
        {
            return null;
        }
        var token = await result.Content.ReadAsStringAsync();
        return token;
    }
    public async Task UpdateInfo(
        string matricula,
        string nome,
        string email,
        string endereco,
        string UF,
        string estado,
        string cidade,
        string Setor,
        string telefone,
        string empresa,
        DateTime DataNascimento 
        )
    {
        UsuarioDTO user = new UsuarioDTO();
        user.Matricula = matricula;
        user.Nome = nome;
        user.Email = email;
        user.telefone = telefone;
        user.endereco = endereco;
        user.UF = UF;
        user.estado = estado;
        user.cidade = cidade;
        user.empresa = empresa;
        user.Setor = Setor;
        user.DataNascimento = DataNascimento;
        var result = await client.PostAsJsonAsync("user/Update", user);
    }
    //funçaõ para pegar as informações do Usuario
    public async Task<UsuarioDTO> GetUserInfo(string token)
    {
        var result = await client.PostAsJsonAsync($"user/GetUserInfo", token);
        var content = await result.Content.ReadFromJsonAsync<UsuarioDTO>();
        if (content == null)
        {
            return null;
        }
        return content;
    }

}