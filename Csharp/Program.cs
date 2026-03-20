using System.Text.Json;
using DelSdk.Abstractions.ChargeServices.Dtos;
using DelSdk.Abstractions.ChargeServices.Requests;
using DelSdk.Abstractions.Configurations;
using DelSdk.Abstractions.Configurations.Dtos;
using DelSdk.Abstractions.Configurations.Enums;
using DelSdk.Abstractions.Errors;
using DelSdk.Abstractions.PixServices.Requests;
using DelSdk.Abstractions.Webhooks.Requests;
using DotNetEnv;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

if (File.Exists(Path.Combine(AppContext.BaseDirectory, ".env")))
{
    Env.Load(Path.Combine(AppContext.BaseDirectory, ".env"));
}
else if (File.Exists(".env"))
{
    Env.Load();
}

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(options =>
{
    options.SingleLine = true;
    options.TimestampFormat = "HH:mm:ss ";
});

builder.Services.AddSingleton(static serviceProvider =>
    DelSdkConfigurationFactory.Create(serviceProvider.GetRequiredService<IConfiguration>()));
builder.Services.AddSingleton<DelSdkConsoleApp>();

using var host = builder.Build();
await host.Services.GetRequiredService<DelSdkConsoleApp>().RunAsync();

internal sealed class DelSdkConsoleApp
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    private static readonly JsonSerializerOptions JsonInputOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly DelSdkConfigurations options;
    private readonly ILogger<DelSdkConsoleApp> logger;

    public DelSdkConsoleApp(DelSdkConfigurations options, ILogger<DelSdkConsoleApp> logger)
    {
        this.options = options;
        this.logger = logger;
    }

    public async Task RunAsync()
    {
        logger.LogInformation("Projeto carregado como aplicação console.");
        ShowConfiguration();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Selecione uma operação:");
            Console.WriteLine("1  - Mostrar configuração carregada");
            Console.WriteLine("2  - PIX: Criar QR Code estático");
            Console.WriteLine("3  - PIX: Consultar QR Code estático");
            Console.WriteLine("4  - PIX: Listar pagamentos QR Code estático");
            Console.WriteLine("5  - PIX: Cancelar QR Code estático");
            Console.WriteLine("6  - PIX: Criar QR Code dinâmico imediato");
            Console.WriteLine("7  - PIX: Consultar QR Code dinâmico imediato");
            Console.WriteLine("8  - PIX: Cancelar QR Code dinâmico imediato");
            Console.WriteLine("9  - PIX: Criar QR Code dinâmico com vencimento");
            Console.WriteLine("10 - PIX: Consultar QR Code dinâmico com vencimento");
            Console.WriteLine("11 - PIX: Cancelar QR Code dinâmico com vencimento");
            Console.WriteLine("12 - Transferência: Inicializar pagamento (DICT)");
            Console.WriteLine("13 - Transferência: Inicializar pagamento (QR Code)");
            Console.WriteLine("14 - Transferência: Criar PIX");
            Console.WriteLine("15 - Transferência: Consultar PIX");
            Console.WriteLine("16 - Transferência: Criar TED");
            Console.WriteLine("17 - Transferência: Consultar TED");
            Console.WriteLine("18 - Chaves PIX: Gerar código de autenticação");
            Console.WriteLine("19 - Chaves PIX: Criar chave");
            Console.WriteLine("20 - Chaves PIX: Listar chaves");
            Console.WriteLine("21 - Chaves PIX: Deletar chave");
            Console.WriteLine("22 - Cobrança: Criar cobrança");
            Console.WriteLine("23 - Cobrança: Pagar boleto");
            Console.WriteLine("24 - Webhook: Criar webhook");
            Console.WriteLine("25 - Webhook: Listar webhooks");
            Console.WriteLine("26 - Webhook: Consultar webhook por ID");
            Console.WriteLine("27 - Webhook: Atualizar webhook");
            Console.WriteLine("28 - Webhook: Deletar webhook");
            Console.WriteLine("0 - Sair");
            Console.Write("> ");

            var input = Console.ReadLine()?.Trim();

            switch (input)
            {
                case "1":
                    ShowConfiguration();
                    break;
                case "2":
                    await ExecuteAsync("Criar QR Code estático", CreateStaticQrCodeAsync);
                    break;
                case "3":
                    await ExecuteAsync("Consultar QR Code estático", GetStaticQrCodeAsync);
                    break;
                case "4":
                    await ExecuteAsync("Listar pagamentos do QR Code estático", GetStaticQrCodePaymentsAsync);
                    break;
                case "5":
                    await ExecuteAsync("Cancelar QR Code estático", CancelStaticQrCodeAsync);
                    break;
                case "6":
                    await ExecuteAsync("Criar QR Code dinâmico imediato", CreateDynamicImmediateQrCodeAsync);
                    break;
                case "7":
                    await ExecuteAsync("Consultar QR Code dinâmico imediato", GetDynamicImmediateQrCodeAsync);
                    break;
                case "8":
                    await ExecuteAsync("Cancelar QR Code dinâmico imediato", CancelDynamicImmediateQrCodeAsync);
                    break;
                case "9":
                    await ExecuteAsync("Criar QR Code dinâmico com vencimento", CreateDynamicDueDateQrCodeAsync);
                    break;
                case "10":
                    await ExecuteAsync("Consultar QR Code dinâmico com vencimento", GetDynamicDueDateQrCodeAsync);
                    break;
                case "11":
                    await ExecuteAsync("Cancelar QR Code dinâmico com vencimento", CancelDynamicDueDateQrCodeAsync);
                    break;
                case "12":
                    await ExecuteAsync("Inicializar pagamento DICT", InitializeDictPaymentAsync);
                    break;
                case "13":
                    await ExecuteAsync("Inicializar pagamento QR Code", InitializeQrCodePaymentAsync);
                    break;
                case "14":
                    await ExecuteAsync("Criar transferência PIX", CreatePixTransferAsync);
                    break;
                case "15":
                    await ExecuteAsync("Consultar transferência PIX", GetPixTransferAsync);
                    break;
                case "16":
                    await ExecuteAsync("Criar transferência TED", CreateTedTransferAsync);
                    break;
                case "17":
                    await ExecuteAsync("Consultar transferência TED", GetTedTransferAsync);
                    break;
                case "18":
                    await ExecuteAsync("Gerar código de autenticação de chave PIX", GeneratePixAuthCodeAsync);
                    break;
                case "19":
                    await ExecuteAsync("Criar chave PIX", CreatePixKeyAsync);
                    break;
                case "20":
                    await ExecuteAsync("Listar chaves PIX", ListPixKeysAsync);
                    break;
                case "21":
                    await ExecuteAsync("Deletar chave PIX", DeletePixKeyAsync);
                    break;
                case "22":
                    await ExecuteAsync("Criar cobrança", CreateChargeAsync);
                    break;
                case "23":
                    await ExecuteAsync("Pagar boleto", PayBillAsync);
                    break;
                case "24":
                    await ExecuteAsync("Criar webhook", CreateWebhookAsync);
                    break;
                case "25":
                    await ExecuteAsync("Listar webhooks", ListWebhooksAsync);
                    break;
                case "26":
                    await ExecuteAsync("Consultar webhook por ID", GetWebhookByIdAsync);
                    break;
                case "27":
                    await ExecuteAsync("Atualizar webhook", UpdateWebhookAsync);
                    break;
                case "28":
                    await ExecuteAsync("Deletar webhook", DeleteWebhookAsync);
                    break;
                case "0":
                    logger.LogInformation("Encerrando aplicação.");
                    return;
                default:
                    logger.LogWarning("Opção inválida: {Option}", input);
                    break;
            }
        }
    }

    private void ShowConfiguration()
    {
        WriteSection(
            "Configuração carregada",
            new
            {
                Environment = options.Environment.ToString(),
                options.AccountId,
                ApiKey = MaskSecret(options.ApiKey)
            });
    }

    private async Task ExecuteAsync(string operationName, Func<CancellationToken, Task<object?>> operation)
    {
        logger.LogInformation("Executando: {OperationName}", operationName);

        try
        {
            var result = await operation(CancellationToken.None);
            WriteSection($"Resultado: {operationName}", result);
        }
        catch (ChargeApiException ex)
        {
            LogChargeError(ex);
        }
        catch (DelSdkApiException ex)
        {
            LogDelSdkError(ex);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha inesperada ao executar {OperationName}", operationName);
        }
    }

    private async Task<object?> CreateStaticQrCodeAsync(CancellationToken cancellationToken)
    {
        var request = SampleRequests.CreateStaticQrCode();
        WriteSection("Payload enviado", request);

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        return await pixClient.CreateStaticQrCodeAsync(request, cancellationToken);
    }

    private async Task<object?> GetStaticQrCodeAsync(CancellationToken cancellationToken)
    {
        var transactionId = ReadRequiredString(
            "Informe o transactionId do QR Code estático",
            new[] { "QRCODE_TRANSACTION_ID", "QRCODE_ID" });

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        return await pixClient.GetStaticQrCodeAsync(transactionId, cancellationToken);
    }

    private async Task<object?> GetStaticQrCodePaymentsAsync(CancellationToken cancellationToken)
    {
        var transactionId = ReadRequiredString(
            "Informe o transactionId do QR Code estático",
            new[] { "QRCODE_TRANSACTION_ID", "QRCODE_ID" });

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        return await pixClient.GetStaticQrCodePaymentsAsync(transactionId, cancellationToken);
    }

    private async Task<object?> CancelStaticQrCodeAsync(CancellationToken cancellationToken)
    {
        var transactionId = ReadRequiredString(
            "Informe o transactionId do QR Code estático",
            new[] { "QRCODE_TRANSACTION_ID", "QRCODE_ID" });

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        await pixClient.CancelStaticQrCodeAsync(transactionId, cancellationToken);
        return new { Message = "QR Code estático cancelado com sucesso" };
    }

    private async Task<object?> CreateDynamicImmediateQrCodeAsync(CancellationToken cancellationToken)
    {
        var request = SampleRequests.CreateDynamicImmediateQrCode();
        WriteSection("Payload enviado", request);

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        return await pixClient.CreateDynamicImmediateQrCodeAsync(request, cancellationToken);
    }

    private async Task<object?> GetDynamicImmediateQrCodeAsync(CancellationToken cancellationToken)
    {
        var correlationId = ReadRequiredString(
            "Informe o correlationId do QR Code dinâmico imediato",
            new[] { "QRCODE_CORRELATION_ID", "END_TO_END_ID", "TRANSFER_ID" });

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        return await pixClient.GetDynamicImmediateQrCodeAsync(correlationId, cancellationToken);
    }

    private async Task<object?> CancelDynamicImmediateQrCodeAsync(CancellationToken cancellationToken)
    {
        var correlationId = ReadRequiredString(
            "Informe o correlationId do QR Code dinâmico imediato",
            new[] { "QRCODE_CORRELATION_ID", "END_TO_END_ID", "TRANSFER_ID" });

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        await pixClient.CancelDynamicImmediateQrCodeAsync(correlationId, cancellationToken);
        return new { Message = "QR Code dinâmico imediato cancelado com sucesso" };
    }

    private async Task<object?> CreateDynamicDueDateQrCodeAsync(CancellationToken cancellationToken)
    {
        var request = SampleRequests.CreateDynamicDueDateQrCode();
        WriteSection("Payload enviado", request);

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        return await pixClient.CreateDynamicDueDateQrCodeAsync(request, cancellationToken);
    }

    private async Task<object?> GetDynamicDueDateQrCodeAsync(CancellationToken cancellationToken)
    {
        var transactionId = ReadRequiredString(
            "Informe o transactionId do QR Code dinâmico com vencimento",
            new[] { "QRCODE_TRANSACTION_ID", "QRCODE_ID" });

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        return await pixClient.GetDynamicDueDateQrCodeAsync(transactionId, cancellationToken);
    }

    private async Task<object?> CancelDynamicDueDateQrCodeAsync(CancellationToken cancellationToken)
    {
        var transactionId = ReadRequiredString(
            "Informe o transactionId do QR Code dinâmico com vencimento",
            new[] { "QRCODE_TRANSACTION_ID", "QRCODE_ID" });

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        await pixClient.CancelDynamicDueDateQrCodeAsync(transactionId, cancellationToken);
        return new { Message = "QR Code dinâmico com vencimento cancelado com sucesso" };
    }

    private async Task<object?> InitializeDictPaymentAsync(CancellationToken cancellationToken)
    {
        var request = ReadRequestFromJson<PaymentInitializationRequest>(
            "Inicialização DICT",
            SampleRequests.InitializeDictPaymentJson());

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        return await pixClient.InitializePaymentAsync(request, cancellationToken);
    }

    private async Task<object?> InitializeQrCodePaymentAsync(CancellationToken cancellationToken)
    {
        var request = ReadRequestFromJson<QrCodePaymentInitializationRequest>(
            "Inicialização QR Code",
            SampleRequests.InitializeQrCodePaymentJson());

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        return await pixClient.InitializeQrCodePaymentAsync(request, cancellationToken);
    }

    private async Task<object?> CreatePixTransferAsync(CancellationToken cancellationToken)
    {
        var request = ReadRequestFromJson<CreateTransferRequest>(
            "Criação de transferência PIX",
            SampleRequests.CreatePixTransferJson());

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        var idempotencyKey = Guid.NewGuid().ToString();
        return await pixClient.CreateTransferAsync(request, idempotencyKey, cancellationToken);
    }

    private async Task<object?> GetPixTransferAsync(CancellationToken cancellationToken)
    {
        var identifier = ReadRequiredString(
            "Informe o identificador da transferência PIX",
            new[] { "TRANSFER_ID", "END_TO_END_ID" });

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        return await pixClient.GetTransferAsync(identifier, cancellationToken);
    }

    private async Task<object?> CreateTedTransferAsync(CancellationToken cancellationToken)
    {
        var request = ReadRequestFromJson<CreateTedTransferRequest>(
            "Criação de transferência TED",
            SampleRequests.CreateTedTransferJson());

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        var idempotencyKey = Guid.NewGuid().ToString();
        return await pixClient.CreateTedTransferAsync(request, idempotencyKey, cancellationToken);
    }

    private async Task<object?> GetTedTransferAsync(CancellationToken cancellationToken)
    {
        var identifier = ReadRequiredString(
            "Informe o identificador da transferência TED",
            new[] { "TRANSFER_ID", "END_TO_END_ID" });

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        return await pixClient.GetTedTransferAsync(identifier, cancellationToken);
    }

    private async Task<object?> GeneratePixAuthCodeAsync(CancellationToken cancellationToken)
    {
        var request = ReadRequestFromJson<GenerateAuthCodeRequest>(
            "Geração de código de autenticação de chave PIX",
            SampleRequests.GenerateAuthCodeJson());

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        await pixClient.GenerateAuthCodeAsync(request, cancellationToken);
        return new { Message = "Código de autenticação gerado com sucesso" };
    }

    private async Task<object?> CreatePixKeyAsync(CancellationToken cancellationToken)
    {
        var request = ReadRequestFromJson<CreatePixKeyRequest>(
            "Criação de chave PIX",
            SampleRequests.CreatePixKeyJson());

        var authCode = ReadOptionalString("AuthCode (opcional)", "PIX_KEY_AUTH_CODE");
        var authId = ReadOptionalString("AuthId (opcional)", "PIX_KEY_AUTH_ID");

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        var idempotencyKey = Guid.NewGuid().ToString();
        return await pixClient.CreatePixKeyAsync(
            request,
            idempotencyKey,
            authCode,
            authId,
            cancellationToken);
    }

    private async Task<object?> ListPixKeysAsync(CancellationToken cancellationToken)
    {
        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        return await pixClient.GetPixKeysAsync(cancellationToken);
    }

    private async Task<object?> DeletePixKeyAsync(CancellationToken cancellationToken)
    {
        var key = ReadRequiredString("Informe a chave PIX para remover", new[] { "PIX_KEY" });
        var request = new DeletePixKeyRequest { Key = key };

        var pixClient = SdkClientFactory.CreatePixServicesClient(options);
        var idempotencyKey = Guid.NewGuid().ToString();
        await pixClient.DeletePixKeyAsync(request, idempotencyKey, cancellationToken);
        return new { Message = "Chave PIX removida com sucesso" };
    }

    private async Task<object?> CreateChargeAsync(CancellationToken cancellationToken)
    {
        var request = SampleRequests.CreateCharge();
        WriteSection("Payload enviado", request);

        var chargeClient = SdkClientFactory.CreateChargeServicesClient(options);
        return await chargeClient.CreateChargeAsync(request, cancellationToken);
    }

    private async Task<object?> PayBillAsync(CancellationToken cancellationToken)
    {
        var request = ReadRequestFromJson<PayBillRequest>(
            "Pagamento de boleto",
            SampleRequests.PayBillJson());

        var chargeClient = SdkClientFactory.CreateChargeServicesClient(options);
        var idempotencyKey = Guid.NewGuid().ToString();
        return await chargeClient.PayBillAsync(request, idempotencyKey, cancellationToken);
    }

    private async Task<object?> CreateWebhookAsync(CancellationToken cancellationToken)
    {
        var request = ReadRequestFromJson<CreateWebhookRequest>(
            "Criação de webhook",
            SampleRequests.CreateWebhookJson());

        var webhookClient = SdkClientFactory.CreateWebhookClient(options);
        return await webhookClient.CreateWebhookAsync(request, cancellationToken);
    }

    private async Task<object?> ListWebhooksAsync(CancellationToken cancellationToken)
    {
        var webhookClient = SdkClientFactory.CreateWebhookClient(options);
        return await webhookClient.GetWebhooksAsync(cancellationToken);
    }

    private async Task<object?> GetWebhookByIdAsync(CancellationToken cancellationToken)
    {
        var webhookId = ReadRequiredInt("Informe o ID do webhook", "WEBHOOK_ID");

        var webhookClient = SdkClientFactory.CreateWebhookClient(options);
        return await webhookClient.GetWebhookByIdAsync(webhookId, cancellationToken);
    }

    private async Task<object?> UpdateWebhookAsync(CancellationToken cancellationToken)
    {
        var webhookId = ReadRequiredInt("Informe o ID do webhook", "WEBHOOK_ID");
        var request = ReadRequestFromJson<CreateWebhookRequest>(
            "Atualização de webhook",
            SampleRequests.CreateWebhookJson());

        var webhookClient = SdkClientFactory.CreateWebhookClient(options);
        return await webhookClient.UpdateWebhookAsync(webhookId, request, cancellationToken);
    }

    private async Task<object?> DeleteWebhookAsync(CancellationToken cancellationToken)
    {
        var webhookId = ReadRequiredInt("Informe o ID do webhook", "WEBHOOK_ID");

        var webhookClient = SdkClientFactory.CreateWebhookClient(options);
        await webhookClient.DeleteWebhookAsync(webhookId, cancellationToken);
        return new { Message = "Webhook removido com sucesso" };
    }

    private T ReadRequestFromJson<T>(string requestTitle, string defaultJson)
    {
        Console.WriteLine();
        Console.WriteLine($"{requestTitle}: cole um JSON completo ou pressione ENTER para usar o padrão.");
        Console.Write("JSON> ");
        var jsonInput = Console.ReadLine();
        var json = string.IsNullOrWhiteSpace(jsonInput) ? defaultJson : jsonInput;

        var payloadEcho = JsonSerializer.Deserialize<JsonElement>(json, JsonInputOptions);
        WriteSection("Payload enviado", payloadEcho);

        var request = JsonSerializer.Deserialize<T>(json, JsonInputOptions);
        if (request is null)
        {
            throw new InvalidOperationException("Não foi possível desserializar o JSON informado.");
        }

        return request;
    }

    private static string ReadRequiredString(string prompt, IEnumerable<string> environmentKeys)
    {
        var fallback = environmentKeys
            .Select(Environment.GetEnvironmentVariable)
            .FirstOrDefault(value => !string.IsNullOrWhiteSpace(value));

        Console.Write($"{prompt}{(string.IsNullOrWhiteSpace(fallback) ? string.Empty : $" [{fallback}]")}: ");
        var input = Console.ReadLine()?.Trim();

        var value = string.IsNullOrWhiteSpace(input) ? fallback : input;

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException("Valor obrigatório não informado.");
        }

        return value;
    }

    private static string ReadOptionalString(string prompt, string environmentKey)
    {
        var fallback = Environment.GetEnvironmentVariable(environmentKey);
        Console.Write($"{prompt}{(string.IsNullOrWhiteSpace(fallback) ? string.Empty : $" [{fallback}]")}: ");
        var input = Console.ReadLine()?.Trim();

        return string.IsNullOrWhiteSpace(input)
            ? fallback ?? string.Empty
            : input;
    }

    private static int ReadRequiredInt(string prompt, string environmentKey)
    {
        var fallback = Environment.GetEnvironmentVariable(environmentKey);

        while (true)
        {
            Console.Write($"{prompt}{(string.IsNullOrWhiteSpace(fallback) ? string.Empty : $" [{fallback}]")}: ");
            var input = Console.ReadLine()?.Trim();
            var value = string.IsNullOrWhiteSpace(input) ? fallback : input;

            if (int.TryParse(value, out var parsed))
            {
                return parsed;
            }

            Console.WriteLine("Valor inválido. Digite um número inteiro.");
        }
    }

    private void LogChargeError(ChargeApiException ex)
    {
        logger.LogError(ex, "A API de cobranças retornou erro: {Title}", ex.ErrorResponse.Title);
        WriteSection(
            "Erro da API de cobranças",
            new
            {
                ex.ErrorResponse.Title,
                ex.ErrorResponse.TraceId,
                ex.ErrorResponse.Errors,
                StatusCode = (int)ex.StatusCode
            });
    }

    private void LogDelSdkError(DelSdkApiException ex)
    {
        logger.LogError(ex, "A API Delfinance retornou erro: {Title}", ex.ErrorResponse.Title);
        WriteSection(
            "Erro da API Delfinance",
            new
            {
                ex.ErrorResponse.Title,
                ex.ErrorResponse.Code,
                ex.ErrorResponse.Errors,
                StatusCode = (int)ex.StatusCode
            });
    }

    private static void WriteSection(string title, object? data)
    {
        Console.WriteLine();
        Console.WriteLine($"=== {title} ===");
        Console.WriteLine(JsonSerializer.Serialize(data, JsonOptions));
    }

    private static string MaskSecret(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        if (value.Length <= 4)
        {
            return new string('*', value.Length);
        }

        return string.Concat(new string('*', value.Length - 4), value[^4..]);
    }
}

