Feature: BrowserStackDemo

@Ajay @WEB
Scenario: To verify Login to BrowserStack Demo Application
	Given I am on BrowserStack Demo Home page
	When I click on Sign In button on Home page
	And I add UserName 'demouser' on Sign in page
	And I add Password 'testingisfun99' on Sign in page
	And I click on Log in button on Sign in page
	Then I verify I am on BrowserStack Dashboard page