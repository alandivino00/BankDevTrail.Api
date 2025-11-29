# BankDevTrail.Api

Descrição
---------
API RESTful em .NET 8 que simula um banco digital (BankDevTrail). Implementa as camadas obrigatórias (Domínio, Serviço, Infraestrutura) e as operações financeiras essenciais: cadastro de clientes, abertura de contas, depósito, saque, transferência (atômica) e extrato (todas as transações de uma conta). A API expõe documentação via Swagger e foi desenvolvida seguindo a especificação do projeto.

Principais conceitos
- Entidades: `Cliente`, `Conta`, `Transacao`.
- Enum: `TipoTransacao` (Deposito, Saque, TransferenciaEnviada, TransferenciaRecebida).
- Fluxo: Controller → Service (regras de negócio) → Repository (persistência).
- Atomicidade: transferências atualizam dois saldos e geram duas transações em uma operação atômica.

Pré-requisitos
--------------
- .NET 8 SDK
- Visual Studio 2022 (ou VS Code)
- Docker Desktop (opcional, recomendado para banco)
- Cliente SQL de sua preferência (Azure Data Studio, DBeaver, SSMS, DataGrip)
- (Opcional) EF Core Tools (`dotnet-ef`)

Instalação
----------
1. Clone o repositório:

2. Ajuste a connection string em `appsettings.Development.json` para o banco que irá usar.

3. Restaurar pacotes e aplicar migrações:

dotnet restore dotnet ef database update

Executando localmente com Docker Desktop (banco) e Visual Studio (API via HTTPS)
-------------------------------------------------------------------------------
- Exemplo: iniciar SQL Server em container:
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Your_password123' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest

- Configure a connection string para apontar para `localhost` e porta mapeada.
- Abra a solução no Visual Studio e execute a API pelo perfil padrão (__F5__). A aplicação roda via HTTPS (Kestrel/IIS Express) e conecta ao banco no container Docker.

Connection strings de exemplo
- SQL Server (Docker / local)

Server=localhost,1433;Database=BankDevTrailDb;User Id=sa;Password=Your_password123;TrustServerCertificate=True;

Documentação e teste (Swagger)
------------------------------
- Swagger UI estará disponível em:
- https://localhost:{port}/swagger

Use-o para testar endpoints e visualizar modelos.

Endpoints principais
-------------------
- POST `/api/clientes` — criar cliente
- POST `/api/contas` — abrir conta
- GET `/api/contas/{numero}` — obter conta
- PUT `/api/contas/{numero}/deposito` — depósito (body: `{ "valor": 100 }`)
- POST `/api/contas/{numero}/saque` — saque (body: `{ "valor": 50 }`)
- POST `/api/contas/{numero}/transferencia` — transferência (body: `{ "numeroDestino": "B1", "valor": 30 }`) — `{numero}` é a conta origem
- GET `/api/contas/{numero}/extrato` — extrato (todas as transações da conta)