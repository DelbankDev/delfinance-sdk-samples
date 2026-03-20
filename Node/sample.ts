import 'dotenv/config';
import { sdkClientFactory } from '@delbank/delfinance-api-sdk/dist/delSdk';
import { runPixTransferSample } from './samples/pixTransfer';
import { runPixKeysSample } from './samples/pixKeys';
import { runPixQRCodeSample } from './samples/pixQRCode';
import { runWebHookSample } from './samples/webHook';
import { runBankSlipSample } from './samples/bankSlip';

async function runSample() {
    function getEnv(name: string): string {
        const value = process.env[name];
        if (!value) {
            throw new Error(`Env ${name} não definida`);
        }
        return value;
    }

    const options: any = {
        environment: 'SANDBOX',
        accessToken: getEnv('AUTH_ACCOUNT_API_KEY'),
        accountId: getEnv('AUTH_ACCOUNT_ID'),
        timeout: 30000,
    };

    console.log('--- Iniciando SDK Client com Delfinance SDK ---');
    console.log(`Conta: ${options.accountId}`);
    console.log(`Ambiente: ${options.environment}`);

    // Criar cliente do serviço
    const client = sdkClientFactory.createServicesClient(options);

    await runPixTransferSample(client, getEnv);
    await runPixKeysSample(client, getEnv);
    await runPixQRCodeSample(client, getEnv);
    await runWebHookSample(client, getEnv);
    await runBankSlipSample(client, getEnv);
}

runSample().catch(console.error);
