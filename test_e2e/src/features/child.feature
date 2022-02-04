Feature: Crianca

  Scenario Outline: Adiciona presente
    Given acesso a pagina de login do sistema
    When acesso o sistema como usuario admin
    And pesquiso por "<filter_query>" na tabela de criancas
    And visualizo o detalhe da crianca
    And seleciono todos items da crianca
    And preencho o formulario com "<godparent_name>" "<godparent_phone>"
    And clico no botao de adicionar
    And clico no botao de salvar
    Then as informacoes devem aparecer na tabela
    And uma janela de aviso de sucesso deve aparecer
    And devo excluir o registro criado
    Examples:
      | filter_query | godparent_name    | godparent_phone |
      | nogueira     | Vinicius Avansini | (19) 98119-2732 |
      # TODO: adicionar mais algum exemplo ou remover


  Scenario Outline: Altera presente
    Given acesso a pagina de login do sistema
    When acesso o sistema como usuario admin
    And pesquiso por "<filter_query>" na tabela de criancas
    And visualizo o detalhe da crianca
    And seleciono todos items da crianca
    And preencho o formulario com "<godparent_name>" "<godparent_phone>"
    And clico no botao de adicionar
    And clico no botao de salvar
    And clico na linha do padrinho
    And preencho o formulario com "Vini2" "(xx) XXXXX-XXXX"
    And clico no botao de atualizar
    And clico no botao de salvar
    Then uma janela de aviso de sucesso deve aparecer
    And devo excluir o registro criado
    Examples:
      | filter_query | godparent_name    | godparent_phone |
      | nogueira     | Vinicius Avansini | (19) 98119-2732 |


  Scenario Outline: Exclui presente
    Given acesso a pagina de login do sistema
    When acesso o sistema como usuario admin
    And pesquiso por "<filter_query>" na tabela de criancas
    And visualizo o detalhe da crianca
    And seleciono todos items da crianca
    And preencho o formulario com "<godparent_name>" "<godparent_phone>"
    And clico no botao de adicionar
    And clico no botao de salvar
    And clico no botao excluir padrinho
    And clico no botao de salvar
    Then uma janela de aviso de sucesso deve aparecer
    And nao deve haver informacoes na tabela de items apadrinhados
    And devo excluir o registro criado
    Examples:
      | filter_query | godparent_name    | godparent_phone |
      | nogueira     | Vinicius Avansini | (19) 98119-2732 |
