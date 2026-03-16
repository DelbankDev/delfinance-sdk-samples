import { delEnvironment, delSdkOptions } from '@delbank/del-sdk';
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

    const options: delSdkOptions = {
        environment: delEnvironment.Sandbox,
        accessToken: getEnv('AUTH_ACCOUNT_API_KEY'),
        accountId: getEnv('AUTH_ACCOUNT_ID'),
        timeout: 30000,
    };

    console.log('--- Iniciando SDK Client ---');

    await runPixTransferSample(options, getEnv);
    await runPixKeysSample(options, getEnv);
    await runPixQRCodeSample(options, getEnv);
    await runWebHookSample(options, getEnv);
    await runBankSlipSample(options, getEnv);
}

runSample().catch(console.error);
