from selenium import webdriver


class WebApp:
    instance = None
    __BASE_URL = "http://localhost:4200"

    @classmethod
    def get_instance(cls):
        if cls.instance is None:
            cls.instance = WebApp()
        return cls.instance

    def __init__(self):
        self.driver = webdriver.Chrome()
        self.driver.implicitly_wait(15)

    def get_driver(self):
        return self.driver

    def load_website(self):
        self.driver.get(WebApp.__BASE_URL)

    def load_page(self, page):
        self.driver.get(f"{WebApp.__BASE_URL}/{page}")

    def maximize(self):
        self.driver.maximize_window()

    def close(self):
        self.driver.close()

    def wait(self, time):
        self.driver.implicitly_wait(time)
        self.driver.current_url


webapp = WebApp.get_instance()
