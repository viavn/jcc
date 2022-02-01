from selenium.common.exceptions import NoSuchElementException

from features.helpers.helper_webapp import webapp
from features.pages.base_page import LoginPage, MessagesResultPage, DashboardPage, ManageAccountsPage, SettingsPage, \
    ChildDetailsPage


def before_all(context):
    context.webapp = webapp
    context.webapp.maximize()
    context.login_page = LoginPage(webapp.driver)
    context.messages_popup = MessagesResultPage(webapp.driver)
    context.dashboard_page = DashboardPage(webapp.driver)
    context.manage_accounts_page = ManageAccountsPage(webapp.driver)
    context.settings_page = SettingsPage(webapp.driver)
    context.child_details_page = ChildDetailsPage(webapp.driver)


def after_scenario(context, scenario):
    try:
        context.dashboard_page.click_logout_button()
        context.webapp.wait(3)
    except NoSuchElementException:
        context.webapp.load_website()


def after_all(context):
    context.webapp.close()
