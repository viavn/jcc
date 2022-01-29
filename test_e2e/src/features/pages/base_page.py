from selenium.common.exceptions import NoSuchElementException

from features.pages.element import BasePageElement
from features.pages.locators import LoginPageLocators, DashboardPageLocators, MessagesResultLocators, \
    ManageAccountsPageLocators, SettingsPageLocators


class InputTextElement(BasePageElement):
    locator = 'q'


class BasePage(object):
    def __init__(self, driver):
        self.driver = driver

    def is_element_visible(self, locator):
        element = self.driver.find_element(*locator)
        return element is not None

    def is_element_hidden(self, locator):
        try:
            return not self.is_element_visible(locator)
        except NoSuchElementException:
            return True
        except Exception as e:
            raise e


class LoginPage(BasePage):
    login_input_element = InputTextElement()

    def send_keys_to_login(self, value):
        element = self.driver.find_element(*LoginPageLocators.LOGIN_INPUT)
        element.clear()
        element.send_keys(value)

    def send_keys_to_password(self, value):
        element = self.driver.find_element(*LoginPageLocators.PASSWORD_INPUT)
        element.clear()
        element.send_keys(value)

    def click_login_button(self):
        element = self.driver.find_element(*LoginPageLocators.LOGIN_BUTTON)
        element.click()


class DashboardPage(BasePage):
    def is_manage_accounts_button_hidden(self):
        return self.is_element_hidden(DashboardPageLocators.MANAGE_ACCOUNTS_BUTTON)

    def is_manage_accounts_button_visible(self):
        return self.is_element_visible(DashboardPageLocators.MANAGE_ACCOUNTS_BUTTON)

    def is_settings_button_visible(self):
        return self.is_element_visible(DashboardPageLocators.SETTINGS_BUTTON)

    def is_logout_button_visible(self):
        return self.is_element_visible(DashboardPageLocators.LOGOUT_BUTTON)

    def click_logout_button(self):
        element = self.driver.find_element(*DashboardPageLocators.LOGOUT_BUTTON)
        element.click()

    def click_settings_button(self):
        element = self.driver.find_element(*DashboardPageLocators.SETTINGS_BUTTON)
        element.click()

    def click_manage_accounts_button(self):
        element = self.driver.find_element(*DashboardPageLocators.MANAGE_ACCOUNTS_BUTTON)
        element.click()

    def send_keys_to_filter(self, value):
        element = self.driver.find_element(*DashboardPageLocators.TABLE_FILTER)
        element.send_keys(value)

    def is_family_acronym_equals(self, value):
        element = self.driver.find_element(*DashboardPageLocators.FAMILY_ACRONYM)
        return element.text == value

    def is_child_name_equals(self, value):
        element = self.driver.find_element(*DashboardPageLocators.CHILD_NAME)
        return element.text == value

    def is_legal_responsible_equals(self, value):
        element = self.driver.find_element(*DashboardPageLocators.LEGAL_RESPONSIBLE)
        return element.text == value


class ManageAccountsPage(BasePage):
    def click_new_account_button(self):
        element = self.driver.find_element(*ManageAccountsPageLocators.NEW_USER_BUTTON)
        element.click()

    def send_keys_to_login(self, value):
        element = self.driver.find_element(*ManageAccountsPageLocators.LOGIN_INPUT)
        element.send_keys(value)

    def send_keys_to_name(self, value):
        element = self.driver.find_element(*ManageAccountsPageLocators.NAME_INPUT)
        element.send_keys(value)

    def send_keys_to_password(self, value):
        element = self.driver.find_element(*ManageAccountsPageLocators.PASSWORD_INPUT)
        element.send_keys(value)

    def select_user_type(self, value):
        elements = self.driver.find_elements(*ManageAccountsPageLocators.USER_TYPE_RADIO_BUTTON)
        for element in elements:
            if element.text == value:
                element.click()
                break

    def click_submit_button(self):
        element = self.driver.find_element(*ManageAccountsPageLocators.SUBMIT_BUTTON)
        element.click()

    def send_keys_to_filter(self, value):
        element = self.driver.find_element(*ManageAccountsPageLocators.TABLE_FILTER)
        element.send_keys(value)

    def is_login_equals(self, value):
        element = self.driver.find_element(*ManageAccountsPageLocators.LOGIN_CELL)
        return element.text == value

    def is_child_name_equals(self, value):
        element = self.driver.find_element(*ManageAccountsPageLocators.NAME_CELL)
        return element.text == value

    def is_admin_equals(self, value):
        element = self.driver.find_element(*ManageAccountsPageLocators.ADMIN_CELL)
        return element.text == value

    def is_disabled_equals(self, value):
        element = self.driver.find_element(*ManageAccountsPageLocators.DISABLED_CELL)
        return element.text == value


class SettingsPage(BasePage):
    def send_keys_to_password(self, value):
        element = self.driver.find_element(*SettingsPageLocators.PASSWORD_INPUT)
        element.send_keys(value)

    def click_submit_button(self):
        element = self.driver.find_element(*SettingsPageLocators.SUBMIT_BUTTON)
        element.click()


class MessagesResultPage(BasePage):
    def is_error_message_visible(self):
        return self.is_element_visible(MessagesResultLocators.ERROR_MESSAGE)

    def is_error_message_hidden(self):
        return self.is_element_hidden(MessagesResultLocators.ERROR_MESSAGE)
