from time import sleep

from behave import then


@then(u'deve aparecer uma mensagem de erro')
def step_impl(context):
    result = context.messages_popup.is_error_message_visible()
    assert result, True


@then(u'a mensagem de erro deve desaparecer')
def step_impl(context):
    sleep(5)
    result = context.messages_popup.is_error_message_hidden()
    assert result, True

