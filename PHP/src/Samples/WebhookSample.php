<?php

declare(strict_types=1);

namespace App\Samples;

use App\ClientFactory;
use Delfinance\Webhooks\Services\WebhookService;
use Delfinance\Webhooks\Requests\CreateWebhookRequest;
use Delfinance\Webhooks\Requests\UpdateWebhookRequest;

class WebhookSample
{
    public function run(): void
    {
        echo "\n=========================================================\n";
        echo " WebHook Service\n";
        echo "=========================================================\n";

        $client         = ClientFactory::create();
        $webhookService = new WebhookService($client);

        $createdWebhookId = null;

        // -----------------------------------------------------------
        // createWebhook
        // -----------------------------------------------------------
        try {
            echo "--- createWebhook ---\n";

            $request = new CreateWebhookRequest(
                ClientFactory::env('WEBHOOK_EVENT_TYPE'),
                ClientFactory::env('WEBHOOK_URL'),
                'BEARER',
                ClientFactory::env('WEBHOOK_AUTHORIZATION')
            );

            $result = $webhookService->createWebhook($request);
            $createdWebhookId = $result->id ?? null;

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // ID a usar nas operações seguintes: criado agora ou fallback do .env
        $webhookId = $createdWebhookId ?: ClientFactory::env('WEBHOOK_ID');

        // -----------------------------------------------------------
        // getAllWebhooks
        // -----------------------------------------------------------
        try {
            echo "--- getAllWebhooks ---\n";

            $result = $webhookService->getAllWebhooks();

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // getWebhookById
        // -----------------------------------------------------------
        try {
            echo "--- getWebhookById ---\n";

            $result = $webhookService->getWebhookById($webhookId);

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // updateWebhook
        // -----------------------------------------------------------
        try {
            echo "--- updateWebhook ---\n";

            $request = new UpdateWebhookRequest(
                $webhookId,
                ClientFactory::env('WEBHOOK_EVENT_TYPE'),
                ClientFactory::env('WEBHOOK_URL'),
                'BEARER',
                ClientFactory::env('WEBHOOK_AUTHORIZATION')
            );

            $result = $webhookService->updateWebhook($request);

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // deleteWebhook
        // -----------------------------------------------------------
        try {
            echo "--- deleteWebhook ---\n";

            $result = $webhookService->deleteWebhook($webhookId);

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }
    }
}
