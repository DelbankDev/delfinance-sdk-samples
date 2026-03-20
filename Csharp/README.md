# Delfinance SDK Samples - Console C#

Projeto console em .NET 10 para executar chamadas do DelSDK direto no terminal, sem controllers e sem API HTTP.

## Requisitos

- .NET SDK 10
- Credenciais Delfinance no `.env` ou `appsettings.json`

## Configuração

A aplicação prioriza variáveis do `.env` e usa `appsettings.json` como fallback.

### Variáveis principais

- `AUTH_ACCOUNT_ID`
- `AUTH_ACCOUNT_API_KEY`
- `SDK_ENVIRONMENT` (ou `AUTH_ENVIRONMENT`)

## Executar

```bash
dotnet run
```

## Módulos disponíveis no menu

### PIX QR Code

- Criar QR Code estático
- Consultar QR Code estático
- Listar pagamentos de QR Code estático
- Cancelar QR Code estático
- Criar QR Code dinâmico imediato
- Consultar QR Code dinâmico imediato
- Cancelar QR Code dinâmico imediato
- Criar QR Code dinâmico com vencimento
- Consultar QR Code dinâmico com vencimento
- Cancelar QR Code dinâmico com vencimento

### Transferências

- Inicializar pagamento (DICT)
- Inicializar pagamento (QR Code)
- Criar transferência PIX
- Consultar transferência PIX
- Criar transferência TED
- Consultar transferência TED

### Chaves PIX

- Gerar código de autenticação
- Criar chave
- Listar chaves
- Deletar chave

### Cobranças

- Criar cobrança
- Pagar boleto

### Webhooks

- Criar webhook
- Listar webhooks
- Consultar webhook por ID
- Atualizar webhook
- Deletar webhook

## Observações importantes

- Operações com payload aceitam JSON interativo no terminal: você pode colar JSON customizado ou pressionar ENTER para usar o JSON padrão montado a partir do `.env`.
- A aplicação imprime payload enviado, resposta e erros estruturados do SDK.
- Arquivos de documentação antigos foram removidos para manter apenas este README.
