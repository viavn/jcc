from selenium import webdriver


class WebApp:
    instance = None
    __BASE_URL = "https://ge.globo.com/"

    @classmethod
    def get_instance(cls):
        if cls.instance is None:
            cls.instance = WebApp()
        return cls.instance

    def __init__(self):
        self.driver = webdriver.Chrome()

    def get_driver(self):
        return self.driver

    def load_website(self):
        self.driver.get(WebApp.__BASE_URL)

    def maximize(self):
        self.driver.maximize_window()

    def goto_page(self, page):
        self.driver.get(f"{WebApp.__BASE_URL}{page}")

    def close(self):
        self.driver.close()


webapp = WebApp.get_instance()
