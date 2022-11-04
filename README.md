# JCC Apadrinhador

Projeto foi criado com intuito de ajudar e agilizar o cadastro das pessoas que ajudaram
na Campanha de Natal do grupo de jovens JCC, Jovens Caminhando com Cristo, da cidade de Americana-SP.
Tal campanha acontece a mais de 25 anos, onde ajudamos as famílias carentes dos bairros em torno ondem moramos. 
São arrecadadas cestas básicas, leites e em especial, para as crianças, arrecadamos brinquedos, roupas e calçados.

Aqui você vai encontrar: 
* Back-end desenvolvido em **dotnet 3.1**;
* Banco de dados utilizando **Postgres**;
* Front-end em **Angular 13**;
* Testes E2E com **Python** e **Selenium**.

Além do mais, o projeto foi implantado no Heroku.

## Features do sistema
* Login
* Home
  * Visualização de todas as crianças
  * Download de relatório das crianças
* Usuários
  * Cadastro de usuários (administrador e regular)
  * Inativação do usuário
  * Listagem de usuários
  * Troca de senha do usuário logado
* Criança
  * Visualização dos detalhes da criança
  * Cadastro de padrinho/madrinha

docker tag jccapi registry.heroku.com/jccapi/web
docker push registry.heroku.com/jccapi/web
heroku container:release web --app jccapi

docker tag jccfrontend registry.heroku.com/jccfrontend/web
docker push registry.heroku.com/jccfrontend/web
heroku container:release web --app jccfrontend