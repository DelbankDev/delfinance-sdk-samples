using DelSdk.Abstractions.Configurations;
using DelSdk.Abstractions.Configurations.Dtos;
using DelSdk.Abstractions.Configurations.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DelfinanceSDK.Controllers.Base;

public abstract class DelSdkBaseController : ControllerBase
{
    protected readonly ILogger Logger;
    protected readonly IConfiguration Configuration;

    protected DelSdkBaseController(ILogger logger, IConfiguration configuration)
    {
        Logger = logger;
        Configuration = configuration;
    }

    protected DelSdkConfigurations GetDelSdkOptions()
    {
        var apiKey = Configuration["DelSdk:ApiKey"] 
            ?? Environment.GetEnvironmentVariable("AUTH_ACCOUNT_API_KEY") 
            ?? "sua-api-key";
            
        var accountId = Configuration["DelSdk:AccountId"] 
            ?? Environment.GetEnvironmentVariable("AUTH_ACCOUNT_ID") 
            ?? "sua-account-id";
            
        var envValue = Configuration["DelSdk:Environment"] 
            ?? Environment.GetEnvironmentVariable("AUTH_ENVIRONMENT") 
            ?? "Sandbox";
            
        var environment = envValue == "Production" 
            ? DelEnvironment.Production 
            : DelEnvironment.Sandbox;

        return new DelSdkConfigurations(environment, apiKey, accountId);
    }

    protected IActionResult Success(object data, string message = "Operação realizada com sucesso")
    {
        return Ok(new
        {
            success = true,
            data,
            message
        });
    }

    protected IActionResult Success(string message)
    {
        return Ok(new
        {
            success = true,
            message
        });
    }
}
