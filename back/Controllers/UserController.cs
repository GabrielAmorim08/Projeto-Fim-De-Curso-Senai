using Microsoft.AspNetCore.Mvc;
using dto;

namespace back.Controllers;

using back.Services;
using Model;

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
    public async Task<IActionResult> Login([FromBody]UsuarioDTO user, [FromServices]TokenService service)
    {
        using TccSiteContext context = new TccSiteContext();
        var possibleUser = context.Usuarios.FirstOrDefault( u => u.Matricula == user.Matricula);

        if(possibleUser == null)
        {
            return BadRequest("Nome de usuário inválido");
        }
        if(possibleUser.Senha != user.Senha)
        {
            return BadRequest("Senha inválida");
        }
        var token = await service.CreateToken(possibleUser);
        return Ok(token.Valor);
    }

    [HttpPost("Update")]
        public IActionResult UpdateName()
        {
            throw new NotImplementedException();
        }
}