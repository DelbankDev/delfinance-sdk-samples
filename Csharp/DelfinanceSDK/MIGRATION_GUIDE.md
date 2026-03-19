# 📝 Guia de Migração - Controllers Modularizados

## 🎯 O que mudou?

O projeto foi refatorado para separar as funcionalidades em controllers distintos, seguindo o princípio de **Single Responsibility** e melhorando a organização do código.

### Antes (❌ Estrutura Antiga)
- ❌ Um único controller monolítico (`DelSdkSampleController.cs`)
- ❌ Mais de 900 linhas de código em um único arquivo
- ❌ Difícil manutenção e navegação
- ❌ Rotas genéricas (`/api/DelSdkSample/...`)

### Agora (✅ Estrutura Nova)
- ✅ Controllers separados por domínio/funcionalidade
- ✅ Classe base compartilhada (`DelSdkBaseController`)
- ✅ Código mais limpo e organizado
- ✅ Rotas RESTful semânticas
- ✅ Suporte a variáveis de ambiente (.env)

---

## 🔄 Mapeamento de Rotas

### PIX - QR Codes

| Antes | Agora | Método |
|-------|-------|--------|
| `/api/DelSdkSample/pix/static-qrcode` | `/api/PixQrCode/static` | POST |
| `/api/DelSdkSample/pix/static-qrcode/{id}` | `/api/PixQrCode/static/{id}` | GET |
| `/api/DelSdkSample/pix/static-qrcode/{id}/payments` | `/api/PixQrCode/static/{id}/payments` | GET |
| `/api/DelSdkSample/pix/static-qrcode/{id}` | `/api/PixQrCode/static/{id}` | DELETE |
| `/api/DelSdkSample/pix/dynamic-immediate-qrcode` | `/api/PixQrCode/dynamic-immediate` | POST |
| `/api/DelSdkSample/pix/dynamic-immediate-qrcode/{id}` | `/api/PixQrCode/dynamic-immediate/{id}` | GET |
| `/api/DelSdkSample/pix/dynamic-immediate-qrcode/{id}` | `/api/PixQrCode/dynamic-immediate/{id}` | DELETE |
| `/api/DelSdkSample/pix/dynamic-duedate-qrcode` | `/api/PixQrCode/dynamic-duedate` | POST |
| `/api/DelSdkSample/pix/dynamic-duedate-qrcode/{id}` | `/api/PixQrCode/dynamic-duedate/{id}` | GET |
| `/api/DelSdkSample/pix/dynamic-duedate-qrcode/{id}` | `/api/PixQrCode/dynamic-duedate/{id}` | DELETE |

### PIX - Transferências

| Antes | Agora | Método |
|-------|-------|--------|
| `/api/DelSdkSample/pix/initialize-payment` | `/api/PixTransfer/initialize/dict` | POST |
| `/api/DelSdkSample/pix/initialize-qrcode-payment` | `/api/PixTransfer/initialize/qrcode` | POST |
| `/api/DelSdkSample/pix/transfer` | `/api/PixTransfer/pix` | POST |
| `/api/DelSdkSample/pix/transfer/{id}` | `/api/PixTransfer/pix/{id}` | GET |
| `/api/DelSdkSample/pix/ted-transfer` | `/api/PixTransfer/ted` | POST |
| `/api/DelSdkSample/pix/ted-transfer/{id}` | `/api/PixTransfer/ted/{id}` | GET |

### PIX - Chaves

| Antes | Agora | Método |
|-------|-------|--------|
| `/api/DelSdkSample/pix/keys/generate-auth-code` | `/api/PixKey/generate-auth-code` | POST |
| `/api/DelSdkSample/pix/keys` | `/api/PixKey` | POST |
| `/api/DelSdkSample/pix/keys` | `/api/PixKey` | GET |
| `/api/DelSdkSample/pix/keys/{key}` | `/api/PixKey/{key}` | DELETE |

### Cobranças

| Antes | Agora | Método |
|-------|-------|--------|
| `/api/DelSdkSample/charges/create` | `/api/Charge` | POST |
| `/api/DelSdkSample/charges/pay-bill` | `/api/Charge/pay-bill` | POST |

### Webhooks

| Antes | Agora | Método |
|-------|-------|--------|
| `/api/DelSdkSample/webhooks/subscribe` | `/api/Webhook` | POST |
| `/api/DelSdkSample/webhooks` | `/api/Webhook` | GET |
| `/api/DelSdkSample/webhooks/{id}` | `/api/Webhook/{id}` | DELETE |
| ➕ **NOVO** | `/api/Webhook/{id}` | GET |
| ➕ **NOVO** | `/api/Webhook/{id}` | PUT |

### Exemplos

