using Moq;
using WebAPI.Application.Interfaces;
using WebAPI.Application.ViewModel;

namespace WebAPI.Application.Services.Tests;

public class ServiceUsuarioTest
{
    #region Mock

    private static List<UsuarioViewModel> Usuarios
    {
        get => new()
        {
            RetornaUsuarioTeste(1),
            RetornaUsuarioTeste(2),
            RetornaUsuarioTeste(3),
        };
    }

    private static UsuarioViewModel RetornaUsuarioTeste(int id = 0)
    {
        return new UsuarioViewModel() { Id = id, Nome = $"Teste {id}", Email = $"teste{id}@email.com", Senha = "123" };
    }

    private static IUsuarioService UsuarioService
    {
        get => RetornaMockIUsuarioService();
    }

    private static IUsuarioService RetornaMockIUsuarioService()
    {
        var mock = new Mock<IUsuarioService>();

        mock.Setup(x => x.GetUsuariosAsync())
            .ReturnsAsync(Usuarios);

        mock.Setup(x => x.GetUsuarioByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => Usuarios.FirstOrDefault(x => x.Id == id));

        mock.Setup(x => x.GetUsuarioByEmailAsync(It.IsAny<string>()))
           .ReturnsAsync((string email) => Usuarios.FirstOrDefault(x => x.Email == email));

        mock.Setup(x => x.CreateAsync(It.IsAny<UsuarioViewModel>()))
           .ReturnsAsync((UsuarioViewModel u) => Usuarios.Max(x => x.Id) + 1);

        mock.Setup(x => x.UpdateAsync(It.IsAny<UsuarioViewModel>(), It.IsAny<int>()))
            .ReturnsAsync((UsuarioViewModel u, int id) => Usuarios.Any(x => x.Id == id) ? (u ?? throw new ArgumentNullException(nameof(u), "O objeto usuário estava vazio")) : throw new Exception($"Usuário de id {id} não encontrado"));

        mock.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                    .ReturnsAsync((int id) => Usuarios.Any(x => x.Id == id) ? true : throw new Exception($"Usuário de Id {id} não encontrado"));


        return mock.Object;
    }

    #endregion Mock


    #region GetUsuariosAsync

    [Fact]
    public async void RetornaUmaListaDosUsuarios()
    {
        // Arrange

        // Act
        var usuarios = await UsuarioService.GetUsuariosAsync();

        // Assert
        Assert.NotNull(usuarios);
    }

    #endregion GetUsuariosAsync

    #region GetUsuarioByIdAsync

    [Fact]
    public async void RetornarUmUsuarioDeId1QuandoPesquisarPorId()
    {
        // Arrange
        var id = 1;

        // Act
        var usuario = await UsuarioService.GetUsuarioByIdAsync(id);

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
        var usuario = await UsuarioService.GetUsuarioByIdAsync(id);

        // Assert
        Assert.Null(usuario);
    }

    #endregion GetUsuarioByIdAsync

    #region GetUsuarioByEmailAsync

    [Fact]
    public async void RetornarUmUsuarioQuandoPesquisarPorEmailValido()
    {
        // Arrange
        var email = "teste1@email.com";

        // Act
        var usuario = await UsuarioService.GetUsuarioByEmailAsync(email);

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
        var usuario = await UsuarioService.GetUsuarioByEmailAsync(email);

        // Assert
        Assert.Null(usuario);
    }

    #endregion GetUsuarioByEmailAsync

    #region CreateAsync

    [Fact]
    public async void RetornarProximoIdAposCreateDeNovoUsuario()
    {
        // Arrange
        var novoUsuario = RetornaUsuarioTeste();

        // Act
        var id = await UsuarioService.CreateAsync(novoUsuario);

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
        var usuario = await UsuarioService.UpdateAsync(novoUsuario, novoUsuario.Id);

        // Assert
        Assert.Equal(usuario, novoUsuario);
    }

    [Fact]
    public async Task RetornarArgumentNullExceptionAposUpdateComUsuarioNull()
    {
        // Arrange
        UsuarioViewModel novoUsuario = null;

        // Act
        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => UsuarioService.UpdateAsync(novoUsuario, 1));
    }

    [Fact]
    public async Task RetornarExceptionAoUpdateComIdInvalido()
    {
        // Arrange
        var novoUsuario = RetornaUsuarioTeste(99);

        // Act
        // Assert
        await Assert.ThrowsAsync<Exception>(() => UsuarioService.UpdateAsync(novoUsuario, novoUsuario.Id));
    }


    #endregion UpdateAsync

    #region DeleteAsync

    [Fact]
    public async void RetornarTrueAoDeleteUsuarioValido()
    {
        // Arrange
        var id = 1;

        // Act
        var retorno = await UsuarioService.DeleteAsync(id);

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
        await Assert.ThrowsAsync<Exception>(() => UsuarioService.DeleteAsync(id));
    }

    #endregion DeleteAsync
}