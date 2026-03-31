# Node Sample - Delfinance SDK

Este projeto executa exemplos do SDK Delfinance por módulos:
- Pix Transfer
- Pix Keys
- Pix QRCode
- WebHook
- BankSlip

## Como executar

Comando de referência para instalar o SDK:

```bash
npm i @delbank/delfinance-api-sdk
```

1. Instale dependências:

```bash
npm install
```

2. Configure o arquivo `.env` com as variáveis de ambiente necessárias (veja a seção **Variáveis de Ambiente**).

3. Execute o sample:

```bash
npm run test:sample
```

## Build Seletivo de Módulos

Você pode compilar apenas o módulo que deseja sem compilar todo o projeto:

### Via npm scripts:

```bash
# Compilar apenas Pix Transfer
npm run build:transfer

# Compilar apenas Pix Keys
npm run build:keys

# Compilar apenas Pix QRCode
npm run build:qrcode

# Compilar apenas WebHook
npm run build:webhook

# Compilar apenas BankSlip
npm run build:bankslip

# Compilar apenas o arquivo principal
npm run build:main

# Compilar tudo (padrão)
npm run build
```

### Via linha de comando (sem npm script):

```bash
# Compilar um arquivo específico
npx tsc samples/pixTransfer.ts

# Compilar todos os samples
npx tsc samples/*.ts

# Compilar apenas o arquivo principal
npx tsc sample.ts
```

## Configuração Inicial

### Variáveis de Ambiente

O arquivo `.env` na raiz do projeto contém todas as variáveis necessárias para autenticação e testes. Certifique-se de preencher:

**Autenticação (obrigatório):**
- `SDK_ENVIRONMENT` - Ambiente de execução (SANDBOX or PRODUCTION)
- `AUTH_ACCOUNT_ID` - ID da conta
- `AUTH_ACCOUNT_API_KEY` - Chave de API da conta

**Conta Origem (obrigatório para transferências):**
- `SOURCE_ACCOUNT_NUMBER` - Número da conta
- `SOURCE_ACCOUNT_BRANCH` - Agência da conta
- `SOURCE_HOLDER_DOCUMENT` - CPF/CNPJ do titular
- `SOURCE_HOLDER_EMAIL` - Email do titular
- `SOURCE_HOLDER_NAME` - Nome do titular

**Pix (obrigatório para operações Pix):**
- `PIX_KEY` - Chave Pix

**Beneficiário (obrigatório para transferências):**
- `BENEFICIARY_ACCOUNT` - Conta destino
- `BENEFICIARY_BRANCH` - Agência destino
- `BENEFICIARY_HOLDER_NAME` - Nome do beneficiário
- `BENEFICIARY_HOLDER_DOCUMENT` - CPF/CNPJ do beneficiário
- `BENEFICIARY_HOLDER_DOCUMENT_TYPE` - Tipo do documento (NATURAL ou LEGAL)
- `ISPB` - ISPB da instituição destino

**Valores de Teste:**
- `END_TO_END_ID` - ID de transferência para consulta
- `AMOUNT` - Valor padrão de transferência
- `QRCODE_AMOUNT` - Valor padrão de QR Code

**Endereço Mock (para QR Code):**
- `QRCODE_PUBLIC_PLACE`, `QRCODE_STREET`, `QRCODE_NEIGHBORHOOD`, etc.

**QR Code:**
- `QRCODE_EXPIRES_IN` - Tempo de expiração (ms)
- `QRCODE_FORMAT_RESPONSE` - Formato de resposta
- `QRCODE_DUE_DATE` - Data de vencimento
- `QRCODE_SOURCE` - Fonte do QR Code

**Cobranças:**
- `CHARGE_YOUR_NUMBER` - Número da cobrança

## Estrutura do Projeto

- `sample.ts`: Orquestrador principal que cria o cliente SDK e executa todos os samples
- `samples/pixTransfer.ts` - Exemplos de transferências Pix e TED
- `samples/pixKeys.ts` - Exemplos de gerenciamento de chaves Pix
- `samples/pixQRCode.ts` - Exemplos de criação e consulta de QR Codes
- `samples/webHook.ts` - Exemplos de gerenciamento de webhooks
- `samples/bankSlip.ts` - Exemplos de cobranças (boletos)

## Nova Arquitetura do SDK

O SDK Delfinance agora utiliza uma arquitetura baseada em `sdkClientFactory`:

```typescript
import { sdkClientFactory, delEnvironment, delSdkOptions } from '@delbank/delfinance-api-sdk';

const options: delSdkOptions = {
  environment: delEnvironment.Sandbox,
  accessToken: 'sua-api-key',
  accountId: 'sua-account-id',
  timeout: 30000,
};

const client = sdkClientFactory.createServicesClient(options);

// Usar os serviços
await client.pixService.getTransfer(transferId);
await client.webHookService.createWebHook(request);
await client.bankSlipService.createBankSlip(request);
```

## Erro Comum

Se aparecer erro como `Env XYZ não definida`, significa que a variável não existe no arquivo `.env`.

## Serviços Disponíveis

O cliente SDK oferece os seguintes serviços:

- **pixService** - Operações Pix (transferências, chaves, QR Codes)
- **webHookService** - Gerenciamento de webhooks
- **bankSlipService** - Gerenciamento de boletos/cobranças

## Requisitos do Sistema

- **Node.js**: 14+
- **TypeScript**: 5.9.3+
- **npm**: 6+

## Dependências

| Pacote | Versão |
|--------|--------|
| `@delbank/delfinance-api-sdk` | ^0.5.5 |
| `@delbank/del-sdk` | ^0.4.0 |
| `dotenv` | ^17.3.1 |
| `ts-node` | ^10.9.2 |
| `typescript` | ^5.9.3 |

## Estrutura de Arquivos de Saída

Após compilar, os arquivos JavaScript são gerados em:

```
dist/
├── sample.js
└── samples/
    ├── pixTransfer.js
    ├── pixKeys.js
    ├── pixQRCode.js
    ├── webHook.js
    └── bankSlip.js
```

## Dicas e Troubleshooting

- **Limpeza de build**: Delete a pasta `dist/` e rode `npm run build` novamente
- **Verificar imports**: Certifique-se de que as importações usam o path correto: `@delbank/delfinance-api-sdk/dist/delSdk`
- **Atualizar dependências**: Execute `npm install` antes de compilar após clonar o projeto
