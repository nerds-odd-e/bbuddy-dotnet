@web
Feature: BudgetQuery
	

@CleanBudgets
Scenario: Query budget within single month
	Given go to query budget page
	And Budget table existed budgets
	| Amount | YearMonth |
	| 30000  | 2017-04   |
	When query from "2017-04-05" to "2017-04-14"
	Then show budget 10000.00
