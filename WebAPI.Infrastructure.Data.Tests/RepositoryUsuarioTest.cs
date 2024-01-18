using Moq;
using WebAPI.Domain.Interfaces;
using WebAPI.Domain.Model;

namespace WebAPI.Infrastructure.Data.Tests;

public class RepositoryUsuarioTest
{
    #region Mock

    private static List<Usuario> Usuarios
    {
        get => new()
        {
            RetornaUsuarioTeste(1),
            RetornaUsuarioTeste(2),
            RetornaUsuarioTeste(3),
        };
    }

    private static Usuario RetornaUsuarioTeste(int id = 0)
    {
        return new Usuario() { Id = id, Nome = $"Teste {id}", Email = $"teste{id}@email.com", Senha = "123" };
    }

    private static IUsuarioRepository UsuarioRepository
    {
        get => RetornaMockIUsuarioRepository();
    }

    private static IUsuarioRepository RetornaMockIUsuarioRepository()
    {
        var mock = new Mock<IUsuarioRepository>();

        mock.Setup(x => x.FindAllAsync())
            .ReturnsAsync(Usuarios);

        mock.Setup(x => x.FindByIdAsync(It.IsAny<int>()))
                                 .ReturnsAsync((int id) => Usuarios.FirstOrDefault(x => x.Id == id));

        mock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((string email)  => Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper()));

        mock.Setup(x => x.CreateAsync(It.IsAny<Usuario>()))
            .ReturnsAsync((Usuario u) => Usuarios.Max(x => x.Id) + 1);

        mock.Setup(x => x.UpdateAsync(It.IsAny<Usuario>()))
            .ReturnsAsync((Usuario u) => u ?? throw new ArgumentNullException(nameof(u), "O objeto usuário estava vazio"));
        
        mock.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                    .ReturnsAsync((int id) => Usuarios.Any(x => x.Id == id) ? true : throw new Exception($"Usuário de Id {id} não encontrado"));

        return mock.Object;
    }

    #endregion Mock


    #region FindAllAsync

    [Fact]
    public async void RetornarUmaListaDosUsuarios()
    {
        // Arrange

        // Act
        var usuarios = await UsuarioRepository.FindAllAsync();

        // Assert
        Assert.NotNull(usuarios);
    }

    #endregion FindAllAsync

    #region FindByIdAsync

    [Fact]
    public async void RetornarUmUsuarioDeId1QuandoPesquisarPorId()
    {
        // Arrange
        var id = 1;

        // Act
        var usuario = await UsuarioRepository.FindByIdAsync(id);

        // Assert
        Assert.NotNull(usuario);
        Assert.Equal(1, usuario.Id);
    }

    [Fact]
    public async void RetornarNuloQuandoPesquisarPorIdInexistente()
    {
        // Arrange
        var id = 0;

        // Act
        var usuario = await UsuarioRepository.FindByIdAsync(id);

        // Assert
        Assert.Null(usuario);
    }

    #endregion FindByIdAsync

    #region FindByEmailAsync

    [Fact]
    public async void RetornarUmUsuarioQuandoPesquisarPorEmailValido()
    {
        // Arrange
        var email = "teste1@email.com";

        // Act
        var usuario = await UsuarioRepository.FindByEmailAsync(email);

        // Assert
        Assert.NotNull(usuario);
        Assert.Equal(email, usuario.Email);
    }

    [Fact]
    public async void RetornarNuloQuandoPesquisarPorEmailInvalido()
    {
        // Arrange
        var email = "teste@email.com";

        // Act
        var usuario = await UsuarioRepository.FindByEmailAsync(email);

        // Assert
        Assert.Null(usuario);
    }

    #endregion FindByEmailAsync

    #region CreateAsync

    [Fact]
    public async void RetornarProximoIdAposCreateDeNovoUsuario()
    {
        // Arrange
        var novoUsuario = RetornaUsuarioTeste();

        // Act
        var id = await UsuarioRepository.CreateAsync(novoUsuario);

        // Assert
        Assert.Equal(Usuarios.Max(x => x.Id) + 1, id);
    }

    #endregion CreateAsync

    #region UpdateAsync

    [Fact]
    public async void RetornarUsuarioAposUpdate()
    {
        // Arrange
        var novoUsuario = RetornaUsuarioTeste(1);

        // Act
        var usuario = await UsuarioRepository.UpdateAsync(novoUsuario);

        // Assert
        Assert.Equal(usuario, novoUsuario);
    }

    [Fact]
    public async Task RetornarArgumentNullExceptionAposUpdateComUsuarioNull()
    {
        // Arrange
        Usuario novoUsuario = null;

        // Act
        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => UsuarioRepository.UpdateAsync(novoUsuario));
    }

    #endregion UpdateAsync

    #region DeleteAsync

    [Fact]
    public async void RetornarTrueAoDeleteUsuarioValido()
    {
        // Arrange
        var id = 1;

        // Act
        var retorno = await UsuarioRepository.DeleteAsync(id);

        // Assert
        Assert.True(retorno);
    }

    [Fact]
    public async Task RetornarExceptionAoDeleteUsuarioInvalido()
    {
        // Arrange
        var id = 0;

        // Act
        // Assert
        await Assert.ThrowsAsync<Exception>(() => UsuarioRepository.DeleteAsync(id));
    }

    #endregion DeleteAsync
}