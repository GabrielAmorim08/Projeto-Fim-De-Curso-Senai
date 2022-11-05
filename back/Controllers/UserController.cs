using Microsoft.AspNetCore.Mvc;
using dto;
namespace back.Controllers;
using back.Model;
[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    [HttpPost("Register")]
    public IActionResult register([FromBody] UsuarioDTO user)
    {
        using siteContext context = new siteContext();
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
        Usuario.Nome = user.Nome;
        Usuario.Email = user.Email;
        Usuario.Senha = user.Senha;
        Usuario.Confsenha = user.Confsenha;

        context.Add(Usuario);
        context.SaveChanges();
        return Ok("Usuario cadastrado com sucesso");
    }
    [HttpPost("Login")]
    public IActionResult Login([FromBody]UsuarioDTO user)
    {
        using siteContext context = new siteContext();
        var possibleUser = context.Usuarios.FirstOrDefault( u => u.Nome == user.Nome);

        if(possibleUser == null)
        {
            return BadRequest("Nome de usuário inválido");
        }
        if(possibleUser.Senha != user.Senha)
        {
            return BadRequest("Senha inválida");
        }
        return Ok();
    }

    [HttpPost("Update")]
        public IActionResult UpdateName()
        {
            throw new NotImplementedException();
        }
}
