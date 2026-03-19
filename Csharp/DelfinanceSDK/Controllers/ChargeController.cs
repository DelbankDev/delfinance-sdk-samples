using DelSdk.Abstractions.ChargeServices.Requests;
using DelSdk.Abstractions.Configurations;
using DelSdk.Abstractions.Errors;
using DelfinanceSDK.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace DelfinanceSDK.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChargeController : DelSdkBaseController
{
    public ChargeController(ILogger<ChargeController> logger, IConfiguration configuration) 
        : base(logger, configuration)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateCharge([FromBody] CreateChargeRequest request)
    {
        try
        {
            var options = GetDelSdkOptions();
            var chargeClient = SdkClientFactory.CreateChargeServicesClient(options);

            var response = await chargeClient.CreateChargeAsync(request, CancellationToken.None);

            return Success(response, "Cobrança criada com sucesso");
        }
        catch (ChargeApiException ex)
        {
            Logger.LogError(ex, "Erro ao criar cobrança");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title,
                traceId = ex.ErrorResponse.TraceId,
                errors = ex.ErrorResponse.Errors
            });
        }
    }

    [HttpPost("pay-bill")]
    public async Task<IActionResult> PayBill([FromBody] PayBillRequest request)
    {
        try
        {
            var options = GetDelSdkOptions();
            var chargeClient = SdkClientFactory.CreateChargeServicesClient(options);

            var idempotencyKey = Guid.NewGuid().ToString();
            var proof = await chargeClient.PayBillAsync(request, idempotencyKey, CancellationToken.None);

            var message = proof.Status == "PAID" 
                ? "Boleto pago com sucesso" 
                : "Pagamento agendado com sucesso";

            return Success(proof, message);
        }
        catch (ChargeApiException ex)
        {
            Logger.LogError(ex, "Erro ao pagar boleto");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title,
                traceId = ex.ErrorResponse.TraceId,
                errors = ex.ErrorResponse.Errors
            });
        }
    }
}
