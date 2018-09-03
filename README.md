# restaurante

Projeto de api desenvolvido com Asp.Net Core, Entity framework Core, testes unitários, Repositório genérico, Domain Notificaions, 
Swagger, Value Objects. Para rodar o projeto, é preciso uma conexão com o MySQL e um usuário com permissão de criar a base de dados. Pontos a melhorar: autenticação usando JWT, melhorar retornos da API visando utilizar melhor os recursos do HTTP melhorando também a documentação do swagger.

Para o projeto de front, desenvolvido em AngularJS, abrir o CMD na pasta raiz Restaurante.APP e executar os seguintes comandos.

Para instalar os pacotes

npm i

bower i

O projeto foi automatizado utilizando gulp como task runner.

Para rodar em modo desenv:

gulp serve

Para rodar em modo release:

gulp serve-release

No mode release é criada uma pasta target/www a partir da pasta raiz. Lá é colocado todo o código js minificado e otimizado para produção.

Duvida ou sugestões, entre em contato. Estou a disposição para mais informações.
