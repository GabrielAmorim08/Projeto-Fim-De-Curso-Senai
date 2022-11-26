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
        info.Name = user.Nome;
        info.Matricula = possibleUser.Matricula;
        info.email = user.Email;
        var token = jwt.GetToken(info);

        return Ok(token);
    }
    
    [HttpGet("GetUserInfo/{token}")]
    public async Task<IActionResult> GetUserInfo(string token, [FromServices]CryptoService jwt)
    {
        try
        {
            var info = jwt.Validate<UserInfoToken>(token);
            string matricula = info.Matricula;

            using TccSiteContext context = new TccSiteContext();
            var user = await context.Usuarios.FindAsync(matricula);

            if (user == null)
                return Ok(new {
                    Status = "Error",
                    Message = "Usuário Inexistente."
                });

            UsuarioDTO userData = new UsuarioDTO();
            user.Matricula = matricula;
            userData.Nome = user.Nome;
            user.Email = user.Email;

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
            return Ok(new {
                Status = "Error",
                Message = "Erro interno no servidor."
            });
        }
    }

    [HttpPost("Update")]
    public IActionResult UpdateName()
    {
        throw new NotImplementedException();
    }
}