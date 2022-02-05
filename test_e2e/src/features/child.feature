Feature: Crianca

  @child
  Scenario Outline: Apadrinhando todos os itens da crianca
    Given acesso o sistema com usuario "<user_type>"
    When apadrinho todos os items de uma crianca
    Then as informacoes devem aparecer na tabela
    And uma janela de aviso de sucesso deve aparecer
    Examples:
      | user_type |
      | admin     |
      | regular   |

  @child
  Scenario Outline: Altera items selecionados do padrinho
    Given que uma crianca eh apadrinhada com usuario "<user_type>"
    When atualizo as informacoes do padrinho
    Then uma janela de aviso de sucesso deve aparecer
    Examples:
      | user_type |
      | admin     |
      | regular   |

  @child
  Scenario Outline: Exclui padrinho de uma crianca
    Given que uma crianca eh apadrinhada com usuario "<user_type>"
    When excluo o padrinho da crianca
    Then uma janela de aviso de sucesso deve aparecer
    And nao deve haver informacoes na tabela de items apadrinhados
    Examples:
      | user_type |
      | admin     |
      | regular   |
