using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Interfaces;
using WebAPI.Application.ViewModel;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class VeiculoController : ControllerBase
{
    private readonly IVeiculoService _veiculoService;

    public VeiculoController(IVeiculoService veiculoService)
    {
        _veiculoService = veiculoService;
    }

    [HttpGet]
    public async Task<ActionResult<IList<VeiculoViewModel>>> Get()
    {
        try
        {
            var veiculos = await _veiculoService.GetVeiculosAsync();
            return Ok(veiculos);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}", Name = "GetVeiculoById")]
    public async Task<ActionResult<VeiculoViewModel>> GetById(int id)
    {
        try
        {
            var veiculo = await _veiculoService.GetVeiculoByIdAsync(id);
            if (veiculo is null)
                return NotFound();

            return Ok(veiculo);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetByPlaca/{placa}", Name = "GetByPlaca")]
    public async Task<ActionResult<VeiculoViewModel>> GetByPlaca(string placa)
    {
        try
        {
            var veiculo = await _veiculoService.GetVeiculoByPlacaAsync(placa.ToUpper());
            if (veiculo is null)
                return NotFound();

            return Ok(veiculo);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] VeiculoViewModel veiculo)
    {
        try
        {
            var id = await _veiculoService.CreateAsync(veiculo);
            return CreatedAtRoute("GetVeiculoById", new { id = id }, veiculo);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromBody] VeiculoViewModel veiculo, int id)
    {
        try
        {
            await _veiculoService.UpdateAsync(veiculo, id);
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
            await _veiculoService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
