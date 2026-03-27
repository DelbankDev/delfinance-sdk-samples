# Delfinance SDK Samples

Este repositório contém exemplos práticos de integração com a **Delfinance API**, implementados em diferentes linguagens de programação. Os samples apresentam como utilizar o SDK para as operações mais comuns do sistema de pagamentos.

## 📋 Linguagens Suportadas

O projeto oferece implementações em 5 linguagens:
- **C#** (.NET)
- **Java** (Maven)
- **TypeScript/Node.js**
- **PHP**
- **Python**

## 🎯 Samples Inclusos (5 Funcionalidades Principais)

### 1. **Boletos Bancários (Charges)** 📄
Gerenciamento completo de cobranças via boleto bancário.

**Funcionalidades:**
- Criar boletos/cobranças
- Atualizar dados de cobranças
- Listar cobranças por período
- Registrar pagamentos de contas
- Consultar detalhes de cobranças

**Arquivos:**
- `Csharp/Controllers/...` - Implementação C#
- `Java/src/main/java/com/...` - Implementação Java
- `Node/samples/bankSlip.ts` - Implementação TypeScript
- `PHP/src/Samples/ChargesSample.php` - Implementação PHP
- `Phyton/app/samples/charges_methods.py` - Implementação Python

---

### 2. **Chaves PIX (PIX Keys)** 🔑
Gerenciamento de chaves PIX para recebimento de transferências.

**Funcionalidades:**
- Criar chaves PIX (EVP, CPF, CNPJ, Email, Telefone)
- Listar chaves PIX cadastradas
- Autenticação para criação de chaves
- Validação de chaves
- Gerenciamento de autorizações

**Arquivos:**
- `Node/samples/pixKeys.ts` - Implementação TypeScript
- `PHP/src/Samples/PixKeysSample.php` - Implementação PHP
- `Phyton/app/samples/pix_methods.py` - Implementação Python

---

### 3. **QR Code PIX** 🔲
Geração e gerenciamento de QR Codes PIX para cobranças instantâneas.

**Funcionalidades:**
- Criar QR Codes estáticos
- Criar QR Codes dinâmicos
- Consultar QR Codes criados
- Atualizar informações de QR Codes
- Integração com sistema de cobranças

**Arquivos:**
- `Node/samples/pixQRCode.ts` - Implementação TypeScript
- `PHP/src/Samples/PixQrCodeSample.php` - Implementação PHP
- `Phyton/app/samples/qrcode_methods.py` - Implementação Python

---

### 4. **Transferências PIX** 💸
Processamento de transferências PIX entre contas.

**Funcionalidades:**
- Inicializar pagamentos via chave PIX
- Inicializar pagamentos via QR Code
- Consultar status de transferências
- Obter detalhes de transações
- Validar dados de beneficiários
- Operações de débito e crédito

**Arquivos:**
- `Node/samples/pixTransfer.ts` - Implementação TypeScript
- `PHP/src/Samples/PixTransferSample.php` - Implementação PHP
- `Phyton/app/samples/transfers_methods.py` - Implementação Python

---

### 5. **WebHooks** 🪝
Recebimento de notificações de eventos do sistema em tempo real.

**Funcionalidades:**
- Criar webhooks para eventos
- Listar webhooks configurados
- Atualizar configurações de webhooks
- Deletar webhooks
- Gerenciar autenticação de webhooks
- Suporte para diferentes tipos de eventos

**Arquivos:**
- `Node/samples/webHook.ts` - Implementação TypeScript
- `PHP/src/Samples/WebhookSample.php` - Implementação PHP
- `Phyton/app/samples/webhooks_methods.py` - Implementação Python

---

## 🚀 Como Começar

### Pré-requisitos
- Credenciais da Delfinance API (API Key e API Secret)
- Variáveis de ambiente configuradas (consultar arquivo `.env` em cada diretório)

### Estrutura de Diretórios
```
delfinance-sdk-samples/
├── Csharp/               # Implementação .NET
├── Java/                 # Implementação Java
├── Node/                 # Implementação TypeScript/Node.js
│   └── samples/          # Samples individuais
├── PHP/                  # Implementação PHP
│   └── src/Samples/      # Samples individuais
└── Phyton/               # Implementação Python
    └── app/samples/      # Samples individuais
```

### Executando os Samples

Acesse cada diretório de linguagem e consulte o `README.md` específico para instruções de configuração e execução.

## 📚 Documentação Adicional

Cada linguagem possui um `README.md` próprio com detalhes específicos:
- [C# Docs](./Csharp/README.md)
- [Java Docs](./Java/README.md)
- [Node.js Docs](./Node/README.md)
- [PHP Docs](./PHP/README.md)
- [Python Docs](./Phyton/README.md)

## 🔐 Segurança

Nunca compartilhe suas credenciais de API. Utilize variáveis de ambiente para armazenar informações sensíveis.

## 📞 Suporte

Para mais informações sobre a API Delfinance, visite a [documentação oficial](https://docs.delfinance.com.br)