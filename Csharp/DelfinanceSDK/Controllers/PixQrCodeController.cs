using DelSdk.Abstractions.Configurations;
using DelSdk.Abstractions.Errors;
using DelSdk.Abstractions.PixServices.Requests;
using DelfinanceSDK.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace DelfinanceSDK.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PixQrCodeController : DelSdkBaseController
{
    public PixQrCodeController(ILogger<PixQrCodeController> logger, IConfiguration configuration) 
        : base(logger, configuration)
    {
    }

    #region QR Code Estático

    [HttpPost("static")]
    public async Task<IActionResult> CreateStaticQrCode([FromBody] CreateStaticQrCodeRequest request)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            var response = await pixClient.CreateStaticQrCodeAsync(request, CancellationToken.None);

            return Success(response, "QR Code estático criado com sucesso");
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao criar QR Code estático");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title,
                code = ex.ErrorResponse.Code,
                errors = ex.ErrorResponse.Errors
            });
        }
    }

    [HttpGet("static/{transactionId}")]
    public async Task<IActionResult> GetStaticQrCode(string transactionId)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            var response = await pixClient.GetStaticQrCodeAsync(transactionId, CancellationToken.None);

            return Success(response);
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao consultar QR Code estático");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title
            });
        }
    }

    [HttpGet("static/{transactionId}/payments")]
    public async Task<IActionResult> GetStaticQrCodePayments(string transactionId)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            var response = await pixClient.GetStaticQrCodePaymentsAsync(transactionId, CancellationToken.None);

            return Ok(new
            {
                success = true,
                data = response,
                count = response.Count()
            });
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao listar pagamentos do QR Code estático");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title
            });
        }
    }

    [HttpDelete("static/{transactionId}")]
    public async Task<IActionResult> CancelStaticQrCode(string transactionId)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            await pixClient.CancelStaticQrCodeAsync(transactionId, CancellationToken.None);

            return Success("QR Code estático cancelado com sucesso");
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao cancelar QR Code estático");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title
            });
        }
    }

    #endregion

    #region QR Code Dinâmico Imediato

    [HttpPost("dynamic-immediate")]
    public async Task<IActionResult> CreateDynamicImmediateQrCode([FromBody] CreateDynamicImmediateQrCodeRequest request)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            var response = await pixClient.CreateDynamicImmediateQrCodeAsync(request, CancellationToken.None);

            return Success(response, "QR Code dinâmico imediato criado com sucesso");
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao criar QR Code dinâmico imediato");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title,
                errors = ex.ErrorResponse.Errors
            });
        }
    }

    [HttpGet("dynamic-immediate/{correlationId}")]
    public async Task<IActionResult> GetDynamicImmediateQrCode(string correlationId)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            var response = await pixClient.GetDynamicImmediateQrCodeAsync(correlationId, CancellationToken.None);

            return Success(response);
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao consultar QR Code dinâmico imediato");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title
            });
        }
    }

    [HttpDelete("dynamic-immediate/{correlationId}")]
    public async Task<IActionResult> CancelDynamicImmediateQrCode(string correlationId)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            await pixClient.CancelDynamicImmediateQrCodeAsync(correlationId, CancellationToken.None);

            return Success("QR Code dinâmico imediato cancelado com sucesso");
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao cancelar QR Code dinâmico imediato");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title
            });
        }
    }

    #endregion

    #region QR Code Dinâmico com Vencimento

    [HttpPost("dynamic-duedate")]
    public async Task<IActionResult> CreateDynamicDueDateQrCode([FromBody] CreateDynamicDueDateQrCodeRequest request)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            var response = await pixClient.CreateDynamicDueDateQrCodeAsync(request, CancellationToken.None);

            return Success(response, "QR Code dinâmico com vencimento criado com sucesso");
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao criar QR Code dinâmico com vencimento");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title,
                errors = ex.ErrorResponse.Errors
            });
        }
    }

    [HttpGet("dynamic-duedate/{transactionId}")]
    public async Task<IActionResult> GetDynamicDueDateQrCode(string transactionId)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            var response = await pixClient.GetDynamicDueDateQrCodeAsync(transactionId, CancellationToken.None);

            return Success(response);
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao consultar QR Code dinâmico com vencimento");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title
            });
        }
    }

    [HttpDelete("dynamic-duedate/{transactionId}")]
    public async Task<IActionResult> CancelDynamicDueDateQrCode(string transactionId)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            await pixClient.CancelDynamicDueDateQrCodeAsync(transactionId, CancellationToken.None);

            return Success("QR Code dinâmico com vencimento cancelado com sucesso");
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao cancelar QR Code dinâmico com vencimento");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title
            });
        }
    }

    #endregion
}
