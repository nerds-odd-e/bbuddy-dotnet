@web
Feature: BudgetCreate


Scenario: Add a budget successfully
	Given go to adding budget page
	When I add a buget 2000 for "2017-10"
	Then it should display "added successfully"
