from time import sleep

from behave import given, when, then

from features.steps.dashboard_step import step_impl_search_for_child, step_impl_login_by_user_type


@given(u'que uma crianca eh apadrinhada com usuario "{user_type}"')
def step_impl(context, user_type):
    step_impl_login_by_user_type(context, user_type)
    step_impl_add_god_parent_to_child(context)


@when(u'apadrinho todos os items de uma crianca')
def step_impl_add_god_parent_to_child(context):
    step_impl_search_for_child(context, 'nogueira')
    show_child_details(context)
    check_all_child_items(context)
    fill_god_parent_form(context, 'Vinicius Avansini', '(19) 98119-2732')
    add_god_parent(context)
    save_god_parents(context)


@when(u'atualizo as informacoes do padrinho')
def step_impl_update_god_parent_details(context):
    click_on_god_parent(context)
    fill_god_parent_form(
        context,
        'Vinicius de Souza Avansini',
        '19 98119-2732'
    )
    add_god_parent(context)
    save_god_parents(context)


@when(u'excluo o padrinho da crianca')
def step_impl_delete_god_parent(context):
    click_on_delete_button(context)
    sleep(2)
    save_god_parents(context)


@then(u'as informacoes devem aparecer na tabela')
def step_impl_verify_if_info_is_on_table(context):
    assert context.child_details_page.is_godparent_table_with_one_row(), True


@then(u'uma janela de aviso de sucesso deve aparecer')
def step_impl_verify_if_message_is_visible(context):
    result = context.messages_popup.is_info_message_visible()
    assert result, True


@then(u'nao deve haver informacoes na tabela de items apadrinhados')
def step_impl(context):
    assert context.child_details_page.get_godparent_table_len() == 0


def show_child_details(context):
    context.child_details_page.click_on_table_row()


def check_all_child_items(context):
    context.child_details_page.click_on_clothes_checkbox()
    context.child_details_page.click_on_shoes_checkbox()
    context.child_details_page.click_on_gift_checkbox()


def fill_god_parent_form(context, godparent_name, godparent_phone):
    context.child_details_page.send_keys_to_godparent_name(godparent_name)
    context.child_details_page.send_keys_to_godparent_phone(godparent_phone)


def add_god_parent(context):
    context.child_details_page.click_on_submit()


def save_god_parents(context):
    context.child_details_page.click_on_save_button()


def click_on_god_parent(context):
    context.child_details_page.click_on_table_row()


def click_on_delete_button(context):
    context.child_details_page.click_on_delete_button()