internal static class DelSdkConfigurationFactory
{
    public static DelSdkConfigurations Create(IConfiguration configuration)
    {
        var apiKey = GetString(
                "AUTH_ACCOUNT_API_KEY",
                "DelSdk:ApiKey",
                configuration)
            ?? "sua-api-key";

        var accountId = GetString(
                "AUTH_ACCOUNT_ID",
                "DelSdk:AccountId",
                configuration)
            ?? "sua-account-id";

        var environmentValue = GetString(
                new[] { "SDK_ENVIRONMENT", "AUTH_ENVIRONMENT" },
                "DelSdk:Environment",
                configuration)
            ?? "Sandbox";

        var environment = string.Equals(environmentValue, "Production", StringComparison.OrdinalIgnoreCase)
            ? DelEnvironment.Production
            : DelEnvironment.Sandbox;

        return new DelSdkConfigurations(environment, apiKey, accountId);
    }

    private static string? GetString(string environmentVariable, string configurationKey, IConfiguration configuration)
    {
        return GetString(new[] { environmentVariable }, configurationKey, configuration);
    }

    private static string? GetString(IEnumerable<string> environmentVariables, string configurationKey, IConfiguration configuration)
    {
        foreach (var environmentVariable in environmentVariables)
        {
            var value = Environment.GetEnvironmentVariable(environmentVariable);

            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
        }

        return configuration[configurationKey];
    }
}

