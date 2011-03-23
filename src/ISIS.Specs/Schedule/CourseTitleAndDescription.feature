Feature: Manage course title, long title, and description
	In order to manage the course schedule
	As a scheduler
	I want to change the course title, long title, and description

@domain
Scenario: Create a credit course with a very long title
	When I create an ACAD course BIOL 1301 Very Very Very Very Long Title For Introductory Biology
	Then the course title is Very Very Very Very Long Title
	And the course long title is Very Very Very Very Long Title For Introductory Biology

@domain
Scenario: Change course title when long title matches title
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	When I change the course title to Biology 101
	Then the course title is Biology 101
	And the course long title is Biology 101
	And it should do nothing else

@domain
Scenario: Change course title when long title doesn't match title
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And the course long title is Very Very Very Very Long Title For Introductory Biology
	When I change the course title to Biology 101
	Then the course title is Biology 101
	And it should do nothing else

@domain
Scenario: Change course long title when long title matches title
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	When I change the course long title to Long title of Introductory Biology
	Then the course long title is Long title of Introductory Biology
	And the course title is Long title of Introductory Bio
	And it should do nothing else

@domain
Scenario: Change course long title when long title doesn't match title
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	When I change the course long title to Long title of Introductory Biology
	Then the course long title is Long title of Introductory Biology
	And the course title is Long title of Introductory Bio
	And it should do nothing else

@domain
Scenario: Change course description
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	When I change the course description to Cutting up frogs
	Then the course description is Cutting up frogs
	And it should do nothing else

@domain
Scenario: Set course description to the same description
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have changed the course description to Cutting up frogs
	When I change the course description to Cutting up frogs
	Then it should do nothing

@domain
Scenario: Set course title to the same title
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	When I change the course title to Introductory Biology
	Then it should do nothing

@domain
Scenario: Set course long title to the same long title
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have set the course long title to Long title of Introductory Biology 
	When I change the course long title to Long title of Introductory Biology
	Then it should do nothing
