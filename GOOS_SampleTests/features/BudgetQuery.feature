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

@CleanBudgets
Scenario: Query budget within 3 month
	Given go to query budget page
	And Budget table existed budgets
	| Amount | YearMonth |
	| 6200  | 2017-03   |
	| 9000  | 2017-04   |
	| 3100  | 2017-05   |
	When query from "2017-03-22" to "2017-05-05"
	Then show budget 11500.00
