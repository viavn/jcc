from behave import given, when, then


@given(u'que tento entrar no sistema com login invalido')
def step_impl(context):
    context.webapp.load_page('login')
    step_impl_input_login(context, 'user-inexisteste')
    step_impl_input_password(context, 'xxxx')
    step_impl_click_login_button(context)


@given(u'acesso o sistema como usuario admin')
def step_impl_given_login_as_admin(context):
    context.webapp.load_page('login')
    step_impl_input_login(context, "admin-test")
    step_impl_input_password(context, "123")
    step_impl_click_login_button(context)


@then(u'deve aparecer os botões disponiveis para usuario admin')
def step_impl(context):
    step_impl_should_show_manage_accounts(context)
    step_impl_should_show_account_config(context)
    step_impl_should_show_logout(context)


@then(u'deve aparecer apenas os botões disponiveis para usuario regular')
def step_impl(context):
    step_impl_should_not_show_manage_accounts(context)
    step_impl_should_show_account_config(context)
    step_impl_should_show_logout(context)


@given(u'acesso o sistema como usuario regular')
def step_impl_login_as_regular(context):
    context.webapp.load_page('login')
    step_impl_input_login(context, "test")
    step_impl_input_password(context, "123")
    step_impl_click_login_button(context)


@when(u'preencher os campos de login com a nova senha')
def step_impl_login_as_regular_with_new_password(context):
    step_impl_input_login(context, "test")
    step_impl_input_password(context, "122")
    step_impl_click_login_button(context)


@given(u'altero a senha do usuario regular')
def step_impl_change_password(context):
    step_impl_login_as_regular(context)
    step_impl_go_to_change_password_screen(context)
    context.settings_page.send_keys_to_password('122')
    context.settings_page.click_submit_button()


@then(u'deve aparecer o botao de gerenciar contas')
def step_impl_should_show_manage_accounts(context):
    result = context.dashboard_page.is_manage_accounts_button_visible()
    assert result, True


@then(u'deve aparecer o botao de configuracoes da conta')
def step_impl_should_show_account_config(context):
    result = context.dashboard_page.is_settings_button_visible()
    assert result, True


@then(u'deve aparecer o botao de logout')
def step_impl_should_show_logout(context):
    result = context.dashboard_page.is_logout_button_visible()
    assert result, True


@then(u'nao deve aparecer o botao de gerenciar contas')
def step_impl_should_not_show_manage_accounts(context):
    result = context.dashboard_page.is_manage_accounts_button_hidden()
    assert result, True


@when(u'acesso o sistema como usuario admin')
def step_impl_login_as_admin(context):
    step_impl_input_login(context, "admin-test")
    step_impl_input_password(context, "123")
    step_impl_click_login_button(context)


@when(u'preencho o campo de login com valor "{login}"')
def step_impl_input_login(context, login):
    context.login_page.send_keys_to_login(login)


@when(u'preencho o campo de senha com valor "{password}"')
def step_impl_input_password(context, password):
    context.login_page.send_keys_to_password(password)


@when(u'clicar no botao entrar')
def step_impl_click_login_button(context):
    context.login_page.click_login_button()


@given(u'acesso a pagina de mudanca de senha')
def step_impl_go_to_change_password_screen(context):
    context.dashboard_page.click_settings_button()
    context.webapp.wait(3)


@given(u'acesso a pagina de dashboard')
def step_impl(context):
    context.webapp.load_page('home')


@given(u'faco logout do sistema')
def step_impl_logout(context):
    context.dashboard_page.click_logout_button()


@when(u'faco logout do sistema')
def step_impl_logout_when(context):
    step_impl_logout(context)


@then(u'devo retornar para pagina de dashboard')
def step_impl(context):
    driver = context.webapp.get_driver()
    assert driver.current_url == 'http://localhost:4200/home'


@then(u'devo retornar para pagina de login')
def step_impl(context):
    driver = context.webapp.get_driver()
    assert driver.current_url == 'http://localhost:4200/login'


@when(u'tento acessar a pagina de gerenciar contas')
def step_impl(context):
    context.webapp.load_page('manage-accounts')
    context.webapp.wait(5)


# -------------------- OLD -------------------- #


@given(u'acesso a pagina de login do sistema')
def step_impl(context):
    context.webapp.load_page('login')
