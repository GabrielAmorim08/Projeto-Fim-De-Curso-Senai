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
    public async Task<UsuarioDTO> GetUserInfo(string token)
    {
        var result = await client.GetAsync($"user/GetUserInfo/{token}");
        var content = await result.Content.ReadFromJsonAsync<UsuarioDTO>();
        if (content == null)
        {
            return null;
        }
        return content;
    }
}