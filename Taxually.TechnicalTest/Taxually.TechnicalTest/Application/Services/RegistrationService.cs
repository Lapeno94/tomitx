using System.Text;
using System.Xml.Serialization;
using Taxually.TechnicalTest.Application.Models;
using Taxually.TechnicalTest.Infrastructure.Http;
using Taxually.TechnicalTest.Infrastructure.Queue;

namespace Taxually.TechnicalTest.Application.Services;

public class RegistrationService : IRegistrationService
{
    private readonly ITaxuallyHttpClient _taxuallyHttpClient;
    private readonly ITaxuallyQueueClient _taxuallyQueueClient;

    public RegistrationService(ITaxuallyHttpClient taxuallyHttpClient, ITaxuallyQueueClient taxuallyQueueClient)
    {
        _taxuallyHttpClient = taxuallyHttpClient;
        _taxuallyQueueClient = taxuallyQueueClient;
    }

    public async Task SendUK(RegistrationCommand command)
    {
        try
        {
            // UK has an API to register for a VAT number
            await _taxuallyHttpClient.PostAsync(command);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }
    }

    public async Task SendFR(RegistrationCommand command)
    {
        try
        {
            // France requires an excel spreadsheet to be uploaded to register for a VAT number
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("CompanyName,CompanyId");
            csvBuilder.AppendLine($"{command.CompanyName}{command.CompanyId}");
            var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            // Queue file to be processed
            await _taxuallyQueueClient.EnqueueAsync("vat-registration-csv", csv);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }
    }

    public async Task SendDE(RegistrationCommand command)
    {
        try
        {
            // Germany requires an XML document to be uploaded to register for a VAT number
            await using var stringWriter = new StringWriter();
            var serializer = new XmlSerializer(typeof(RegistrationCommand));
            serializer.Serialize(stringWriter, command);
            var xml = stringWriter.ToString();
            
            // Queue xml doc to be processed
            await _taxuallyQueueClient.EnqueueAsync("vat-registration-xml", xml);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }
    }
}