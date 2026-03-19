# Guia de Exemplos - DelfinanceSDK

Este documento fornece exemplos práticos de como usar todos os endpoints do sample.

## 📋 Sumário

1. [Configuração Inicial](#configuração-inicial)
2. [PIX - QR Codes](#pix---qr-codes)
3. [PIX - Transferências](#pix---transferências)
4. [PIX - Chaves PIX](#pix---chaves-pix)
5. [Cobranças (Boletos)](#cobranças-boletos)
6. [Webhooks](#webhooks)

---

## Configuração Inicial

Antes de começar, configure suas credenciais no `appsettings.json`:

```json
{
  "DelSdk": {
    "Environment": "Sandbox",
    "ApiKey": "sua-api-key-aqui",
    "AccountId": "sua-account-id-aqui"
  }
}
```

---

## PIX - QR Codes

### 1. QR Code Estático

#### Criar QR Code Estático
```http
POST /api/DelSdkSample/pix/static-qrcode
Content-Type: application/json

{
  "correlationId": "550e8400-e29b-41d4-a716-446655440000",
  "amount": 150.00,
  "additionalInfo": "Pagamento de mensalidade"
}
```

#### Consultar QR Code Estático
```http
GET /api/DelSdkSample/pix/static-qrcode/{transactionId}
```

#### Listar Pagamentos do QR Code
```http
GET /api/DelSdkSample/pix/static-qrcode/{transactionId}/payments
```

#### Cancelar QR Code Estático
```http
DELETE /api/DelSdkSample/pix/static-qrcode/{transactionId}
```

---

### 2. QR Code Dinâmico Imediato

**Ideal para**: Cobranças com expiração rápida (pagamentos imediatos)

#### Criar QR Code Dinâmico Imediato
```http
POST /api/DelSdkSample/pix/dynamic-immediate-qrcode
Content-Type: application/json

{
  "correlationId": "660e8400-e29b-41d4-a716-446655440001",
  "amount": 99.90,
  "expiresIn": "3600"
}
```

**Nota**: `expiresIn` está em segundos (3600 = 1 hora)

#### Consultar QR Code Dinâmico Imediato
```http
GET /api/DelSdkSample/pix/dynamic-immediate-qrcode/{correlationId}
```

#### Cancelar QR Code Dinâmico Imediato
```http
DELETE /api/DelSdkSample/pix/dynamic-immediate-qrcode/{correlationId}
```

---

### 3. QR Code Dinâmico com Vencimento

**Ideal para**: Boletos PIX com data de vencimento, multas e juros

#### Criar QR Code com Vencimento
```http
POST /api/DelSdkSample/pix/dynamic-duedate-qrcode
Content-Type: application/json

{
  "correlationId": "770e8400-e29b-41d4-a716-446655440002",
  "amount": 250.00,
  "dueDate": "2024-03-30T00:00:00"
}
```

#### Consultar QR Code com Vencimento
```http
GET /api/DelSdkSample/pix/dynamic-duedate-qrcode/{transactionId}
```

#### Cancelar QR Code com Vencimento
```http
DELETE /api/DelSdkSample/pix/dynamic-duedate-qrcode/{transactionId}
```

---

## PIX - Transferências

### 1. Transferência via Chave PIX

**Passo 1**: Inicializar o pagamento
```http
POST /api/DelSdkSample/pix/initialize-payment
Content-Type: application/json

{
  "pixKey": "12345678901",
  "amount": 100.00
}
```

**Response**:
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

**Passo 2**: Executar a transferência
```http
POST /api/DelSdkSample/pix/transfer
Content-Type: application/json

{
  "endToEndId": "E123456789202401011234567890ABCD",
  "amount": 100.00,
  "description": "Pagamento de serviços"
}
```

### 2. Transferência via QR Code

**Passo 1**: Inicializar pagamento via QR Code
```http
POST /api/DelSdkSample/pix/initialize-qrcode-payment
Content-Type: application/json

{
  "emvQrCode": "00020126580014br.gov.bcb.pix...",
  "amount": 50.00
}
```

**Passo 2**: Executar a transferência (mesmo endpoint do exemplo anterior)

### 3. Consultar Transferência
```http
GET /api/DelSdkSample/pix/transfer/{identifier}
```

### 4. Transferência TED

#### Criar Transferência TED
```http
POST /api/DelSdkSample/pix/ted-transfer
Content-Type: application/json

{
  "amount": 500.00,
  "beneficiary": {
    "name": "João Silva",
    "document": "12345678901",
    "bankCode": "001",
    "branch": "1234",
    "account": "123456",
    "accountType": "CHECKING"
  },
  "description": "Pagamento de fornecedor"
}
```

#### Consultar TED
```http
GET /api/DelSdkSample/pix/ted-transfer/{identifier}
```

---

## PIX - Chaves PIX

### 1. Gerar Código de Autenticação

**Importante**: Necessário para vincular chaves de e-mail ou telefone

```http
POST /api/DelSdkSample/pix/keys/generate-auth-code
Content-Type: application/json

{
  "entryType": "EMAIL",
  "entry": "usuario@example.com"
}
```

**Response**: O código será enviado para o e-mail/telefone especificado

### 2. Criar Chave PIX

#### Criar chave aleatória (EVP)
```http
POST /api/DelSdkSample/pix/keys?authCode=&authId=
Content-Type: application/json

{
  "entryType": "EVP"
}
```

#### Criar chave com autenticação (Email/Telefone)
```http
POST /api/DelSdkSample/pix/keys?authCode=123456&authId=auth-id-recebido
Content-Type: application/json

{
  "entryType": "EMAIL"
}
```

### 3. Listar Chaves PIX
```http
GET /api/DelSdkSample/pix/keys
```

**Response**:
```json
{
  "success": true,
  "data": [
    {
      "key": "12345678901",
      "type": "CPF",
      "status": "ACTIVE"
    },
    {
      "key": "usuario@example.com",
      "type": "EMAIL",
      "status": "ACTIVE"
    }
  ],
  "count": 2
}
```

### 4. Deletar Chave PIX
```http
DELETE /api/DelSdkSample/pix/keys/12345678901
```

---

## Cobranças (Boletos)

### 1. Criar Cobrança (Boleto)

```http
POST /api/DelSdkSample/charges/create
Content-Type: application/json

{
  "dueDate": "2024-03-30T00:00:00",
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
    "date": "2024-03-31T00:00:00"
  },
  "latePayment": {
    "type": "Percentage",
    "amount": 0.033,
    "date": "2024-03-31T00:00:00"
  },
  "notifyPayerOfCreation": true
}
```

**Response**:
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

### 2. Pagar Boleto

```http
POST /api/DelSdkSample/charges/pay-bill
Content-Type: application/json

{
  "barCode": "12345678901234567890123456789012345678901234",
  "amount": 500.00
}
```

**Para agendar pagamento**:
```json
{
  "barCode": "12345678901234567890123456789012345678901234",
  "amount": 500.00,
  "payAt": "2024-03-20T00:00:00"
}
```

**Response**:
```json
{
  "success": true,
  "data": {
    "id": "PAYMENT-789",
    "status": "PAID",
    "paidAmount": 500.00,
    "paidAt": "2024-03-18T14:30:00",
    "beneficiary": {
      "nameOrCompanyName": "Empresa XYZ Ltda"
    }
  },
  "message": "Boleto pago com sucesso"
}
```

---

## Webhooks

### 1. Inscrever Webhook

```http
POST /api/DelSdkSample/webhooks/subscribe
Content-Type: application/json

{
  "url": "https://seu-servidor.com/webhook",
  "events": [
    "PIX_RECEIVED",
    "CHARGE_PAID",
    "TRANSFER_COMPLETED"
  ],
  "authorizationScheme": "BEARER",
  "authorizationToken": "seu-token-secreto"
}
```

**Eventos disponíveis**:
- `PIX_RECEIVED` - Pagamento PIX recebido
- `CHARGE_PAID` - Cobrança paga
- `CHARGE_EXPIRED` - Cobrança expirada
- `TRANSFER_COMPLETED` - Transferência concluída
- `TRANSFER_FAILED` - Transferência falhou

### 2. Listar Webhooks
```http
GET /api/DelSdkSample/webhooks
```

**Response**:
```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "url": "https://seu-servidor.com/webhook",
      "events": ["PIX_RECEIVED"],
      "status": "ACTIVE"
    }
  ],
  "count": 1
}
```

### 3. Remover Webhook
```http
DELETE /api/DelSdkSample/webhooks/1
```

---

## 📚 Endpoints de Exemplos Prontos

Para facilitar o teste, use estes endpoints para obter exemplos de JSON prontos:

### Obter exemplo de QR Code Estático
```http
GET /api/DelSdkSample/examples/static-qrcode
```

### Obter exemplo de QR Code Dinâmico Imediato
```http
GET /api/DelSdkSample/examples/dynamic-immediate-qrcode
```

### Obter exemplo de QR Code com Vencimento
```http
GET /api/DelSdkSample/examples/dynamic-duedate-qrcode
```

### Obter exemplo de Criação de Cobrança
```http
GET /api/DelSdkSample/examples/create-charge
```

---

## 🔒 Tratamento de Erros

Todos os endpoints retornam erros no seguinte formato:

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

### Códigos de Status HTTP Comuns:
- `200` - Sucesso
- `400` - Erro de validação (dados incorretos)
- `401` - Não autorizado (API Key inválida)
- `404` - Recurso não encontrado
- `500` - Erro interno do servidor

---

## 💡 Dicas de Uso

### 1. Idempotência
Alguns endpoints (transferências, pagamentos) usam chaves de idempotência automáticas para evitar duplicações.

### 2. Ambiente Sandbox vs Produção
- **Sandbox**: Para testes (não processa transações reais)
- **Production**: Ambiente de produção (transações reais)

Configure no `appsettings.json`:
```json
{
  "DelSdk": {
    "Environment": "Sandbox"  // ou "Production"
  }
}
```

### 3. Logs
Todos os erros são automaticamente logados. Verifique os logs para debug.

---

## 📞 Suporte

Para mais informações:
- [Documentação Oficial da API Delfinance](https://docs.delfinance.com.br)
- [Repositório do DelSDK](https://github.com/DelbankDev/delfinance-sdk-samples)

---

**Desenvolvido com DelfinanceSDK 0.2.2** 🚀
