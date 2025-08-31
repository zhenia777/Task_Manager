using Domain.Exceptions;
using Domain.Services.TokenService;
using Domain.UseCases.AccountOperations.Commands.Login;
using FluentAssertions;
using Moq;

namespace Domain.Tests.UseCases.Account;

public class LoginCommandHandlerShould
{
    private readonly LoginCommandHandler _sut;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<ILoginStorage> _loginStorageMock;

    public LoginCommandHandlerShould()
    {
        _tokenServiceMock = new Mock<ITokenService>();
        _loginStorageMock = new Mock<ILoginStorage>();

        _sut = new LoginCommandHandler(_tokenServiceMock.Object, _loginStorageMock.Object);
    }

    [Fact]
    public async Task ReturnLoginUser_WhenValidCredentials()
    {
        var command = new LoginCommand("MyEmail@gmail.com", "myPassword");
        var userId = Guid.NewGuid();
        var expectedToken = "myToken";

        _loginStorageMock
          .Setup(s => s.IsUserExist(It.IsAny<string>(), It.IsAny<CancellationToken>()))
          .ReturnsAsync(true);
        _loginStorageMock
          .Setup(s => s.CheckPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
          .ReturnsAsync(true);
        _loginStorageMock
          .Setup(s => s.GetUserId(It.IsAny<string>(), It.IsAny<CancellationToken>()))
          .ReturnsAsync(userId);
        _tokenServiceMock
          .Setup(s => s.CreateToken(userId))
          .Returns(expectedToken);

        var actual = await _sut.Handle(command, CancellationToken.None);

        actual.Should().NotBeNull();
        actual.Token.Should().Be(expectedToken);

        _loginStorageMock.Verify(s => s.IsUserExist(command.Email, It.IsAny<CancellationToken>()), Times.Once());
        _loginStorageMock.Verify(s => s.CheckPassword(command.Email, command.Password, It.IsAny<CancellationToken>()), Times.Once());
        _loginStorageMock.Verify(s => s.GetUserId(command.Email, It.IsAny<CancellationToken>()), Times.Once());
        _loginStorageMock.VerifyNoOtherCalls();

        _tokenServiceMock.Verify(s => s.CreateToken(userId), Times.Once());
        _tokenServiceMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ReturnFailed_WhenUserIsNotExist()
    {
        var command = new LoginCommand("myEmail@gmail.com", "myPassword");
        _loginStorageMock.Setup(s => s.IsUserExist(It.IsAny<string>(), It.IsAny<CancellationToken>()))
          .ReturnsAsync(false);

        await _sut.Invoking(s => s.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<DomainException>();

        _loginStorageMock.Verify(s => s.IsUserExist(command.Email, It.IsAny<CancellationToken>()), Times.Once());
        _loginStorageMock.Verify(s => s.CheckPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never());
        _loginStorageMock.Verify(s => s.GetUserId(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never());
        _loginStorageMock.VerifyNoOtherCalls();

        _tokenServiceMock.Verify(s => s.CreateToken(It.IsAny<Guid>()), Times.Never());
        _tokenServiceMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ReturnFailed_WhenCredentialsNotValid()
    {
        var command = new LoginCommand("myEmail@gmail.com", "myPassword");
        _loginStorageMock.Setup(s => s.IsUserExist(It.IsAny<string>(), It.IsAny<CancellationToken>()))
          .ReturnsAsync(true);
        _loginStorageMock.Setup(s => s.CheckPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
          .ReturnsAsync(false);

        await _sut.Invoking(s => s.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<DomainException>();

        _loginStorageMock.Verify(s => s.IsUserExist(command.Email, It.IsAny<CancellationToken>()), Times.Once());
        _loginStorageMock.Verify(s => s.CheckPassword(command.Email, command.Password, It.IsAny<CancellationToken>()), Times.Once());
        _loginStorageMock.Verify(s => s.GetUserId(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never());
        _loginStorageMock.VerifyNoOtherCalls();

        _tokenServiceMock.Verify(s => s.CreateToken(It.IsAny<Guid>()), Times.Never());
        _tokenServiceMock.VerifyNoOtherCalls();
    }
}