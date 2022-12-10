using Microsoft.AspNetCore.Mvc;
using dto;

namespace back.Controllers;
using back.Services;
using Model;
using Trevisharp.Security.Jwt;
using Trevisharp.Security.Jwt.Exceptions;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    [HttpPost("Register")]
    public IActionResult register([FromBody] UsuarioDTO user)
    {
        using TccSiteContext context = new TccSiteContext();
        List<string> errors = new List<string>();
        if (user.Nome == null)
        {
            errors.Add("Nome não foi informado");
        }
        if(user.Email == null)
        {
            errors.Add("email não foi informado");
        }
        if(user.Senha.Length < 5)
        {
            errors.Add("senha deve conter mais que 8 letras");
        }
        if(user.Senha == null || user.Confsenha == null)
        {
            errors.Add("Senha não foi informada");
        }
        if (user.Senha != user.Confsenha)
        {
            errors.Add("As duas senhas não são iguais");
        }
        if(errors.Count > 0)
        {
            return this.BadRequest(errors);
        }

        Usuario Usuario = new Usuario();
        Usuario.Matricula = user.Matricula;
        Usuario.Nome = user.Nome;
        Usuario.Email = user.Email;
        Usuario.Senha = user.Senha;
        Usuario.Confsenha = user.Confsenha;
        context.Add(Usuario);
        context.SaveChanges();
        return Ok("Usuario cadastrado com sucesso");
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody]UsuarioDTO user,
        [FromServices]CryptoService jwt)
    {
        using TccSiteContext context = new TccSiteContext();
        var possibleUser = context.Usuarios
            .FirstOrDefault( u => u.Matricula == user.Matricula);

        if(possibleUser == null)
        {
            return BadRequest("Nome de usuário inválido");
        }
        if(possibleUser.Senha != user.Senha)
        {
            return BadRequest("Senha inválida");
        }

        UserInfoToken info = new UserInfoToken();
        info.Nome = user.Nome;
        info.Matricula = possibleUser.Matricula;
        info.email = user.Email;
        var token = jwt.GetToken(info);

        return Ok(token);
    }
    
    [HttpPost("GetUserInfo")]
    public async Task<IActionResult> GetUserInfo([FromBody] string token, [FromServices]CryptoService jwt)
    {
        try
        {
            var info = jwt.Validate<UserInfoToken>(token);
            string matricula = info.Matricula;

            using TccSiteContext context = new TccSiteContext();
            var user = context.Usuarios.FirstOrDefault(u => u.Matricula == matricula);

            if (user == null)
                return Ok(new {
                    Status = "Error",
                    Message = "Usuário Inexistente."
                });

            UsuarioDTO userData = new UsuarioDTO();
            userData.Matricula = matricula;
            userData.Nome = user.Nome;
            userData.Email = user.Email;
            userData.empresa = user.Empresa;
            userData.Setor = user.Setor;
            userData.cidade = user.Cidade;
            userData.endereco = user.Endereco;
            userData.estado = user.Estado;
            userData.DataNascimento = user.DataNascimento ?? DateTime.Now;
            userData.telefone = user.Telefone;
            userData.UF = user.Uf;

            return Ok(userData);
        }
        catch (Exception e) when (
            e is JwtInvalidFormatException || 
            e is JwtInvalidPayloadException || 
            e is JwtInvalidSignatureException)
        {
            return Ok(new {
                Status = "Error",
                Message = "Token inválido."
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Ok(new {
                Status = "Error",
                Message = "Erro interno no servidor."
            });
        }
    }

    [HttpPost("Update")]
    public IActionResult UpdateName([FromBody] UsuarioDTO user)
    {
        using TccSiteContext context = new TccSiteContext();
        List<string> errors = new List<string>();
        Usuario Usuario = new Usuario();
        Usuario.Matricula = user.Matricula;
        Usuario.Nome = user.Nome;
        Usuario.Email = user.Email;
        Usuario.Estado = user.estado;
        Usuario.Empresa = user.empresa;
        Usuario.Setor = user.Setor;
        Usuario.Cidade = user.cidade;
        Usuario.Endereco = user.endereco;
        Usuario.DataNascimento = user.DataNascimento;
        Usuario.Telefone = user.telefone;
        Usuario.Uf = user.UF;

        context.Entry(Usuario);
        context.Update(Usuario);
        return Ok("Informações alterado com sucessoo");
    }
}