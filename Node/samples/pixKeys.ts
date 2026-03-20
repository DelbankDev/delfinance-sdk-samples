// Tipos importados apenas para referência (podem não ser necessários na nova versão)
export async function runPixKeysSample(
    client: any,
    getEnv: (name: string) => string
): Promise<void> {
    console.log('\n=========================================================');
    console.log(' Pix Keys Service');
    console.log('=========================================================');

    try {
        console.log('--- postCreateKey ---');

        const result = await client.pixService.postCreateKey('EVP');

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- getKeys ---');

        console.log('--- Buscando chaves ---');

        const result = await client.pixService.getKeys();

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- postAuthCreateKey ---');

        const req: any = {
            sender: 'SMS',
            receiver: '+5579986545308',
            payload: '{{code}}',
        };

        const result = await client.pixService.postAuthCreateKey(req);

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- deleteKey ---');

        const req: any = {
            key: 'key-exemple',
            entryType: 'EVP',
        };

        const result = await client.pixService.deleteKey(req);

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }
}
