from pages.base_page import BasePage
from pages.locators import MessagesResultLocators


class MessagesResultPage(BasePage):
    def is_error_message_visible(self):
        return self.is_element_visible(MessagesResultLocators.ERROR_MESSAGE)

    def is_error_message_hidden(self):
        return self.is_element_hidden(MessagesResultLocators.ERROR_MESSAGE)

    def is_info_message_visible(self):
        return self.is_element_visible(MessagesResultLocators.INFO_MESSAGE)