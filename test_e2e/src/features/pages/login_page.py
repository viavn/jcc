from features.pages.base_page import BasePage
from features.pages.locators import LoginPageLocators


class LoginPage(BasePage):
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
