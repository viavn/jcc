from pages.base_page import BasePage
from pages.locators import ChildDetailsPageLocators, BaseLocators


class ChildDetailsPage(BasePage):
    def click_on_save_button(self):
        element = self.driver.find_element(*ChildDetailsPageLocators.SAVE_BUTTON)
        element.click()

    def click_on_clothes_checkbox(self):
        element = self.driver.find_element(*ChildDetailsPageLocators.CLOTHES_CHECKBOX)
        parent = element.find_element(*ChildDetailsPageLocators.CHECKBOX_PARENT)
        parent.click()

    def click_on_shoes_checkbox(self):
        element = self.driver.find_element(*ChildDetailsPageLocators.SHOES_CHECKBOX)
        parent = element.find_element(*ChildDetailsPageLocators.CHECKBOX_PARENT)
        parent.click()

    def click_on_gift_checkbox(self):
        element = self.driver.find_element(*ChildDetailsPageLocators.GIFT_CHECKBOX)
        parent = element.find_element(*ChildDetailsPageLocators.CHECKBOX_PARENT)
        parent.click()

    def click_on_submit(self):
        element = self.driver.find_element(*ChildDetailsPageLocators.SUBMIT_BUTTON)
        element.click()

    def click_on_delete_button(self):
        element = self.driver.find_element(*ChildDetailsPageLocators.DELETE_BUTTON)
        element.click()

    def send_keys_to_godparent_name(self, value):
        element = self.driver.find_element(*ChildDetailsPageLocators.GODPARENT_NAME_INPUT)
        element.clear()
        element.send_keys(value)

    def send_keys_to_godparent_phone(self, value):
        element = self.driver.find_element(*ChildDetailsPageLocators.GODPARENT_PHONE_INPUT)
        element.clear()
        element.send_keys(value)

    def get_godparent_table_len(self):
        elements = self.driver.find_elements(*BaseLocators.MAT_ROW)
        return len(elements)

    def is_godparent_table_with_one_row(self):
        return self.get_godparent_table_len() == 1
