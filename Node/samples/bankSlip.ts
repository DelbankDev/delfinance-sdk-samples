// Tipos importados apenas para referência (podem não ser necessários na nova versão)
export async function runBankSlipSample(
    client: any,
    getEnv: (name: string) => string
): Promise<void> {
    console.log('\n=========================================================');
    console.log(' BankSlip Service');
    console.log('=========================================================');

    try {
        console.log('--- createBankSlip ---');

        const req: any = {
            type: 'BANKSLIP',
            dueDate: '2026-12-31',
            amount: 100.00,
            description: 'Cobrança de teste via SDK',
            correlationId: getEnv('BANKSLIP_CORRELATION_ID'),
            payer: {
                name: getEnv('PAYER_NAME'),
                document: getEnv('PAYER_DOCUMENT'),
            },
        };

        const result = await client.bankSlipService.createBankSlip(req);

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- getBankSlipByCorrelationId ---');

        const result = await client.bankSlipService.getBankSlipByCorrelationId(
            getEnv('BANKSLIP_CORRELATION_ID')
        );

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- getListBankSlip ---');

        const result = await client.bankSlipService.getListBankSlip({
            startDate: '2026-01-01',
            endDate: '2026-12-31',
            page: 1,
            limit: 10,
        });

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- updateBankSlip ---');

        const req: any = {
            correlationId: getEnv('BANKSLIP_CORRELATION_ID'),
            dueDate: '2027-01-31',
        };

        const result = await client.bankSlipService.updateBankSlip(req);

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- postBaiWhenBankSlip ---');

        const result = await client.bankSlipService.postBaiWhenBankSlip(
            getEnv('BANKSLIP_CORRELATION_ID')
        );

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- postPaymentBankSlip ---');

        const req: any = {
            amount: 100.00,
            digitableLine: getEnv('BANKSLIP_DIGITABLE_LINE'),
        };

        const result = await client.bankSlipService.postPaymentBankSlip(req);

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- getPaymentBankSlip ---');

        const result = await client.bankSlipService.getPaymentBankSlip(getEnv('BANKSLIP_BARCODE'));

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }

    try {
        console.log('--- getDownloadBankSlip ---');

        const result = await client.bankSlipService.getDownloadBankSlip(getEnv('BANKSLIP_OUR_NUMBER'));

        console.log('Sucesso:', JSON.stringify(result, null, 2));
    } catch (error) {
        console.error('Erro ao testar o SDK:');
        if (error instanceof Error) console.error(`Mensagem: ${error.message}`);
    }
}