internal static class SampleRequests
{
    public static CreateStaticQrCodeRequest CreateStaticQrCode() => new()
    {
        CorrelationId = Guid.NewGuid().ToString(),
        Amount = EnvReader.GetDecimal("QRCODE_AMOUNT", 150.00m),
        AdditionalInfo = EnvReader.GetString("QRCODE_ADDITIONAL_VALUE", "Pagamento de mensalidade")
    };

    public static CreateDynamicImmediateQrCodeRequest CreateDynamicImmediateQrCode() => new()
    {
        CorrelationId = Guid.NewGuid().ToString(),
        Amount = EnvReader.GetDecimal("QRCODE_AMOUNT", 99.90m),
        ExpiresIn = EnvReader.GetString("QRCODE_EXPIRES_IN", "3600")
    };

    public static CreateDynamicDueDateQrCodeRequest CreateDynamicDueDateQrCode() => new()
    {
        CorrelationId = Guid.NewGuid().ToString(),
        Amount = EnvReader.GetDecimal("QRCODE_AMOUNT", 99.90m),
        DueDate = EnvReader.GetDateTime("QRCODE_DUE_DATE", DateTime.Now.AddDays(10))
    };

    public static CreateChargeRequest CreateCharge() => new()
    {
        DueDate = EnvReader.GetDateTime("QRCODE_DUE_DATE", DateTime.Now.AddDays(30)),
        Amount = EnvReader.GetDecimal("AMOUNT", 500.00m),
        Description = EnvReader.GetString("QRCODE_ADDITIONAL_VALUE", "Pagamento de mensalidade"),
        YourNumber = EnvReader.GetString("CHARGE_YOUR_NUMBER", "12345"),
        Payer = new ChargePayerDto
        {
            Name = EnvReader.GetString("SOURCE_HOLDER_NAME", "João Silva"),
            Document = EnvReader.GetString("SOURCE_HOLDER_DOCUMENT", "12345678901"),
            Email = EnvReader.GetString("SOURCE_HOLDER_EMAIL", "joao@example.com"),
            Phone = new ChargePhoneDto
            {
                Prefix = EnvReader.GetString("SOURCE_ACCOUNT_BRANCH", "11"),
                Number = EnvReader.GetString("SOURCE_ACCOUNT_NUMBER", "999999999")
            },
            Address = new ChargePayerAddressDto
            {
                ZipCode = EnvReader.GetString("QRCODE_ZIP_CODE", "01310-100"),
                PublicPlace = EnvReader.GetString("QRCODE_PUBLIC_PLACE", "Av. Paulista"),
                Number = EnvReader.GetString("QRCODE_NUMBER", "1000"),
                Neighborhood = EnvReader.GetString("QRCODE_NEIGHBORHOOD", "Bela Vista"),
                City = EnvReader.GetString("QRCODE_CITY_NAME", "São Paulo"),
                State = EnvReader.GetString("QRCODE_UF", "SP")
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

    public static string InitializeDictPaymentJson() => $$"""
{
  "pixKey": "{{EnvReader.GetString("PIX_KEY", "12345678901")}}",
  "amount": {{EnvReader.GetDecimal("AMOUNT", 1m).ToString(System.Globalization.CultureInfo.InvariantCulture)}}
}
""";

    public static string InitializeQrCodePaymentJson() => $$"""
{
  "emvQrCode": "{{EnvReader.GetString("PAYLOAD_QR_CODE", "000201...")}}",
  "amount": {{EnvReader.GetDecimal("QRCODE_AMOUNT", 1m).ToString(System.Globalization.CultureInfo.InvariantCulture)}}
}
""";

    public static string CreatePixTransferJson() => $$"""
{
  "endToEndId": "{{EnvReader.GetString("END_TO_END_ID", "E00000000000000000000000000000")}}",
  "amount": {{EnvReader.GetDecimal("AMOUNT", 1m).ToString(System.Globalization.CultureInfo.InvariantCulture)}},
  "description": "Transferência PIX via sample console"
}
""";

    public static string CreateTedTransferJson() => $$"""
{
  "amount": {{EnvReader.GetDecimal("AMOUNT", 1m).ToString(System.Globalization.CultureInfo.InvariantCulture)}},
  "description": "Transferência TED via sample console",
  "beneficiary": {
    "name": "{{EnvReader.GetString("BENEFICIARY_HOLDER_NAME", "Favorecido Teste")}}",
    "document": "{{EnvReader.GetString("BENEFICIARY_HOLDER_DOCUMENT", "00000000000")}}",
    "branch": "{{EnvReader.GetString("BENEFICIARY_BRANCH", "0001")}}",
    "account": "{{EnvReader.GetString("BENEFICIARY_ACCOUNT", "12345")}}",
    "ispb": "{{EnvReader.GetString("ISPB", "00000000")}}"
  }
}
""";

    public static string GenerateAuthCodeJson() => $$"""
{
  "entryType": "EMAIL",
  "entry": "{{EnvReader.GetString("SOURCE_HOLDER_EMAIL", "email@exemplo.com")}}"
}
""";

    public static string CreatePixKeyJson() => """
{
  "entryType": "EVP"
}
""";

    public static string PayBillJson() => $$"""
{
  "barCode": "{{EnvReader.GetString("BAR_CODE", "00000000000000000000000000000000000000000000")}}",
  "amount": {{EnvReader.GetDecimal("AMOUNT", 1m).ToString(System.Globalization.CultureInfo.InvariantCulture)}}
}
""";

    public static string CreateWebhookJson() => $$"""
{
  "url": "{{EnvReader.GetString("WEBHOOK_URL", "https://seu-endpoint.com/webhook")}}",
  "events": [
    "PIX_RECEIVED"
  ]
}
""";
}

internal static class EnvReader
{
    public static string GetString(string key, string fallback)
    {
        return Environment.GetEnvironmentVariable(key) ?? fallback;
    }

    public static decimal GetDecimal(string key, decimal fallback)
    {
        var value = Environment.GetEnvironmentVariable(key);

        if (decimal.TryParse(value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out var parsed))
        {
            return parsed;
        }

        if (decimal.TryParse(value, out parsed))
        {
            return parsed;
        }

        return fallback;
    }

    public static DateTime GetDateTime(string key, DateTime fallback)
    {
        var value = Environment.GetEnvironmentVariable(key);

        if (DateTime.TryParse(value, out var parsed))
        {
            return parsed;
        }

        return fallback;
    }
}
