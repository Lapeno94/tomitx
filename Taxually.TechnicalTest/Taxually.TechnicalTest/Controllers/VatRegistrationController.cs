using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Application.Models;
using Taxually.TechnicalTest.Application.Services;
using Taxually.TechnicalTest.Interfaces.Api.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taxually.TechnicalTest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VatRegistrationController : ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public VatRegistrationController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    /// <summary>
    /// Registers a company for a VAT number in a given country
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // todo if infra is real [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Register(VatRegistrationRequest request)
    {
        if (string.IsNullOrEmpty(request.Country))
            return BadRequest();
        // better to use mediatr to create handler by commands
        var command = new RegistrationCommand()
            { Country = request.Country, CompanyId = request.CompanyId, CompanyName = request.CompanyName };

        var task = request.Country switch
        {
            "GB" => _registrationService.SendUK(command),
            "FR" => _registrationService.SendFR(command),
            "DE" => _registrationService.SendDE(command),
            _ => null
        };

        if (task is null)
        {
            return NotFound("Requested Country haven't supported.");
        }

        await task;

        return Accepted();
    }
}