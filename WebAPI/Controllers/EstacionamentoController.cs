using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Interfaces;
using WebAPI.Application.ViewModel;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class EstacionamentoController : ControllerBase
{
    private readonly IEstacionamentoService _estacionamentoService;

    public EstacionamentoController(IEstacionamentoService estacionamentoService)
    {
        _estacionamentoService = estacionamentoService;
    }


    [HttpGet]
    public async Task<ActionResult<IList<EstacionamentoViewModel>>> Get()
    {
        try
        {
            var estacionamentos = await _estacionamentoService.GetEstacionamentosAsync();
            return Ok(estacionamentos);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{id}", Name = "GetEstacionamentoById")]
    public async Task<ActionResult<EstacionamentoViewModel>> GetById(int id)
    {
        try
        {
            var estacionamento = await _estacionamentoService.GetEstacionamentoByIdAsync(id);
            if (estacionamento is null)
                return NotFound();

            return Ok(estacionamento);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] EstacionamentoViewModel estacionamento)
    {
        try
        {
            var id = await _estacionamentoService.CreateAsync(estacionamento);
            return CreatedAtRoute("GetEstacionamentoById", new { id = id }, estacionamento);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromBody] EstacionamentoViewModel estacionamento, int id)
    {
        try
        {
            await _estacionamentoService.UpdateAsync(estacionamento, id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Apagar(int id)
    {
        try
        {
            await _estacionamentoService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
