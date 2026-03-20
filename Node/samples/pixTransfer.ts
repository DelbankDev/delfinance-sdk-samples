// Tipos importados apenas para referência (podem não ser necessários na nova versão)
export async function runPixTransferSample(
    client: any,
    getEnv: (name: string) => string
): Promise<void> {
    console.log('\n=========================================================');
    console.log(' Pix Transfer Service');
    console.log('=========================================================');

    try {
        console.log('--- getTransfer ---');

        const transferId = getEnv('END_TO_END_ID');
        console.log(`--- Buscando Transferência: ${transferId} ---`);

        const result = await client.pixService.getTransfer(transferId);

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- postPaymentInitalization ---');

        const key = {
            key: getEnv('PIX_KEY'),
            // informar holderDocument em caso de chave document
        };
        console.log(`--- Buscando chave: ${JSON.stringify(key)} ---`);

        const result = await client.pixService.postPaymentInitalization(key);

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- postQRCodePaymentInitalization ---');

        const payload = getEnv('PAYLOAD_QR_CODE');
        console.log(`--- Payload QRCode: ${payload} ---`);

        const result = await client.pixService.postQRCodePaymentInitalization(payload);

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- postInternalTransfer ---');

        const req: any = {
            beneficiaryAccount: getEnv('BENEFICIARY_ACCOUNT'),
            amount: 25,
            description: '',
            externalId: '',
        };
        console.log(`--- Realizando transfer: ${JSON.stringify(req)} ---`);

        const result = await client.pixService.postInternalTransfer(req);

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- postTEDTransfer ---');

        const req: any = {
            amount: 25,
            description: '',
            beneficiary: {
                participantIspb: getEnv('ISPB'),
                number: getEnv('BENEFICIARY_ACCOUNT'),
                branch: getEnv('BENEFICIARY_BRANCH'),
                type: 'CURRENT',
                holder: {
                    document: getEnv('BENEFICIARY_HOLDER_DOCUMENT'),
                    name: getEnv('BENEFICIARY_HOLDER_NAME'),
                },
            },
        };
        console.log(`--- Realizando transfer: ${JSON.stringify(req)} ---`);

        const result = await client.pixService.postTEDTransfer(req);

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }
}
