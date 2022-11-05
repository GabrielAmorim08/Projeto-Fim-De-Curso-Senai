using dto;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace front.Services;

public class UserService
{
    HttpClient client;

    public UserService(string server)
    {
        client = new HttpClient();
        client.BaseAddress = new Uri(server);
    }
    
    public async Task Register(
        string nome,
        string email,
        string senha,
        string confsenha)
    {
        UsuarioDTO user = new UsuarioDTO();
        user.Nome = nome;
        user.Email = email;
        user.Senha = senha;
        user.Confsenha = confsenha;
        var result = await client.PostAsJsonAsync("user/Register", user);

    }

    public async Task<string> Login(
        string nome,
        string senha
    )
    {
        UsuarioDTO user = new UsuarioDTO();
        user.Nome = nome;
        user.Senha = senha;
    var result = await client.PostAsJsonAsync("user/Login", user);

    if (result.StatusCode != HttpStatusCode.OK)
    {
        return null;
    }
    var token = await result.Content.ReadAsStringAsync();
    return token;
    }
}