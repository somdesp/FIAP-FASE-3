[![CI - Actions GITHUB](https://github.com/somdesp/FIAP-FASE-3/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/somdesp/FIAP-FASE-3/actions/workflows/dotnet-desktop.yml)

## 🎖️ Desafio
API de gerenciamento de contatos proposto pela Fase 1 do curso de Arquitetura de Sistemas da FIAP 
com todos os métodos CRUD para criação, consulta, consulta pelo DDD da região, alteração e exclusão.

## 🔑 Usuário e senha para realizar login na API e obter acesso aos métodos:
Login: tester@fiaptest.com.br
Senha: Senha@123

## 🧪 Como testar o projeto

### Antes de realizar os passos abaixo, é necessário instalar:
- SDK .NET 8.0.x
- Visual Studio 2022 ou VS Code
- MS SQL LocalDB

[-] Faça clone do projeto:
https://github.com/somdesp/FIAP-FASE-3.git

Execute o comando abaixo para criar o BD e as tabelas:

```powershell
Update-Database
```

Execute o projeto através do IISExpress ou execute os comandos abaixo:

```powershell
dotnet restore
```

```powershell
dotnet clean
```

```powershell
dotnet run
