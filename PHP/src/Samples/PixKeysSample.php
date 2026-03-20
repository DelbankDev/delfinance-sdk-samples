<?php

declare(strict_types=1);

namespace App\Samples;

use App\ClientFactory;
use Delfinance\Transfers\Services\PixService;
use Delfinance\Transfers\Requests\CreatePixKeyRequest;
use Delfinance\Transfers\Requests\DeletePixKeyRequest;
use Delfinance\Transfers\Requests\GenerateAuthCodeRequest;

class PixKeysSample
{
    public function run(): void
    {
        echo "\n=========================================================\n";
        echo " Pix Keys Service\n";
        echo "=========================================================\n";

        $client     = ClientFactory::create();
        $pixService = new PixService($client);

        // -----------------------------------------------------------
        // createPixKey (EVP - chave aleatória)
        // -----------------------------------------------------------
        try {
            echo "--- createPixKey (EVP) ---\n";

            $request        = new CreatePixKeyRequest('EVP');
            $idempotencyKey = uniqid('pixkey-', true);
            $result         = $pixService->createPixKey($request, $idempotencyKey);

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // getPixKeys
        // -----------------------------------------------------------
        try {
            echo "--- getPixKeys ---\n";
            echo "--- Buscando chaves ---\n";

            $result = $pixService->getPixKeys();

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // generateAuthCode (para criação de chave EMAIL/PHONE)
        // -----------------------------------------------------------
        try {
            echo "--- generateAuthCode ---\n";

            $receiver = ClientFactory::env('AUTH_RECEIVER');
            $request  = new GenerateAuthCodeRequest(
                'SMS',
                $receiver,
                'Seu código de autenticação é: {{code}}'
            );

            $result = $pixService->generateAuthCode($request);

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // deletePixKey
        // -----------------------------------------------------------
        try {
            echo "--- deletePixKey ---\n";

            $keyToDelete    = ClientFactory::env('PIX_KEY_TO_DELETE');
            $request        = new DeletePixKeyRequest('EVP', $keyToDelete);
            $idempotencyKey = uniqid('delpixkey-', true);
            $result         = $pixService->deletePixKey($request, $idempotencyKey);

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }
    }
}
