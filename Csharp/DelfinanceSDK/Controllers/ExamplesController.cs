using DelSdk.Abstractions.ChargeServices.Dtos;
using DelSdk.Abstractions.ChargeServices.Requests;
using DelSdk.Abstractions.PixServices.Requests;
using DelfinanceSDK.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace DelfinanceSDK.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExamplesController : DelSdkBaseController
{
    public ExamplesController(ILogger<ExamplesController> logger, IConfiguration configuration) 
        : base(logger, configuration)
    {
    }

    [HttpGet("static-qrcode")]
    public IActionResult GetStaticQrCodeExample()
    {
        var example = new CreateStaticQrCodeRequest
        {
            CorrelationId = Guid.NewGuid().ToString(),
            Amount = 150.00m,
            AdditionalInfo = "Pagamento de mensalidade"
        };

        return Ok(new
        {
            endpoint = "/api/PixQrCode/static",
            method = "POST",
            body = example,
            description = "Exemplo de criação de QR Code estático com valor fixo"
        });
    }

    [HttpGet("dynamic-immediate-qrcode")]
    public IActionResult GetDynamicImmediateQrCodeExample()
    {
        var example = new CreateDynamicImmediateQrCodeRequest
        {
            CorrelationId = Guid.NewGuid().ToString(),
            Amount = 99.90m,
            ExpiresIn = "3600"
        };

        return Ok(new
        {
            endpoint = "/api/PixQrCode/dynamic-immediate",
            method = "POST",
            body = example,
            description = "Exemplo de criação de QR Code dinâmico imediato com expiração de 1 hora"
        });
    }

    [HttpGet("dynamic-duedate-qrcode")]
    public IActionResult GetDynamicDueDateQrCodeExample()
    {
        var example = new CreateDynamicDueDateQrCodeRequest
        {
            CorrelationId = Guid.NewGuid().ToString(),
            Amount = 250.00m,
            DueDate = DateTime.Now.AddDays(30)
        };

        return Ok(new
        {
            endpoint = "/api/PixQrCode/dynamic-duedate",
            method = "POST",
            body = example,
            description = "Exemplo de criação de QR Code com vencimento, multa e juros"
        });
    }

    [HttpGet("create-charge")]
    public IActionResult GetCreateChargeExample()
    {
        var example = new CreateChargeRequest
        {
            DueDate = DateTime.Now.AddDays(30),
            Amount = 500.00m,
            Description = "Pagamento de mensalidade",
            YourNumber = "12345",
            Payer = new ChargePayerDto
            {
                Name = "João Silva",
                Document = "12345678901",
                Email = "joao@example.com",
                Phone = new ChargePhoneDto
                {
                    Prefix = "11",
                    Number = "999999999"
                },
                Address = new ChargePayerAddressDto
                {
                    ZipCode = "01310-100",
                    PublicPlace = "Av. Paulista",
                    Number = "1000",
                    Neighborhood = "Bela Vista",
                    City = "São Paulo",
                    State = "SP"
                }
            },
            LateFine = new ChargeFeeDto
            {
                Type = "Percentage",
                Amount = 2.0m,
                Date = DateTime.Now.AddDays(31)
            },
            LatePayment = new ChargeFeeDto
            {
                Type = "Percentage",
                Amount = 0.033m,
                Date = DateTime.Now.AddDays(31)
            },
            NotifyPayerOfCreation = true
        };

        return Ok(new
        {
            endpoint = "/api/Charge",
            method = "POST",
            body = example,
            description = "Exemplo completo de criação de cobrança (boleto) com dados do pagador"
        });
    }
}
