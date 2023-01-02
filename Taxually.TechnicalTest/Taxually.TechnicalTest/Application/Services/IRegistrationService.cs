using Taxually.TechnicalTest.Application.Models;

namespace Taxually.TechnicalTest.Application.Services;

public interface IRegistrationService
{
    Task SendUK(RegistrationCommand command);
    
    Task SendFR(RegistrationCommand command);
    
    Task SendDE(RegistrationCommand command);
}