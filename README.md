# Digital Innovation One

## Construindo um projeto de uma ASP.NET Core API integrada ao MongoDB

Projeto de uma *ASP.NET Core API Application* integrada a um cluster MongoDB no Atlas (Cloud DBaaS for MongoDB).

[🖱 Clique aqui para ver online!][heroku-deploy]

---

### ⚙ Projeto ASP.NET Core API

1 - O projeto ASP.NET Core API foi criado através do seguinte comando:

```console
dotnet new api -n <project-name>
```

2 - Depois de iniciar um repositório **Git** na pasta do projeto, foi gerado um arquivo *.gitignore* através do seguinte comando:

```console
dotnet new gitignore
```

3 - A dependência **MongoDB Driver** foi instalada no projeto através do seguinte comando:

```console
dotnet add package MongoDB.Driver
```

Depois de concluído, o projeto pode ser executado localmente com o seguinte comando:

```console
dotnet run
```

Acesse o projeto através do endereço local abaixo:

```console
https://localhost:5001/api/v1/infectado
```

---

### ⚙ REST API

| Endpoint: | Method: | Descrição: | Response Status Code: |
|-----------|---------|--------------|-----------------------|
| /infectado | GET  | Retorna uma lista com todos os registros. | 200 / 404 |
| /infectado/{id} | GET | Retorna um registro específico. | 200 / 404 |
| /infectado | POST | Cria um novo registro no sistema. | 201 / 400 |
| /infectado/{id} | PUT | Edita um registro específico. | 200 / 404 |
| /infectado/{id} | DELETE | Deleta um registro específico. | 204 / 404 |

---

### 🛠 Heroku Deploy

O projeto foi publicado em um container no **Heroku: Cloud Application Platform** conforme os passos abaixo:

#### Docker Image

1 - Um arquivo *Dockerfile* foi criado na raiz do projeto com as instruções para a geração da imagem **Docker**.
2 - A imagem **Docker** do projeto foi gerada através do seguinte comando:

```console
docker build -t <nome-da-imagem> .
```

#### Heroku Deploy

1 - Foi efetuado login na conta **Heroku** utilizando o seguinte comando da [Heroku CLI][heroku-cli]:

```console
heroku login
```

2 - Um app foi criado no [Heroku Dashboard][heroku-dash] para a publicação do projeto na plataforma.

3 - Foi efetuado o login no *Container Registry* do **Heroku** com o seguinte comando:

```console
heroku container:login
```

4 - A imagem **Docker** que foi gerada a partir do arquivo *Dockerfile* foi enviada para um container no **Heroku** através do seguinte comando:

```console
heroku container:push -a <heroku-app-name> web
```

5 - O *release* da imagem **Docker** enviada foi realizado através do comando abaixo:

```console
heroku container:release -a <heroku-app-name> web
```

⚠ Importante: Esse *app container* (free dyno) entra em modo *sleep* depois de 30 minutos de inatividade. 😴

---

### 📚 Referências

- [Microsoft .NET Core](https://docs.microsoft.com/en-us/dotnet/)
- [MongoDB Atlas](https://www.mongodb.com/cloud/atlas)
- [MongoDB Driver](https://docs.mongodb.com/drivers/csharp)
- [Docker](https://www.docker.com/)
- [Heroku](https://www.heroku.com/)

[heroku-deploy]:https://dio-aspnet-core-api.herokuapp.com/api/v1/infectado
[heroku-cli]:https://devcenter.heroku.com/articles/heroku-cli#download-and-install
[heroku-dash]:https://dashboard.heroku.com/apps