| Antes | Agora | Método |
|-------|-------|--------|
| `/api/DelSdkSample/examples/static-qrcode` | `/api/Examples/static-qrcode` | GET |
| `/api/DelSdkSample/examples/dynamic-immediate-qrcode` | `/api/Examples/dynamic-immediate-qrcode` | GET |
| `/api/DelSdkSample/examples/dynamic-duedate-qrcode` | `/api/Examples/dynamic-duedate-qrcode` | GET |
| `/api/DelSdkSample/examples/create-charge` | `/api/Examples/create-charge` | GET |

---

## 📁 Nova Estrutura de Arquivos

```
Controllers/
├── Base/
│   └── DelSdkBaseController.cs       # ✨ Controller base (NOVO)
├── PixQrCodeController.cs            # 🔵 QR Codes PIX
├── PixTransferController.cs          # 🔵 Transferências PIX/TED
├── PixKeyController.cs               # 🔵 Chaves PIX
├── ChargeController.cs               # 🔵 Cobranças
├── WebhookController.cs              # 🔵 Webhooks
└── ExamplesController.cs             # 🔵 Exemplos

❌ REMOVIDO: Controllers/DelSdkSampleController.cs
```

---

## 🆕 Novos Recursos

### 1. Suporte a Variáveis de Ambiente (.env)

Agora você pode configurar as credenciais usando um arquivo `.env`:

```env
AUTH_ACCOUNT_ID=53929
AUTH_ACCOUNT_API_KEY=sua-api-key
AUTH_ENVIRONMENT=Sandbox
```

### 2. Controller Base Compartilhado

Todos os controllers herdam de `DelSdkBaseController`, que fornece:

```csharp
protected DelSdkConfigurations GetDelSdkOptions()
protected IActionResult Success(object data, string message)
protected IActionResult Success(string message)
```

### 3. Novos Endpoints de Webhook

Adicionados endpoints para:
- `GET /api/Webhook/{id}` - Consultar webhook específico
- `PUT /api/Webhook/{id}` - Atualizar webhook

---

## 🔧 Como Migrar

### Para desenvolvedores que estão usando o projeto

1. **Atualize o código do repositório:**
   ```bash
   git pull origin main
   ```

2. **Instale o novo pacote:**
   ```bash
   dotnet restore
   ```

3. **Configure o arquivo `.env`:**
   ```bash
   cp .env.example .env
   # Edite o .env com suas credenciais
   ```

4. **Atualize as chamadas de API:**
   - Substitua as URLs antigas pelas novas (veja tabela de mapeamento acima)
   - As respostas continuam no mesmo formato

### Para integrações existentes

Se você tem clientes consumindo a API antiga:

**Opção 1: Atualização Gradual (Recomendado)**
1. Atualize a documentação da API
2. Notifique os clientes sobre as novas rotas
3. Implemente ambas as versões temporariamente
4. Migre gradualmente

**Opção 2: Mantendo compatibilidade**
Você pode criar controllers com as rotas antigas que redirecionam para as novas:

```csharp
[ApiController]
[Route("api/DelSdkSample")]
public class LegacyController : ControllerBase
{
    [HttpPost("pix/static-qrcode")]
    public IActionResult LegacyCreateStaticQrCode([FromBody] CreateStaticQrCodeRequest request)
    {
        // Redireciona para a nova rota
        return RedirectToAction("CreateStaticQrCode", "PixQrCode", request);
    }
}
```

---

## ✅ Benefícios da Nova Estrutura

1. **🎯 Separação de Responsabilidades**
   - Cada controller tem uma única responsabilidade
   - Facilita manutenção e testes

2. **📖 Código Mais Legível**
   - Arquivos menores e focados
   - Navegação mais fácil no projeto

3. **🔒 Segurança Aprimorada**
   - Arquivo `.env` não é commitado (incluído no `.gitignore`)
   - Credenciais separadas do código

4. **🚀 Melhor Performance**
   - Controllers menores carregam mais rápido
   - Menor consumo de memória

5. **🧪 Facilita Testes**
   - Controllers menores são mais fáceis de testar
   - Classe base facilita mocks

6. **📚 Melhor Documentação**
   - Rotas mais semânticas
   - Swagger/OpenAPI mais organizado

---

## 🆘 Problemas Comuns e Soluções

### Erro: "Could not find a part of the path '.env'"

**Solução:** Crie o arquivo `.env` baseado no `.env.example`:
```bash
cp .env.example .env
```

### Erro: 404 Not Found nas rotas antigas

**Solução:** Atualize as URLs conforme a tabela de mapeamento acima.

### Erro: "DotNetEnv" não encontrado

**Solução:** Restaure os pacotes NuGet:
```bash
dotnet restore
```

---

## 📞 Suporte

- Consulte a documentação atualizada no `README_DELSDK_SAMPLES.md`
- Veja exemplos no `EXEMPLOS_USO.md`
- Para dúvidas, abra uma issue no repositório

---

**Data da Migração:** Janeiro 2025  
**Versão:** 2.0.0  
**Autor:** Equipe Delfinance SDK
