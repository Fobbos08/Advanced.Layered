Feature: Category

Scenario: One category should be added
	Given the Category Name: category1 ImageUrl: http://some.url
	When defined categories added
	Then should exist 1 categories
	And first category Name is category1

Scenario: Category shouldn't be added and validation error should appear
	Given the Category Name: TooLondCategoryNameTooLondCategoryNameTooLondCategoryNameTooLondCategoryNameTooLondCategoryName
	When defined categories added
	Then should exist 0 categories
	And should exist 1 validation errors

Scenario: Two categories should be added
	Given the Category Name: category1 ImageUrl: http://some.url
	And the Category Name: category2 ImageUrl: http://some.url
	When defined categories added
	Then should exist 2 categories