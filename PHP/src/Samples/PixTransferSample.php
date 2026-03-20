<?php

declare(strict_types=1);

namespace App\Samples;

use App\ClientFactory;
use Delfinance\Transfers\Services\TransfersService;
use Delfinance\Transfers\Services\PixService;
use Delfinance\Transfers\Requests\CreateTransferRequest;
use Delfinance\Transfers\Requests\CreateTedTransferRequest;
use Delfinance\Transfers\Requests\PaymentInitializationRequest;
use Delfinance\Transfers\Requests\DecodeQrCodeRequest;

class PixTransferSample
{
    public function run(): void
    {
        echo "\n=========================================================\n";
        echo " Pix Transfer Service\n";
        echo "=========================================================\n";

        $client           = ClientFactory::create();
        $transfersService = new TransfersService($client);
        $pixService       = new PixService($client);

        // -----------------------------------------------------------
        // getTransfer
        // -----------------------------------------------------------
        try {
            echo "--- getTransfer ---\n";
            $transferId = getenv('TRANSFER_ID') ?: ClientFactory::env('END_TO_END_ID');
            echo "--- Buscando Transferência: {$transferId} ---\n";

            $result = $transfersService->getTransfer($transferId);

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // paymentInitialization (busca chave Pix)
        // -----------------------------------------------------------
        try {
            echo "--- paymentInitialization ---\n";
            $key = getenv('PIX_KEY') ?: ClientFactory::env('SOURCE_HOLDER_DOCUMENT');
            echo "--- Buscando chave: {$key} ---\n";

            $request = new PaymentInitializationRequest($key);
            $result  = $pixService->paymentInitialization($request);

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // decodeQrCode (decode de QR Code para pagamento)
        // -----------------------------------------------------------
        try {
            echo "--- decodeQrCode ---\n";
            $payload = ClientFactory::env('PAYLOAD_QR_CODE');
            echo "--- Payload QRCode: {$payload} ---\n";

            $request = new DecodeQrCodeRequest($payload);
            $result  = $pixService->decodeQrCode($request);

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // createTransfer (transferência Pix interna por conta)
        // -----------------------------------------------------------
        try {
            echo "--- createTransfer (Pix Interno) ---\n";
            $beneficiaryAccount = ClientFactory::env('BENEFICIARY_ACCOUNT');

            $amount  = (float) (getenv('AMOUNT') ?: 2);
            $request = new CreateTransferRequest(
                $amount,
                '',
                null,
                null,
                null,
                false,
                'Pix',
                null,
                $beneficiaryAccount,
                null,
                null,
                [],
                ''
            );

            echo '--- Realizando transfer: ' . json_encode($request) . " ---\n";
            $idempotencyKey = uniqid('transfer-', true);
            $result = $transfersService->createTransfer($request, $idempotencyKey);

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // createTedTransfer
        // -----------------------------------------------------------
        try {
            echo "--- createTedTransfer ---\n";
            $request = new CreateTedTransferRequest(
                25.00,
                '',
                [
                    'participantIspb' => ClientFactory::env('ISPB'),
                    'number'          => ClientFactory::env('BENEFICIARY_ACCOUNT'),
                    'branch'          => ClientFactory::env('BENEFICIARY_BRANCH'),
                    'type'            => 'CURRENT',
                    'holder'          => [
                        'document' => ClientFactory::env('BENEFICIARY_HOLDER_DOCUMENT'),
                        'name'     => ClientFactory::env('BENEFICIARY_HOLDER_NAME'),
                    ],
                ]
            );

            echo '--- Realizando TED: ' . json_encode($request) . " ---\n";
            $idempotencyKey = uniqid('ted-', true);
            $result = $transfersService->createTedTransfer($request, $idempotencyKey);

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }
    }
}
