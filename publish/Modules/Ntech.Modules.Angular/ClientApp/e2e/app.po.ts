import { browser, by, element } from 'protractor';

export class AppPage {
  navigateTo() {
    return browser.get('/Angular');
  }

  getMainHeading() {
    return element(by.css('app-root h1')).getText();
  }
}
