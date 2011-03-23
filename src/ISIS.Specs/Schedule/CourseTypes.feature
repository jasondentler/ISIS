Feature: Track Course Types
	In order to properly integrate with Datatel
	As a scheduler
	I want to set the course type

@domain
Scenario: Create a credit course without a course type
	When I create a course BIOL 1301 Introductory Biology without a course type
	Then the command is invalid
	And the error is "You must select at least one course type."

@domain
Scenario: Create a credit course with duplicate course types
	When I create an ACAD ACAD course BIOL 1301 Introductory Biology
	Then the current course type is ACAD
	And the ACAD course type is added

@domain
Scenario: Add a new course type
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	When I add the NF course type
	Then the NF course type is added
	And the current course types are ACAD and NF
	And it should do nothing else

@domain
Scenario: Add an existing course type
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	When I add the ACAD course type
	Then it should do nothing

@domain
Scenario: Removing a course type
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have added the NF course type
	When I remove the ACAD course type
	Then the ACAD course type is removed
	And the current course type is NF
	And it should do nothing else

@domain
Scenario: Removing a non-existent course type
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	When I remove the NF course type
	Then it should do nothing

@domain
Scenario: Removing the last course type
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	When I remove the ACAD course type
	Then the aggregate state is invalid
	And the error is "Your attempt to remove the course type failed because it's the last one. Each course must have at least one course type."
