from time import sleep

from behave import when, then

from repository import queries


@when(u'visualizo o detalhe da crianca')
def step_impl(context):
    context.child_details_page.click_on_table_row()


@when(u'seleciono todos items da crianca')
def step_impl(context):
    context.child_details_page.click_on_clothes_checkbox()
    context.child_details_page.click_on_shoes_checkbox()
    context.child_details_page.click_on_gift_checkbox()


@when(u'preencho o formulario com "{godparent_name}" "{godparent_phone}"')
def step_impl(context, godparent_name, godparent_phone):
    context.child_details_page.send_keys_to_godparent_name(godparent_name)
    context.child_details_page.send_keys_to_godparent_phone(godparent_phone)
    sleep(2)


@when(u'clico no botao de adicionar')
def step_impl(context):
    context.child_details_page.click_on_submit()


@when(u'clico no botao de atualizar')
def step_impl(context):
    context.child_details_page.click_on_submit()


@when(u'clico no botao de salvar')
def step_impl(context):
    context.child_details_page.click_on_save_button()


@then(u'as informacoes devem aparecer na tabela')
def step_impl(context):
    assert context.child_details_page.is_godparent_table_with_one_row(), True


@then(u'uma janela de aviso de sucesso deve aparecer')
def step_impl(context):
    result = context.messages_popup.is_info_message_visible()
    assert result, True


@when(u'clico na linha do padrinho')
def step_impl(context):
    context.child_details_page.click_on_table_row()


@when(u'clico no botao excluir padrinho')
def step_impl(context):
    sleep(3)
    context.child_details_page.click_on_delete_button()


@then(u'devo excluir o registro criado')
def step_impl(context):
    child = queries.get_child_by_name('Jhonatan')
    queries.delete_godparent(child[0])


@then(u'nao deve haver informacoes na tabela de items apadrinhados')
def step_impl(context):
    assert context.child_details_page.get_godparent_table_len() == 0
