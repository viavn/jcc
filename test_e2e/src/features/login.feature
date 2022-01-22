Feature: Login sistema

  Scenario Outline: Exibir mensagem de erro quando login ou senha sao invalidos
    Given acesso a pagina de login do sistema
    When preencho o campo de login invalido
    And senha invalida
    Then deve aparecer uma mensagem de erro
