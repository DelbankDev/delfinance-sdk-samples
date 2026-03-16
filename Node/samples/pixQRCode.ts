import {
    SdkClientFactory as sdkClientFactory,
    delSdkOptions,
    pixQRCodeRequest,
} from '@delbank/del-sdk';

export async function runPixQRCodeSample(
    options: delSdkOptions,
    getEnv: (name: string) => string
): Promise<void> {
    console.log('\n=========================================================');
    console.log(' Pix QRCode Service');
    console.log('=========================================================');

    try {
        console.log('--- postQRCodeStatic ---');
        const client = sdkClientFactory.createServicesClient(options);

        const req: pixQRCodeRequest = {
            correlationId: getEnv('QRCODE_CORRELATION_ID'),
            amount: 50.00,
            pixKey: getEnv('PIX_KEY'),
            description: 'QRCode estático de teste via SDK',
        };

        const result = await client.pixService.postQRCodeStatic(req);

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- getQRCodeStatic ---');
        const client = sdkClientFactory.createServicesClient(options);

        const result = await client.pixService.getQRCodeStatic(getEnv('QRCODE_STATIC_ID'));

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- getQRCodeStaticPayment ---');
        const client = sdkClientFactory.createServicesClient(options);

        const result = await client.pixService.getQRCodeStaticPayment(getEnv('QRCODE_STATIC_ID'));

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- patchQRCodeCancel ---');
        const client = sdkClientFactory.createServicesClient(options);

        const result = await client.pixService.patchQRCodeCancel(getEnv('QRCODE_STATIC_ID'));

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- postQRCodeDynamicImediate ---');
        const client = sdkClientFactory.createServicesClient(options);

        const req: pixQRCodeRequest = {
            correlationId: getEnv('QRCODE_CORRELATION_ID'),
            amount: 75.00,
            pixKey: getEnv('PIX_KEY'),
            description: 'QRCode dinâmico imediato de teste via SDK',
            expiresIn: '3600',
        };

        const result = await client.pixService.postQRCodeDynamicImediate(req);

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- getQRCodeDynamicImediate ---');
        const client = sdkClientFactory.createServicesClient(options);

        const result = await client.pixService.getQRCodeDynamicImediate(getEnv('QRCODE_DYNAMIC_IMEDIATE_ID'));

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- patchQRCodeDynamicImediateCancel ---');
        const client = sdkClientFactory.createServicesClient(options);

        const result = await client.pixService.patchQRCodeDynamicImediateCancel(getEnv('QRCODE_DYNAMIC_IMEDIATE_ID'));

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- postQRCodeDynamicDueDate ---');
        const client = sdkClientFactory.createServicesClient(options);

        const req: pixQRCodeRequest = {
            correlationId: getEnv('QRCODE_CORRELATION_ID'),
            amount: 120.00,
            pixKey: getEnv('PIX_KEY'),
            dueDate: '2026-12-31',
            description: 'QRCode dinâmico com vencimento de teste via SDK',
        };

        const result = await client.pixService.postQRCodeDynamicDueDate(req);

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- getQRCodeDynamicDueDate ---');
        const client = sdkClientFactory.createServicesClient(options);

        const result = await client.pixService.getQRCodeDynamicDueDate(getEnv('QRCODE_DYNAMIC_DUEDATE_ID'));

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- patchQRCodeDynamicDueDateCancel ---');
        const client = sdkClientFactory.createServicesClient(options);

        const result = await client.pixService.patchQRCodeDynamicDueDateCancel(getEnv('QRCODE_DYNAMIC_DUEDATE_ID'));

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }
}
