using DelSdk.Abstractions.Configurations;
using DelSdk.Abstractions.Errors;
using DelSdk.Abstractions.Webhooks.Requests;
using DelfinanceSDK.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace DelfinanceSDK.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WebhookController : DelSdkBaseController
{
    public WebhookController(ILogger<WebhookController> logger, IConfiguration configuration) 
        : base(logger, configuration)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateWebhook([FromBody] CreateWebhookRequest request)
    {
        try
        {
            var options = GetDelSdkOptions();
            var webhookClient = SdkClientFactory.CreateWebhookClient(options);

            var response = await webhookClient.CreateWebhookAsync(request, CancellationToken.None);

            return Success(response, "Webhook inscrito com sucesso");
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao inscrever webhook");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title,
                errors = ex.ErrorResponse.Errors
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetWebhooks()
    {
        try
        {
            var options = GetDelSdkOptions();
            var webhookClient = SdkClientFactory.CreateWebhookClient(options);

            var response = await webhookClient.GetWebhooksAsync(CancellationToken.None);

            return Ok(new
            {
                success = true,
                data = response,
                count = response.Count
            });
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao listar webhooks");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWebhookById(int id)
    {
        try
        {
            var options = GetDelSdkOptions();
            var webhookClient = SdkClientFactory.CreateWebhookClient(options);

            var response = await webhookClient.GetWebhookByIdAsync(id, CancellationToken.None);

            return Success(response);
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao consultar webhook");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title
            });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWebhook(int id, [FromBody] CreateWebhookRequest request)
    {
        try
        {
            var options = GetDelSdkOptions();
            var webhookClient = SdkClientFactory.CreateWebhookClient(options);

            var response = await webhookClient.UpdateWebhookAsync(id, request, CancellationToken.None);

            return Success(response, "Webhook atualizado com sucesso");
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao atualizar webhook");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title,
                errors = ex.ErrorResponse.Errors
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWebhook(int id)
    {
        try
        {
            var options = GetDelSdkOptions();
            var webhookClient = SdkClientFactory.CreateWebhookClient(options);

            await webhookClient.DeleteWebhookAsync(id, CancellationToken.None);

            return Success("Webhook removido com sucesso");
        }
        catch (DelSdkApiException ex)
        {
            Logger.LogError(ex, "Erro ao remover webhook");
            return StatusCode((int)ex.StatusCode, new
            {
                success = false,
                error = ex.ErrorResponse.Title
            });
        }
    }
}
