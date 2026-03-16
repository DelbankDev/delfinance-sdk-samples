# Node Sample - Delfinance SDK

Este projeto executa exemplos do SDK por modulos:
- Pix Transfer
- Pix Keys
- Pix QRCode
- WebHook
- BankSlip

## Como executar

1. Instale dependencias:

```bash
npm install
```

2. Rode o sample:

```bash
npm run test:sample
```

## Erro comum

Se aparecer erro como `Env XYZ nao definida`, significa que a variavel nao existe no arquivo `.env`.

## Variaveis obrigatorias

### Autenticacao
- `AUTH_ACCOUNT_ID`
- `AUTH_ACCOUNT_API_KEY`

### Pix Transfer
- `END_TO_END_ID`
- `PIX_KEY`
- `PAYLOAD_QR_CODE`
- `BENEFICIARY_ACCOUNT`
- `BENEFICIARY_BRANCH`
- `BENEFICIARY_HOLDER_NAME`
- `BENEFICIARY_HOLDER_DOCUMENT`
- `ISPB`

### Pix QRCode
- `QRCODE_CORRELATION_ID`
- `QRCODE_STATIC_ID`
- `QRCODE_DYNAMIC_IMEDIATE_ID`
- `QRCODE_DYNAMIC_DUEDATE_ID`
- `PIX_KEY`

### WebHook
- `WEBHOOK_EVENT_TYPE`
- `WEBHOOK_URL`
- `WEBHOOK_AUTHORIZATION`
- `WEBHOOK_ID`

### BankSlip
- `BANKSLIP_CORRELATION_ID`
- `PAYER_NAME`
- `PAYER_DOCUMENT`
- `BANKSLIP_DIGITABLE_LINE`
- `BANKSLIP_BARCODE`
- `BANKSLIP_OUR_NUMBER`

## Estrutura

- `sample.ts`: orquestrador principal
- `samples/pixTransfer.ts`
- `samples/pixKeys.ts`
- `samples/pixQRCode.ts`
- `samples/webHook.ts`
- `samples/bankSlip.ts`
