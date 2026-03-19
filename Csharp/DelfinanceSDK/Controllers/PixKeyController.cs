using DelSdk.Abstractions.Configurations;
using DelSdk.Abstractions.Errors;
using DelSdk.Abstractions.PixServices.Requests;
using DelfinanceSDK.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace DelfinanceSDK.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PixKeyController : DelSdkBaseController
{
    public PixKeyController(ILogger<PixKeyController> logger, IConfiguration configuration) 
        : base(logger, configuration)
    {
    }

    [HttpPost("generate-auth-code")]
    public async Task<IActionResult> GenerateAuthCode([FromBody] GenerateAuthCodeRequest request)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            await pixClient.GenerateAuthCodeAsync(request, CancellationToken.None);

            return Success("Código de autenticação gerado e enviado com sucesso");
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao gerar código de autenticação");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title,
                errors = ex.ErrorResponse.Errors
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePixKey(
        [FromBody] CreatePixKeyRequest request, 
        [FromQuery] string? authCode = null, 
        [FromQuery] string? authId = null)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            var idempotencyKey = Guid.NewGuid().ToString();
            var response = await pixClient.CreatePixKeyAsync(
                request, 
                idempotencyKey, 
                authCode ?? string.Empty, 
                authId ?? string.Empty, 
                CancellationToken.None);

            return Success(response, "Chave PIX criada com sucesso");
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao criar chave PIX");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title,
                errors = ex.ErrorResponse.Errors
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetPixKeys()
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            var response = await pixClient.GetPixKeysAsync(CancellationToken.None);

            return Ok(new
            {
                success = true,
                data = response,
                count = response.Count()
            });
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao listar chaves PIX");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title
            });
        }
    }

    [HttpDelete("{pixKey}")]
    public async Task<IActionResult> DeletePixKey(string pixKey)
    {
        try
        {
            var options = GetDelSdkOptions();
            var pixClient = SdkClientFactory.CreatePixServicesClient(options);

            var request = new DeletePixKeyRequest { Key = pixKey };
            var idempotencyKey = Guid.NewGuid().ToString();
            
            await pixClient.DeletePixKeyAsync(request, idempotencyKey, CancellationToken.None);

            return Success("Chave PIX deletada com sucesso");
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao deletar chave PIX");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title
            });
        }
    }
}
