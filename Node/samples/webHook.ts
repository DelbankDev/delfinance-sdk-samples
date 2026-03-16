import {
    SdkClientFactory as sdkClientFactory,
    delSdkOptions,
    webHookRequest,
} from '@delbank/del-sdk';

export async function runWebHookSample(
    options: delSdkOptions,
    getEnv: (name: string) => string
): Promise<void> {
    console.log('\n=========================================================');
    console.log(' WebHook Service');
    console.log('=========================================================');

    try {
        console.log('--- createWebHook ---');
        const client = sdkClientFactory.createServicesClient(options);

        const req: webHookRequest = {
            eventType: getEnv('WEBHOOK_EVENT_TYPE'),
            url: getEnv('WEBHOOK_URL'),
            authorization: getEnv('WEBHOOK_AUTHORIZATION'),
            authorizationScheme: 'Bearer',
        };

        const result = await client.webHookService.createWebHook(req);

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- getWebHooks ---');
        const client = sdkClientFactory.createServicesClient(options);

        const result = await client.webHookService.getWebHooks();

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- getWebHookById ---');
        const client = sdkClientFactory.createServicesClient(options);

        const result = await client.webHookService.getWebHookById(getEnv('WEBHOOK_ID'));

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- updateWebHook ---');
        const client = sdkClientFactory.createServicesClient(options);

        const req: webHookRequest = {
            id: getEnv('WEBHOOK_ID'),
            eventType: getEnv('WEBHOOK_EVENT_TYPE'),
            url: getEnv('WEBHOOK_URL'),
            authorization: getEnv('WEBHOOK_AUTHORIZATION'),
            authorizationScheme: 'Bearer',
        };

        const result = await client.webHookService.updateWebHook(req);

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- deleteWebHookById ---');
        const client = sdkClientFactory.createServicesClient(options);

        const result = await client.webHookService.deleteWebHookById(getEnv('WEBHOOK_ID'));

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }
}
