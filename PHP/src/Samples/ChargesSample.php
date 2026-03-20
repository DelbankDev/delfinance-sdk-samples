<?php

declare(strict_types=1);

namespace App\Samples;

use App\ClientFactory;
use Delfinance\Charges\Services\ChargesService;
use Delfinance\Charges\Requests\CreateChargeRequest;
use Delfinance\Charges\Requests\UpdateChargeRequest;
use Delfinance\Charges\Requests\MakePaymentRequest;
use Delfinance\Charges\Dto\Payer;
use Delfinance\Charges\Dto\Phone;
use Delfinance\Charges\Dto\Address;

class ChargesSample
{
    public function run(): void
    {
        echo "\n=========================================================\n";
        echo " BankSlip (Charges) Service\n";
        echo "=========================================================\n";

        $client         = ClientFactory::create();
        $chargesService = new ChargesService($client);

        // -----------------------------------------------------------
        // createCharge (boleto)
        // -----------------------------------------------------------
        try {
            echo "--- createCharge ---\n";

            $address = new Address(
                '01310100',
                'Av. Paulista',
                'Bela Vista',
                '1000',
                '',
                'São Paulo',
                'SP'
            );

            $phone = new Phone('11', '900000000');

            $payer = new Payer(
                getenv('SOURCE_HOLDER_NAME') ?: ClientFactory::env('PAYER_NAME'),
                getenv('SOURCE_HOLDER_DOCUMENT') ?: ClientFactory::env('PAYER_DOCUMENT'),
                getenv('SOURCE_HOLDER_EMAIL') ?: 'pagador@example.com',
                $phone,
                $address
            );

            $yourNumber = getenv('CHARGE_YOUR_NUMBER') ?: '000001';
            $request = new CreateChargeRequest(
                ClientFactory::env('BANKSLIP_CORRELATION_ID'),
                $yourNumber,
                '2026-12-31',
                100.00,
                $payer
            );

            $result = $chargesService->createCharge($request);

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // getCharge
        // -----------------------------------------------------------
        try {
            echo "--- getCharge ---\n";

            $result = $chargesService->getCharge(ClientFactory::env('BANKSLIP_CORRELATION_ID'));

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // getCharges (listagem com filtros)
        // -----------------------------------------------------------
        try {
            echo "--- getCharges ---\n";

            $result = $chargesService->getCharges('2026-01-01', '2026-12-31', 1, 10);

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // updateCharge
        // -----------------------------------------------------------
        try {
            echo "--- updateCharge ---\n";

            $request = new UpdateChargeRequest('2027-01-31');
            $result  = $chargesService->updateCharge(
                ClientFactory::env('BANKSLIP_CORRELATION_ID'),
                $request
            );

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // voidCharge (baixa / cancelamento)
        // -----------------------------------------------------------
        try {
            echo "--- voidCharge ---\n";

            $result = $chargesService->voidCharge(ClientFactory::env('BANKSLIP_CORRELATION_ID'));

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // getPaymentInfo (consulta boleto pelo código de barras)
        // -----------------------------------------------------------
        try {
            echo "--- getPaymentInfo ---\n";

            $result = $chargesService->getPaymentInfo(ClientFactory::env('BANKSLIP_BARCODE'));

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // makePayment (pagamento de boleto)
        // -----------------------------------------------------------
        try {
            echo "--- makePayment ---\n";

            $request                = new MakePaymentRequest();
            $request->amount        = 100.00;
            $request->digitableLine = ClientFactory::env('BANKSLIP_DIGITABLE_LINE');

            $idempotencyKey = uniqid('payment-', true);
            $result         = $chargesService->makePayment($request, $idempotencyKey);

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }
    }
}
