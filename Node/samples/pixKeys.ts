import type { deleteKeyRequest } from '@delbank/del-sdk/dist/shared/common/interfaces/pix';
import {
    SdkClientFactory as sdkClientFactory,
    delSdkOptions,
    entriesType,
    authEntriesRequest,
    authEntriesType,
} from '@delbank/del-sdk';

export async function runPixKeysSample(
    options: delSdkOptions,
    getEnv: (name: string) => string
): Promise<void> {
    console.log('\n=========================================================');
    console.log(' Pix Keys Service');
    console.log('=========================================================');

    try {
        console.log('--- postCreateKey ---');
        const client = sdkClientFactory.createServicesClient(options);

        const result = await client.pixService.postCreateKey(entriesType.EVP);

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- getKeys ---');
        const client = sdkClientFactory.createServicesClient(options);

        console.log('--- Buscando chaves ---');

        const result = await client.pixService.getKeys();

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- postAuthCreateKey ---');
        const client = sdkClientFactory.createServicesClient(options);

        const req: authEntriesRequest = {
            sender: authEntriesType.SMS,
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
        const client = sdkClientFactory.createServicesClient(options);

        const req: deleteKeyRequest = {
            key: 'key-exemple',
            entryType: entriesType.EVP,
        };

        const result = await client.pixService.deleteKey(req);

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }
}
