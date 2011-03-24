Feature: Course CEUs
	In order to manage the course schedule
	As a scheduler
	I want to track CEUs of courses

@domain
Scenario: Change CEUs to a positive number
	Given I have created a Workforce Funded course AGEQ 1091 Routine Management of Equine Health
	When I change the CEUs to 0.70
	Then the CEUs are 0.70
	And it should do nothing else

@domain
Scenario: Change CEUs to a negative number
	Given I have created a Workforce Funded course AGEQ 1091 Routine Management of Equine Health
	When I change the CEUs to -0.10
	Then the command is invalid
	And the error is "CEUs can't be negative"

@domain
Scenario: Change CEUs to an unreasonable number
	Given I have created a Workforce Funded course AGEQ 1091 Routine Management of Equine Health
	When I change the CEUs to 1000.5
	Then the command is invalid
	And the error is "CEUs must be less than 1000"

@domain
Scenario: Change CEUs to zero
	Given I have created a Workforce Funded course AGEQ 1091 Routine Management of Equine Health
	And I have changed the CEUs to 0.70
	When I change the CEUs to 0
	Then the CEUs are 0
	And it should do nothing else

@domain
Scenario: Change CEUs to the same number
	Given I have created a Workforce Funded course AGEQ 1091 Routine Management of Equine Health
	And I have changed the CEUs to 0.70
	When I change the CEUs to 0.70
	Then it should do nothing

@domain
Scenario: CEUs default to zero
	Given I have created a Workforce Funded course AGEQ 1091 Routine Management of Equine Health
	When I change the CEUs to 0
	Then it should do nothing

@domain
Scenario: Credit courses don't use CEUs
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	When I change the CEUs to 0
	Then the aggregate state is invalid
	And the error is "Your attempt to change the CEUs failed because this is a credit course. CEUs may only be set on Continuing Education courses."
