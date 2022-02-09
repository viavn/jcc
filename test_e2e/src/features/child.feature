# language: pt

Funcionalidade: Crianca

  @child
  Esquema do Cenario: Apadrinhando todos os itens da crianca
    Dado acesso o sistema com usuario "<user_type>"
    Quando apadrinho todos os items de uma crianca
    Entao as informacoes devem aparecer na tabela
    E uma janela de aviso de sucesso deve aparecer
    Exemplos:
      | user_type |
      | admin     |
      | regular   |

  @child
  Esquema do Cenario: Altera items selecionados do padrinho
    Dado que uma crianca eh apadrinhada com usuario "<user_type>"
    Quando atualizo as informacoes do padrinho
    Entao uma janela de aviso de sucesso deve aparecer
    Exemplos:
      | user_type |
      | admin     |
      | regular   |

  @child
  Esquema do Cenario: Exclui padrinho de uma crianca
    Dado que uma crianca eh apadrinhada com usuario "<user_type>"
    Quando excluo o padrinho da crianca
    Entao uma janela de aviso de sucesso deve aparecer
    E nao deve haver informacoes na tabela de items apadrinhados
    Exemplos:
      | user_type |
      | admin     |
      | regular   |
