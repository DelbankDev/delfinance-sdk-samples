// Tipos importados apenas para referência (podem não ser necessários na nova versão)
export async function runWebHookSample(
    client: any,
    getEnv: (name: string) => string
): Promise<void> {
    console.log('\n=========================================================');
    console.log(' WebHook Service');
    console.log('=========================================================');

    try {
        console.log('--- createWebHook ---');

        const req: any = {
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

        const result = await client.webHookService.getWebHooks();

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- getWebHookById ---');

        const result = await client.webHookService.getWebHookById(getEnv('WEBHOOK_ID'));

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- updateWebHook ---');

        const req: any = {
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

        const result = await client.webHookService.deleteWebHookById(getEnv('WEBHOOK_ID'));

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }
}
