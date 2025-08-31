using Domain.Exceptions;
using Domain.UseCases.AccountOperations.Commands.Login;
using Domain.UseCases.AccountOperations.Commands.Registration;
using FluentAssertions;
using Moq;

namespace Domain.Tests.UseCases.Account;

public class RegistrationCommandHandlerShould
{
    private readonly RegistrationCommandHandler sut;
    private readonly Mock<IRegistrationStorage> registrationStorageMock;
    private readonly Mock<ILoginStorage> loginStorageMock;

    public RegistrationCommandHandlerShould()
    {
        registrationStorageMock = new Mock<IRegistrationStorage>();
        loginStorageMock = new Mock<ILoginStorage>();

        sut = new RegistrationCommandHandler(registrationStorageMock.Object, loginStorageMock.Object);
    }

    [Fact]
    public async Task ReturnRegistrationUser_WhenValidCredentials()
    {
        var command = new RegistrationCommand("myUserName", "myEmail", "myPassword");

        loginStorageMock
            .Setup(s => s.IsUserExist(command.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        registrationStorageMock
            .Setup(s => s.CreateUser(command.UserName, command.Email, command.Password, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        await sut.Handle(command, CancellationToken.None);

        loginStorageMock.Verify(s => s.IsUserExist(command.Email, It.IsAny<CancellationToken>()), Times.Once());
        loginStorageMock.VerifyNoOtherCalls();

        registrationStorageMock.Verify(s => s.CreateUser(command.UserName, command.Email, command.Password, 
                                                         It.IsAny<CancellationToken>()), Times.Once());
        registrationStorageMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ThrowDomainException_WhenUserAlreadyExists()
    {
        var command = new RegistrationCommand("myUserName", "myEmail", "myPassword");

        loginStorageMock
            .Setup(s => s.IsUserExist(command.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        await sut.Invoking(s => s.Handle(command, CancellationToken.None))
                 .Should().ThrowAsync<DomainException>()
                 .WithMessage("User with this email or username already exist.");

        loginStorageMock.Verify(s => s.IsUserExist(command.Email, It.IsAny<CancellationToken>()), Times.Once());
        loginStorageMock.VerifyNoOtherCalls();

        registrationStorageMock.Verify(s => s.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never());
        registrationStorageMock.VerifyNoOtherCalls();
    }
}

