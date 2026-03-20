<?php

declare(strict_types=1);

namespace App;

use Delfinance\Abstractions\Startup\DelfinanceClient;
use Delfinance\Abstractions\Enums\Environment;
use RuntimeException;

class ClientFactory
{
    public static function create(): DelfinanceClient
    {
        $apiKey      = self::env('AUTH_ACCOUNT_API_KEY');
        $accountId   = self::env('AUTH_ACCOUNT_ID');
        $environment = getenv('SDK_ENVIRONMENT') ?: (getenv('ENVIRONMENT') ?: 'sandbox');

        return new DelfinanceClient([
            'apiKey'      => $apiKey,
            'accountId'   => $accountId,
            'environment' => strtolower($environment) === 'production'
                ? Environment::PRODUCTION
                : Environment::SANDBOX,
        ]);
    }

    public static function env(string $name): string
    {
        $value = getenv($name);
        if ($value === false || $value === '') {
            throw new RuntimeException("Variável de ambiente '{$name}' não definida. Configure o arquivo .env.");
        }
        return $value;
    }
}
