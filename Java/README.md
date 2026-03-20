# delfinance-sdk-smoke

Projeto Maven Java minimo para testar a integracao com `br.com.delfinance:delfinance-api-sdk`.

## Dependencia adicionada

```xml
<dependency>
    <groupId>br.com.delfinance</groupId>
    <artifactId>delfinance-api-sdk</artifactId>
    <version>0.7.3</version>
</dependency>
```

## Pre-requisitos

- Java 17+ (ja instalado)
- Maven 3.9+

No Windows, se nao tiver Maven:

```powershell
winget install Apache.Maven
```

## Variaveis de ambiente

Defina estas variaveis antes de executar:

- `DELFINANCE_ACCOUNT_ID`
- `DELFINANCE_API_KEY`
- `DELFINANCE_CERT_PATH`
- `DELFINANCE_PRIVATE_KEY_PATH`
- `DELFINANCE_ENVIRONMENT` (`sandbox` ou `production`)

Exemplo PowerShell:

```powershell
$env:DELFINANCE_ACCOUNT_ID="seu-account-id"
$env:DELFINANCE_API_KEY="sua-api-key"
$env:DELFINANCE_CERT_PATH="C:\certs\cert.pem"
$env:DELFINANCE_PRIVATE_KEY_PATH="C:\certs\private-key.pem"
$env:DELFINANCE_ENVIRONMENT="sandbox"
```

## Executar

Compilar e testar dependencia:

```powershell
mvn test
```

Rodar smoke test de inicializacao do cliente:

```powershell
mvn exec:java
```

Opcional: testar chamada real de listagem de webhooks:

```powershell
$env:RUN_WEBHOOKS_LIST="true"
mvn exec:java
```

## Observacao importante

O README oficial do SDK nao estava acessivel publicamente (repositorio pede autenticacao). Por isso, este projeto foi montado com base nas classes publicas do artefato publicado no Maven Central.