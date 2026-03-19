using DelSdk.Abstractions.Configurations;
using DelSdk.Abstractions.Errors;
using DelSdk.Abstractions.PixServices.Requests;
using DelfinanceSDK.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace DelfinanceSDK.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PixTransferController : DelSdkBaseController
{
    public PixTransferController(ILogger<PixTransferController> logger, IConfiguration configuration) 
        : base(logger, configuration)
    {
    }

    #region Inicialização de Pagamentos

    [HttpPost("initialize/dict")]
    public async Task<IActionResult> InitializePayment([FromBody] PaymentInitializationRequest request)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            var response = await pixClient.InitializePaymentAsync(request, CancellationToken.None);

            return Success(response, "Pagamento inicializado com sucesso");
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao inicializar pagamento");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title,
                errors = ex.ErrorResponse.Errors
            });
        }
    }

    [HttpPost("initialize/qrcode")]
    public async Task<IActionResult> InitializeQrCodePayment([FromBody] QrCodePaymentInitializationRequest request)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            var response = await pixClient.InitializeQrCodePaymentAsync(request, CancellationToken.None);

            return Success(response, "Pagamento via QR Code inicializado com sucesso");
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao inicializar pagamento via QR Code");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title,
                errors = ex.ErrorResponse.Errors
            });
        }
    }

    #endregion

    #region Transferências PIX

    [HttpPost("pix")]
    public async Task<IActionResult> CreateTransfer([FromBody] CreateTransferRequest request)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            var idempotencyKey = Guid.NewGuid().ToString();
            var response = await pixClient.CreateTransferAsync(request, idempotencyKey, CancellationToken.None);

            return Success(response, "Transferência PIX criada com sucesso");
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao criar transferência PIX");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title,
                errors = ex.ErrorResponse.Errors
            });
        }
    }

    [HttpGet("pix/{identifier}")]
    public async Task<IActionResult> GetTransfer(string identifier)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            var response = await pixClient.GetTransferAsync(identifier, CancellationToken.None);

            return Success(response);
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao consultar transferência");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title
            });
        }
    }

    #endregion

    #region Transferências TED

    [HttpPost("ted")]
    public async Task<IActionResult> CreateTedTransfer([FromBody] CreateTedTransferRequest request)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            var idempotencyKey = Guid.NewGuid().ToString();
            var response = await pixClient.CreateTedTransferAsync(request, idempotencyKey, CancellationToken.None);

            return Success(response, "Transferência TED criada com sucesso");
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao criar transferência TED");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title,
                errors = ex.ErrorResponse.Errors
            });
        }
    }

    [HttpGet("ted/{identifier}")]
    public async Task<IActionResult> GetTedTransfer(string identifier)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            var response = await pixClient.GetTedTransferAsync(identifier, CancellationToken.None);

            return Success(response);
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao consultar transferência TED");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title
            });
        }
    }

    #endregion
}
