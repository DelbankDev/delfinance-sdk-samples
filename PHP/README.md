# Delfinance SDK Samples (PHP)

Exemplo simples de uso do SDK da Delfinance via console.

## Requisitos

- PHP 8.1+

## Instalação

No diretório do projeto, rode:

```bash
php composer.phar install
```

Referência para adicionar o SDK via Composer:

```bash
composer require delfinance/delfinance-api-sdk
```

## Configuração

1. Crie o arquivo `.env` (ou use o que já existe).
2. Preencha as credenciais e dados de teste.
3. Use `.env.example` como referência das variáveis.

## Comandos

Mostrar ajuda:

```bash
php bin/console help
```

Validar SDK instalado:

```bash
php bin/console check-sdk
```

Executar exemplos por grupo:

```bash
php bin/console pix-transfer
php bin/console pix-keys
php bin/console pix-qrcode
php bin/console charges
php bin/console webhook
```

Executar tudo:

```bash
php bin/console all
```
