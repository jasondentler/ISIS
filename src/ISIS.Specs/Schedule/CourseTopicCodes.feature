Feature: Course Topic Codes
	In order to manage the course schedule
	As a scheduler
	I want to track a course's topic code

@domain
Scenario: Create a topic code
	When I create a topic code BANK Banking/Finance
	Then the topic code BANK is created
	And the topic code description is Banking/Finance
	And it should do nothing else

@domain
Scenario: Change the topic code abbreviation
	Given I have created a topic code BANK Banking/Finance
	When I change the topic code abbreviation to MONEY
	Then the topic code abbreviation is MONEY
	And the previous topic code abbreviation is BANK
	And it should do nothing else

@domain
Scenario: Change the topic code description
	Given I have created a topic code BANK Banking/Finance
	When I change the topic code description to Theft
	Then the topic code description is Theft
	And the previous topic code description is Banking/Finance
	And it should do nothing else

@domain
Scenario: Change the topic code on a CE course
	Given I have created a topic code BANK Banking/Finance
	And I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	When I change the courses's topic code to BANK
	Then the course's topic code is BANK

@domain
Scenario: Change the topic code on a CE course to the same topic code
	Given I have created a topic code BANK Banking/Finance
	And I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	And I have changed the course's topic code to BANK Banking/Finance
	When I change the courses's topic code to BANK
	Then it should do nothing
