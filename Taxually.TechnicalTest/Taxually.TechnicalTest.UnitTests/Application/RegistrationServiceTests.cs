using Moq;
using Taxually.TechnicalTest.Application.Models;
using Taxually.TechnicalTest.Application.Services;
using Taxually.TechnicalTest.Infrastructure.Http;
using Taxually.TechnicalTest.Infrastructure.Queue;

namespace Taxually.TechnicalTest.UnitTests.Application;

public class RegistrationServiceTests
{
    private readonly Mock<ITaxuallyHttpClient> _taxuallyHttpClient;
    private readonly Mock<ITaxuallyQueueClient> _taxuallyQueueClient;

    private readonly RegistrationService _registrationService;


    public RegistrationServiceTests()
    {
        _taxuallyHttpClient = new Mock<ITaxuallyHttpClient>();
        _taxuallyQueueClient = new Mock<ITaxuallyQueueClient>();

        _registrationService = new RegistrationService(_taxuallyHttpClient.Object, _taxuallyQueueClient.Object);
    }

    [Fact]
    public async Task SendUK_Call_TaxuallyHttpClient()
    {
        // given
        var command = new RegistrationCommand()
        {
            Country = "GB",
            CompanyId = "CompanyId",
            CompanyName = "CompanyName"
        };

        _taxuallyHttpClient
            .Setup(x => x.PostAsync(It.IsAny<RegistrationCommand>()))
            .Returns(Task.CompletedTask);

        // when
        await _registrationService.SendUK(command);
        
        // then
        _taxuallyHttpClient.Verify(x => x.PostAsync(It.IsAny<RegistrationCommand>()), Times.Once);
    }
}