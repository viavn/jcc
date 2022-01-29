from behave import given, when, then


@given(u'acesso a pagina de login do sistema')
def step_impl(context):
    context.webapp.load_page('login')


@given(u'acesso o sistema como usuario regular')
def step_impl(context):
    step_impl_input_login(context, "test")
    step_impl_input_password(context, "123")
    step_impl_click_login_button(context)


@given(u'acesso a pagina de dashboard')
def step_impl(context):
    context.webapp.load_page('home')


@given(u'acesso a pagina de mudanca de senha')
def step_impl(context):
    context.dashboard_page.click_settings_button()
    context.webapp.wait(3)


@given(u'altero a senha do usuario regular para "{password}"')
def step_impl(context, password):
    context.settings_page.send_keys_to_password(password)
    context.settings_page.click_submit_button()


@given(u'faco logout do sistema')
def step_impl(context):
    context.dashboard_page.click_logout_button()


@when(u'preencho o campo de login com valor "{login}"')
def step_impl_input_login(context, login):
    context.login_page.send_keys_to_login(login)


@when(u'preencho o campo de senha com valor "{password}"')
def step_impl_input_password(context, password):
    context.login_page.send_keys_to_password(password)


@when(u'acesso o sistema como usuario admin')
def step_impl(context):
    step_impl_input_login(context, "admin-test")
    step_impl_input_password(context, "123")
    step_impl_click_login_button(context)


@when(u'acesso o sistema como usuario regular')
def step_impl(context):
    step_impl_input_login(context, "test")
    step_impl_input_password(context, "123")
    step_impl_click_login_button(context)


@when(u'clicar no botao entrar')
def step_impl_click_login_button(context):
    context.login_page.click_login_button()


@when(u'tento acessar a pagina de gerenciar contas')
def step_impl(context):
    context.webapp.load_page('manage-accounts')
    context.webapp.wait(5)


@then(u'deve aparecer o botao de gerenciar contas')
def step_impl(context):
    result = context.dashboard_page.is_manage_accounts_button_visible()
    assert result, True


@then(u'nao deve aparecer o botao de gerenciar contas')
def step_impl(context):
    result = context.dashboard_page.is_manage_accounts_button_hidden()
    assert result, True


@then(u'deve aparecer o botao de configuracoes da conta')
def step_impl(context):
    result = context.dashboard_page.is_settings_button_visible()
    assert result, True


@then(u'deve aparecer o botao de logout')
def step_impl(context):
    result = context.dashboard_page.is_logout_button_visible()
    assert result, True


@then(u'devo retornar para pagina de dashboard')
def step_impl(context):
    driver = context.webapp.get_driver()
    assert driver.current_url == 'http://localhost:4200/home'


@then(u'devo retornar para pagina de login')
def step_impl(context):
    driver = context.webapp.get_driver()
    assert driver.current_url == 'http://localhost:4200/login'
