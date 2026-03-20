<?php

declare(strict_types=1);

namespace App\Samples;

use App\ClientFactory;
use Delfinance\QrCode\Services\QrCodeService;
use Delfinance\QrCode\Requests\StaticQrCodeRequest;
use Delfinance\QrCode\Requests\ImmediateQrCodeRequest;
use Delfinance\QrCode\Requests\DueDateQrCodeRequest;
use Delfinance\QrCode\Dto\AddressDTO;

class PixQrCodeSample
{
    public function run(): void
    {
        echo "\n=========================================================\n";
        echo " Pix QRCode Service\n";
        echo "=========================================================\n";

        $client        = ClientFactory::create();
        $qrCodeService = new QrCodeService($client);

        // -----------------------------------------------------------
        // createStaticQrCode
        // -----------------------------------------------------------
        try {
            echo "--- createStaticQrCode ---\n";

            $request = new StaticQrCodeRequest(
                ClientFactory::env('QRCODE_CORRELATION_ID'),
                (float) (getenv('QRCODE_AMOUNT') ?: 50),
                getenv('PIX_KEY') ?: null,
                null,
                null,
                null,
                'Teste SDK'
            );

            $result = $qrCodeService->createStaticQrCode($request);

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // getStaticQrCode
        // -----------------------------------------------------------
        try {
            echo "--- getStaticQrCode ---\n";

            $result = $qrCodeService->getStaticQrCode(ClientFactory::env('QRCODE_STATIC_ID'));

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // getStaticQrCodePayments
        // -----------------------------------------------------------
        try {
            echo "--- getStaticQrCodePayments ---\n";

            $result = $qrCodeService->getStaticQrCodePayments(ClientFactory::env('QRCODE_STATIC_ID'));

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // cancelStaticQrCode
        // -----------------------------------------------------------
        try {
            echo "--- cancelStaticQrCode ---\n";

            $result = $qrCodeService->cancelStaticQrCode(ClientFactory::env('QRCODE_STATIC_ID'));

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // createImmediateQrCode (QR Code dinâmico imediato)
        // -----------------------------------------------------------
        try {
            echo "--- createImmediateQrCode ---\n";

            $request = new ImmediateQrCodeRequest(
                ClientFactory::env('QRCODE_CORRELATION_ID'),
                (float) (getenv('QRCODE_AMOUNT') ?: 75),
                getenv('PIX_KEY') ?: null,
                null,
                null,
                null,
                getenv('QRCODE_FORMAT_RESPONSE') ?: 'ONLY_PAYLOAD',
                null,
                getenv('QRCODE_EXPIRES_IN') ?: '3600'
            );

            $result = $qrCodeService->createImmediateQrCode($request);

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // getImmediateQrCode
        // -----------------------------------------------------------
        try {
            echo "--- getImmediateQrCode ---\n";

            $result = $qrCodeService->getImmediateQrCode(ClientFactory::env('QRCODE_DYNAMIC_IMEDIATE_ID'));

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // cancelImmediateQrCode
        // -----------------------------------------------------------
        try {
            echo "--- cancelImmediateQrCode ---\n";

            $result = $qrCodeService->cancelImmediateQrCode(ClientFactory::env('QRCODE_DYNAMIC_IMEDIATE_ID'));

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // createDueDateQrCode (QR Code dinâmico com vencimento)
        // -----------------------------------------------------------
        try {
            echo "--- createDueDateQrCode ---\n";

            $request = new DueDateQrCodeRequest(
                ClientFactory::env('QRCODE_CORRELATION_ID'),
                (float) (getenv('QRCODE_AMOUNT') ?: 120),
                getenv('PIX_KEY') ?: null,
                ['name' => getenv('SOURCE_HOLDER_NAME') ?: ClientFactory::env('PAYER_NAME'), 'document' => getenv('SOURCE_HOLDER_DOCUMENT') ?: ClientFactory::env('PAYER_DOCUMENT')],
                new AddressDTO(
                    getenv('QRCODE_CITY_NAME') ?: 'ARACAJU',
                    getenv('QRCODE_ZIP_CODE') ?: '49060000',
                    getenv('QRCODE_UF') ?: 'SE',
                    getenv('QRCODE_UF') ?: 'SE',
                    getenv('QRCODE_PUBLIC_PLACE') ?: 'Rua Teste'
                ),
                getenv('QRCODE_DUE_DATE') ?: '2026-12-31'
            );

            $result = $qrCodeService->createDueDateQrCode($request);

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // getDueDateQrCode
        // -----------------------------------------------------------
        try {
            echo "--- getDueDateQrCode ---\n";

            $result = $qrCodeService->getDueDateQrCode(ClientFactory::env('QRCODE_DYNAMIC_DUEDATE_ID'));

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }

        // -----------------------------------------------------------
        // cancelDueDateQrCode
        // -----------------------------------------------------------
        try {
            echo "--- cancelDueDateQrCode ---\n";

            $result = $qrCodeService->cancelDueDateQrCode(ClientFactory::env('QRCODE_DYNAMIC_DUEDATE_ID'));

            echo 'Sucesso: ' . json_encode($result, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE) . "\n";
        } catch (\Exception $e) {
            echo "Erro ao testar o SDK:\n";
            echo "Mensagem: {$e->getMessage()}\n";
        }
    }
}
