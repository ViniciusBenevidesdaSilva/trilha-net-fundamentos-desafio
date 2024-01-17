using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Interfaces;
using WebAPI.Application.ViewModel;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class TransacaoController : ControllerBase
{
    private readonly ITransacaoService _transacaoService;

    public TransacaoController(ITransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }

    [HttpGet]
    public async Task<ActionResult<IList<TransacaoViewModel>>> Get()
    {
        try
        {
            var transacoes = await _transacaoService.GetTransacoesAsync();
            return Ok(transacoes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}", Name = "GetTransacaoById")]
    public async Task<ActionResult<TransacaoViewModel>> GetById(int id)
    {
        try
        {
            var transacao = await _transacaoService.GetTransacaoByIdAsync(id);
            if (transacao is null)
                return NotFound();

            return Ok(transacao);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Estacionados", Name = "GetEstacionadosAsync")]
    public async Task<ActionResult<IList<TransacaoViewModel>>> GetEstacionadosAsync()
    {
        try
        {
            var transacoes = await _transacaoService.GetEstacionadosAsync();
            return Ok(transacoes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("Estacionados/{estacionamentoId}", Name = "GetEstacionadosByEstacionamentoIdAsync")]
    public async Task<ActionResult<IList<TransacaoViewModel>>> GetEstacionadosByEstacionamentoIdAsync(int estacionamentoId)
    {
        try
        {
            var transacoes = await _transacaoService.GetEstacionadosByEstacionamentoIdAsync(estacionamentoId);
            return Ok(transacoes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Receita", Name = "GetReceitaTotalAsync")]
    public async Task<ActionResult<double>> GetReceitaAsync()
    {
        try
        {
            var receita = await _transacaoService.GetReceitaAsync();
            return Ok(receita);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Receita/{inicio}", Name = "GetReceitaPorPeriodoAsyncInicio")]
    public async Task<ActionResult<double>> GetReceitaAsync(DateTime inicio)
    {
        try
        {
            var receita = await _transacaoService.GetReceitaAsync(inicio);
            return Ok(receita);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Receita/{inicio}/{fim}", Name = "GetReceitaPorPeriodoAsyncInicioFim")]
    public async Task<ActionResult<double>> GetReceitaAsync(DateTime inicio, DateTime fim)
    {
        try
        {
            var receita = await _transacaoService.GetReceitaAsync(inicio, fim);
            return Ok(receita);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Estacionado/{veiculoPlaca}", Name = "IsVeiculoEstacionadoAsync")]
    public async Task<ActionResult<bool>> IsVeiculoEstacionadoAsync(string veiculoPlaca)
    {
        try
        {
            var estaEstacionado = await _transacaoService.IsVeiculoEstacionadoAsync(veiculoPlaca);
            return Ok(estaEstacionado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] TransacaoViewModel transacao)
    {
        try
        {
            var id = await _transacaoService.CreateAsync(transacao);
            return CreatedAtRoute("GetTransacaoById", new { id = id }, transacao);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromBody] TransacaoViewModel transacao, int id)
    {
        try
        {
            await _transacaoService.UpdateAsync(transacao, id);
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
            await _transacaoService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
