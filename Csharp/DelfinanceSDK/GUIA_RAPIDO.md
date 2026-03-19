# 🚀 DelfinanceSDK Sample - Guia Rápido

## ✅ O que foi criado

Este projeto contém um **sample completo** do DelSDK v0.2.2 com todos os módulos implementados e prontos para teste.

### 📁 Arquivos Criados

1. **`Controllers/DelSdkSampleController.cs`** - Controller principal com todos os endpoints
2. **`appsettings.json`** - Configurações do SDK (API Key, Account ID, Environment)
3. **`README_DELSDK_SAMPLES.md`** - Documentação completa do projeto
4. **`EXEMPLOS_USO.md`** - Guia detalhado com exemplos de uso
5. **`POSTMAN_COLLECTION.md`** - Coleção para Postman/Insomnia
6. **`GUIA_RAPIDO.md`** - Este arquivo

---

## ⚙️ Configuração Rápida

### 1. Configure suas credenciais no `appsettings.json`:

```json
{
  "DelSdk": {
    "Environment": "Sandbox",
    "ApiKey": "SUA-API-KEY-AQUI",
    "AccountId": "SUA-ACCOUNT-ID-AQUI"
  }
}
```

### 2. Execute o projeto:

```bash
dotnet run
```

### 3. Acesse a API:

A API estará disponível em: `https://localhost:5001` (ou a porta configurada)

---

## 📋 Endpoints Disponíveis

### 🔵 PIX - QR Codes (9 endpoints)
- ✅ Criar/Consultar/Listar/Cancelar QR Code Estático
- ✅ Criar/Consultar/Cancelar QR Code Dinâmico Imediato
- ✅ Criar/Consultar/Cancelar QR Code Dinâmico com Vencimento

### 🔵 PIX - Transferências (6 endpoints)
- ✅ Inicializar Pagamento via Chave PIX
- ✅ Inicializar Pagamento via QR Code
- ✅ Criar Transferência PIX
- ✅ Consultar Transferência PIX
- ✅ Criar Transferência TED
- ✅ Consultar Transferência TED

### 🔵 PIX - Chaves (4 endpoints)
- ✅ Gerar Código de Autenticação
- ✅ Criar Chave PIX
- ✅ Listar Chaves PIX
- ✅ Deletar Chave PIX

### 🔵 Cobranças (2 endpoints)
- ✅ Criar Cobrança (Boleto)
- ✅ Pagar Boleto

### 🔵 Webhooks (3 endpoints)
- ✅ Inscrever Webhook
- ✅ Listar Webhooks
- ✅ Remover Webhook

### 🔵 Exemplos (4 endpoints)
- ✅ Exemplo QR Code Estático
- ✅ Exemplo QR Code Dinâmico Imediato
- ✅ Exemplo QR Code com Vencimento
- ✅ Exemplo Criar Cobrança

**Total: 28 endpoints implementados** ✨

---

## 🎯 Teste Rápido

### Testar QR Code Estático

```bash
curl -X POST https://localhost:5001/api/DelSdkSample/pix/static-qrcode \
  -H "Content-Type: application/json" \
  -d '{
    "correlationId": "550e8400-e29b-41d4-a716-446655440000",
    "amount": 150.00,
    "additionalInfo": "Pagamento de mensalidade"
  }'
```

### Obter Exemplo de QR Code

```bash
curl https://localhost:5001/api/DelSdkSample/examples/static-qrcode
```

---

## 📖 Documentação

- **`README_DELSDK_SAMPLES.md`** - Visão geral do projeto e funcionalidades
- **`EXEMPLOS_USO.md`** - Exemplos detalhados de cada endpoint
- **`POSTMAN_COLLECTION.md`** - Coleção completa para importar no Postman/Insomnia

---

## 🔥 Recursos Implementados

✅ **Configuração via appsettings.json**
✅ **Tratamento de erros padronizado**
✅ **Logging automático de erros**
✅ **Idempotência automática em transferências**
✅ **Exemplos prontos para todos os módulos**
✅ **Respostas padronizadas (success, data, message)**
✅ **Suporte a Sandbox e Production**
✅ **Documentação completa em português**

---

## 🛠️ Tecnologias

- **.NET 10**
- **DelSDK 0.2.2**
- **ASP.NET Core Web API**
- **OpenAPI/Swagger**

---

## 💡 Próximos Passos

1. ✅ Configure suas credenciais no `appsettings.json`
2. ✅ Execute o projeto com `dotnet run`
3. ✅ Teste os endpoints usando Postman ou curl
4. ✅ Consulte o `EXEMPLOS_USO.md` para exemplos detalhados
5. ✅ Importe a coleção do Postman do arquivo `POSTMAN_COLLECTION.md`

---

## 📞 Links Úteis

- [Documentação Oficial da API Delfinance](https://docs.delfinance.com.br)
- [Repositório GitHub](https://github.com/DelbankDev/delfinance-sdk-samples)
- [Pacote NuGet DelSDK](https://www.nuget.org/packages/DelSdk)

---

## ✨ Estrutura do Projeto

```
DelfinanceSDK/
├── Controllers/
│   └── DelSdkSampleController.cs    ← 28 endpoints implementados
├── appsettings.json                  ← Configuração do SDK
├── Program.cs                        ← Configuração da aplicação
├── README_DELSDK_SAMPLES.md         ← Documentação principal
├── EXEMPLOS_USO.md                  ← Guia de exemplos detalhados
├── POSTMAN_COLLECTION.md            ← Coleção para testes
└── GUIA_RAPIDO.md                   ← Este arquivo
```

---

## 🎊 Pronto para Usar!

O projeto está **100% funcional** e pronto para testes. Todos os módulos do DelSDK foram implementados com:

- ✅ Documentação completa
- ✅ Exemplos práticos
- ✅ Tratamento de erros
- ✅ Logs detalhados
- ✅ Código limpo e organizado

**Bons testes!** 🚀

---

**Desenvolvido com DelfinanceSDK 0.2.2** 💙
