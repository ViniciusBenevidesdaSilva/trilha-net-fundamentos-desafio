namespace WebAPI.Domain.Model;

public class Usuario : Entity
{
    public string? Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
}
