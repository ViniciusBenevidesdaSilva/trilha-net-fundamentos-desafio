using WebAPI.Domain.Utils;

namespace WebAPI.Domain.Tests;

public class UtilsRegexTest
{
    [Theory]
    [InlineData("teste@email.com")]
    [InlineData("aluno@escola.edu.br")]
    public void RecebeEmailValidoERetornaTrue(string email)
    {
        // Arrange

        // Act
        var retorno = Regex.ValidaFormatoEmail(email);

        // Assert
        Assert.True(retorno);
    }

    [Theory]
    [InlineData("")]
    [InlineData("@")]
    [InlineData("teste")]
    [InlineData("teste@")]
    [InlineData("@teste")]
    [InlineData("teste@@teste.com")]
    [InlineData("123@email.com")]
    [InlineData("teste@teste..com")]
    public void RecebeEmailInValidoERetornaFalse(string email)
    {
        // Arrange

        // Act
        var retorno = Regex.ValidaFormatoEmail(email);

        // Assert
        Assert.False(retorno);
    }

    [Theory]
    [InlineData("ABC-1234")]
    [InlineData("ABC-1D34")]
    [InlineData("abc-1D34")]
    public void RecebePlacaValidaERetornaTrue(string placa)
    {
        // Arrange

        // Act
        var retorno = Regex.ValidaFormatoPlaca(placa);

        // Assert
        Assert.True(retorno);
    }

    [Theory]
    [InlineData("")]
    [InlineData("A")]
    [InlineData("1")]
    [InlineData("ABC1234")]
    [InlineData("ABC1D34")]
    [InlineData("ABC-123A")]
    public void RecebePlacaInValidaERetornaFalse(string placa)
    {
        // Arrange

        // Act
        var retorno = Regex.ValidaFormatoPlaca(placa);

        // Assert
        Assert.False(retorno);
    }
}
