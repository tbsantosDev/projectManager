# Gerenciador de Projetos Backend

Este é o backend do **Gerenciador de Projetos**, desenvolvido em **C# .NET 8** com **PostgreSQL**. O objetivo do projeto é gerenciar projetos, tarefas e equipes responsáveis por diferentes tarefas, além de controlar os custos associados a projetos, tarefas e times.

## 📋 Pré-requisitos

Antes de começar, certifique-se de que sua máquina atende aos seguintes requisitos:

- [SDK .NET 8](https://dotnet.microsoft.com/)
- Um banco de dados PostgreSQL configurado
- [Postman](https://www.postman.com/) ou outra ferramenta para testar as APIs (opcional)

## ⚙️ Configuração

### 1. Variáveis de Ambiente

O modelo das Variáveis de Ambiente pode ser consultado no arquivo .env.exemple

### 2. Migrar o Banco de Dados

Certifique-se de que o banco de dados esteja configurado corretamente. Em seguida, execute as migrações para criar as tabelas:

```bash
dotnet ef database update
```

### 3. Rodar o Seeder de Usuário

Para popular o banco de dados com um usuário inicial, execute o seguinte comando:

```bash
dotnet run --seed User
```

### 4. Executar a Aplicação

Inicie o servidor utilizando o seguinte comando:

```bash
dotnet run
```

O backend estará disponível no endereço:

```
http://localhost:5000
```

## ▶️ Funcionalidades

- **Gerenciamento de Projetos:** Criação, edição e exclusão de projetos.
- **Controle de Tarefas:** Atribuição de tarefas a times, definição de prazos e status.
- **Gestão de Times:** Criação de times responsáveis pelas tarefas.
- **Controle de Custos:** Registro dos custos de projetos, tarefas e times.

## 🛠️ Tecnologias Utilizadas

- **[C# .NET 8](https://dotnet.microsoft.com/):** Framework para desenvolvimento de aplicações modernas e performáticas.
- **[Entity Framework Core](https://docs.microsoft.com/ef/):** ORM para gerenciamento do banco de dados.
- **[PostgreSQL](https://www.postgresql.org/):** Sistema de gerenciamento de banco de dados.
- **[JWT](https://jwt.io/):** Implementação de autenticação segura.

## 🤝 Contribuindo

Contribuições são bem-vindas! Se você deseja contribuir com o projeto, siga os passos abaixo:

1. Faça um fork do repositório.
2. Crie uma branch com sua feature ou correção: `git checkout -b minha-feature`.
3. Commit suas mudanças: `git commit -m 'Adicionei uma nova feature'`.
4. Faça um push para a branch: `git push origin minha-feature`.
5. Abra um pull request.

## 📝 Licença

Este projeto está licenciado sob a licença MIT. Consulte o arquivo `LICENSE` para mais informações.

---

Feito com ❤️ por [Thiago Santos](https://github.com/tbsantosDev).
