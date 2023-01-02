using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Taxually.TechnicalTest.Interfaces.Api.Requests;

namespace Taxually.TechnicalTest.IntegrationTests.Controllers;

public class VatRegistrationControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    private readonly CustomWebApplicationFactory<Program>
        _factory;

    public VatRegistrationControllerTests(
        CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task Register_SendsUKRequest_Returns_Accepted()
    {
        // given
        var request = new VatRegistrationRequest()
        {
            Country = "GB",
            CompanyId = "CompanyId",
            CompanyName = "CompanyName"
        };
        var body = JsonSerializer.Serialize(request);
        
        // when
        var response = await _client.PostAsync("api/VatRegistration", new StringContent(body));

        // then
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
    }
}