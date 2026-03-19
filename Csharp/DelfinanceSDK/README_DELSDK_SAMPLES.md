# DelfinanceSDK - Sample Project

Este projeto demonstra o uso completo do **DelSDK** (versão 0.2.2) para integração com a API da Delfinance.

## 📁 Estrutura do Projeto

O projeto está organizado em controllers separados por funcionalidade:

```
Controllers/
├── Base/
│   └── DelSdkBaseController.cs       # Controller base com métodos compartilhados
├── PixQrCodeController.cs            # QR Codes PIX (estático, dinâmico imediato e com vencimento)
├── PixTransferController.cs          # Transferências PIX e TED
├── PixKeyController.cs               # Gerenciamento de chaves PIX
├── ChargeController.cs               # Cobranças (Boletos)
├── WebhookController.cs              # Gerenciamento de webhooks
└── ExamplesController.cs             # Exemplos prontos para teste
```

## 🚀 Funcionalidades Implementadas

### 1️⃣ PIX - QR Code (`/api/PixQrCode`)
- ✅ **Criar QR Code Estático**: `POST /api/PixQrCode/static`
- ✅ **Consultar QR Code Estático**: `GET /api/PixQrCode/static/{transactionId}`
- ✅ **Listar Pagamentos**: `GET /api/PixQrCode/static/{transactionId}/payments`
- ✅ **Cancelar QR Code Estático**: `DELETE /api/PixQrCode/static/{transactionId}`
- ✅ **Criar QR Code Dinâmico Imediato**: `POST /api/PixQrCode/dynamic-immediate`
- ✅ **Consultar QR Code Dinâmico Imediato**: `GET /api/PixQrCode/dynamic-immediate/{correlationId}`
- ✅ **Cancelar QR Code Dinâmico Imediato**: `DELETE /api/PixQrCode/dynamic-immediate/{correlationId}`
- ✅ **Criar QR Code com Vencimento**: `POST /api/PixQrCode/dynamic-duedate`
- ✅ **Consultar QR Code com Vencimento**: `GET /api/PixQrCode/dynamic-duedate/{transactionId}`
- ✅ **Cancelar QR Code com Vencimento**: `DELETE /api/PixQrCode/dynamic-duedate/{transactionId}`

### 2️⃣ PIX - Transferências (`/api/PixTransfer`)
- ✅ **Inicializar Pagamento (DICT)**: `POST /api/PixTransfer/initialize/dict`
- ✅ **Inicializar Pagamento (QR Code)**: `POST /api/PixTransfer/initialize/qrcode`
- ✅ **Criar Transferência PIX**: `POST /api/PixTransfer/pix`
- ✅ **Consultar Transferência PIX**: `GET /api/PixTransfer/pix/{identifier}`
- ✅ **Criar Transferência TED**: `POST /api/PixTransfer/ted`
- ✅ **Consultar TED**: `GET /api/PixTransfer/ted/{identifier}`

### 3️⃣ PIX - Chaves (`/api/PixKey`)
- ✅ **Gerar Código de Autenticação**: `POST /api/PixKey/generate-auth-code`
- ✅ **Criar Chave PIX**: `POST /api/PixKey`
- ✅ **Listar Chaves PIX**: `GET /api/PixKey`
- ✅ **Deletar Chave PIX**: `DELETE /api/PixKey/{pixKey}`

### 4️⃣ Cobranças (`/api/Charge`)
- ✅ **Criar Cobrança (Boleto)**: `POST /api/Charge`
- ✅ **Pagar Boleto**: `POST /api/Charge/pay-bill`

### 5️⃣ Webhooks (`/api/Webhook`)
- ✅ **Criar Webhook**: `POST /api/Webhook`
- ✅ **Listar Webhooks**: `GET /api/Webhook`
- ✅ **Consultar Webhook**: `GET /api/Webhook/{id}`
- ✅ **Atualizar Webhook**: `PUT /api/Webhook/{id}`
- ✅ **Remover Webhook**: `DELETE /api/Webhook/{id}`

### 6️⃣ Exemplos (`/api/Examples`)
- 📋 **Exemplo QR Code Estático**: `GET /api/Examples/static-qrcode`
- 📋 **Exemplo QR Code Dinâmico Imediato**: `GET /api/Examples/dynamic-immediate-qrcode`
- 📋 **Exemplo QR Code com Vencimento**: `GET /api/Examples/dynamic-duedate-qrcode`
- 📋 **Exemplo Criar Cobrança**: `GET /api/Examples/create-charge`

## ⚙️ Configuração

### Opção 1: Usando arquivo `.env` (Recomendado)

1. Copie o arquivo `.env.example` para `.env`:
```bash
cp .env.example .env
```

2. Edite o arquivo `.env` com suas credenciais:
```env
AUTH_ACCOUNT_ID=seu-account-id
AUTH_ACCOUNT_API_KEY=sua-api-key
AUTH_ENVIRONMENT=Sandbox
```

### Opção 2: Usando `appsettings.json`

Configure no `appsettings.json`:

```json
{
  "DelSdk": {
    "Environment": "Sandbox",
    "ApiKey": "sua-api-key-aqui",
    "AccountId": "sua-account-id-aqui"
  }
}
```

### 3. Executar o projeto:

