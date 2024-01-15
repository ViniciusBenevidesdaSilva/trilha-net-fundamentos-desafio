using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Interfaces;
using WebAPI.Application.ViewModel;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }


    [HttpGet]
    public async Task<ActionResult<IList<UsuarioViewModel>>> Get()
    {
        try
        {
            var usuarios = await _usuarioService.GetUsuariosAsync();
            return Ok(usuarios);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}", Name = "GetUsuarioById")]
    public async Task<ActionResult<UsuarioViewModel>> GetById(int id)
    {
        try
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetByEmail/{email}", Name = "GetByEmail")]
    public async Task<ActionResult<UsuarioViewModel>> GetByEmail(string email)
    {
        try
        {
            var usuario = await _usuarioService.GetUsuarioByEmailAsync(email);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("Autenticacao")]
    public async Task<ActionResult<bool>> AutenciaUsuario([FromBody] UsuarioViewModel usuario)
    {
        try
        {
            var usuarioAutenticado = await _usuarioService.AutenticaUsuario(usuario);
            
            return Ok(usuarioAutenticado != null);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] UsuarioViewModel usuario)
    {
        try
        {
            var id = await _usuarioService.CreateAsync(usuario);
            return CreatedAtRoute("GetUsuarioById", new { id = id }, usuario);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromBody] UsuarioViewModel usuario, int id)
    {
        try
        {
            await _usuarioService.UpdateAsync(usuario, id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _usuarioService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