```bash
dotnet run
```

### 4. Acessar a documentação OpenAPI:

Quando o projeto estiver rodando em modo Development, acesse:
```
https://localhost:{porta}/openapi/v1.json
```

## 📝 Exemplos de Uso

### Exemplo 1: Criar QR Code Estático

**Request:**
```http
POST /api/PixQrCode/static
Content-Type: application/json

{
  "correlationId": "550e8400-e29b-41d4-a716-446655440000",
  "amount": 150.00,
  "additionalInfo": "Pagamento de mensalidade"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "correlationId": "550e8400-e29b-41d4-a716-446655440000",
    "emvQrCode": "00020126580014br.gov.bcb.pix...",
    "qrCodeImage": "data:image/png;base64,iVBORw0KGgo...",
    "status": "ACTIVE"
  },
  "message": "QR Code estático criado com sucesso"
}
```

### Exemplo 2: Criar Cobrança (Boleto)

**Request:**
```http
POST /api/Charge
Content-Type: application/json

{
  "dueDate": "2024-02-28T00:00:00",
  "amount": 500.00,
  "description": "Pagamento de mensalidade",
  "yourNumber": "12345",
  "payer": {
    "name": "João Silva",
    "document": "12345678901",
    "email": "joao@example.com",
    "phone": {
      "prefix": "11",
      "number": "999999999"
    },
    "address": {
      "zipCode": "01310-100",
      "publicPlace": "Av. Paulista",
      "number": "1000",
      "neighborhood": "Bela Vista",
      "city": "São Paulo",
      "state": "SP"
    }
  },
  "lateFine": {
    "type": "Percentage",
    "amount": 2.0,
    "date": "2024-02-29T00:00:00"
  },
  "latePayment": {
    "type": "Percentage",
    "amount": 0.033,
    "date": "2024-02-29T00:00:00"
  },
  "notifyPayerOfCreation": true
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "key": "CHG-123456",
    "barCode": "12345678901234567890123456789012345678901234",
    "digitableLine": "12345.67890 12345.678901 23456.789012 3 45678901234",
    "qrCode": "00020126580014br.gov.bcb.pix...",
    "status": "ACTIVE"
  },
  "message": "Cobrança criada com sucesso"
}
```

### Exemplo 3: Inicializar e Criar Transferência PIX

**Passo 1 - Inicializar:**
```http
POST /api/PixTransfer/initialize/dict
Content-Type: application/json

{
  "pixKey": "12345678901",
  "amount": 100.00
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "endToEndId": "E123456789202401011234567890ABCD",
    "recipientName": "Maria Santos",
    "recipientDocument": "12345678901"
  }
}
```

**Passo 2 - Executar Transferência:**
```http
POST /api/PixTransfer/pix
Content-Type: application/json

{
  "endToEndId": "E123456789202401011234567890ABCD",
  "amount": 100.00,
  "description": "Pagamento de serviços"
}
```

## 🔒 Tratamento de Erros

Todos os endpoints retornam erros padronizados:

```json
{
  "success": false,
  "error": "Validation Error",
  "code": "ERR_VALIDATION",
  "errors": [
    "O campo Amount é obrigatório",
    "O valor deve ser maior que zero"
  ]
}
```

## 🏗️ Arquitetura do Projeto

### Controllers Base
O projeto utiliza uma classe base (`DelSdkBaseController`) que:
- Gerencia a configuração do SDK (lendo do `.env` ou `appsettings.json`)
- Fornece métodos auxiliares para respostas padronizadas
- Centraliza o tratamento de erros
- Facilita a manutenção e reutilização de código

### Variáveis de Ambiente

O projeto suporta as seguintes variáveis de ambiente no arquivo `.env`:

#### Autenticação
- `AUTH_ACCOUNT_ID` - ID da conta Delfinance
- `AUTH_ACCOUNT_API_KEY` - API Key da conta
- `AUTH_ENVIRONMENT` - Ambiente (Sandbox ou Production)

#### Testes de Iniciação
- `END_TO_END_ID` - ID end-to-end para testes
- `PIX_KEY` - Chave PIX para testes
- `PAYLOAD_QR_CODE` - Payload EMV para testes

#### Testes de Transferência
- `BENEFICIARY_ACCOUNT` - Conta do beneficiário
- `BENEFICIARY_BRANCH` - Agência do beneficiário
- `BENEFICIARY_HOLDER_NAME` - Nome do titular
- `BENEFICIARY_HOLDER_DOCUMENT` - Documento do titular
- `ISPB` - ISPB da instituição

#### Testes de QR Code
- `QRCODE_*` - Diversas configurações para testes de QR Code

Consulte o arquivo `.env.example` para ver todas as variáveis disponíveis.

## 📚 Documentação Adicional

- [Documentação Oficial da API Delfinance](https://docs.delfinance.com.br)
- [Repositório do DelSDK](https://github.com/DelbankDev/delfinance-sdk-samples)

## 🛠️ Tecnologias

- **.NET 10**
- **DelSDK 0.2.2**
- **ASP.NET Core Web API**
- **OpenAPI**

## 📞 Suporte

Para dúvidas ou problemas, consulte a documentação oficial ou entre em contato com o suporte da Delfinance.

---

**Desenvolvido com DelfinanceSDK** 🚀
