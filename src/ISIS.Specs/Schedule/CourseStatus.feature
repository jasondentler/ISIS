Feature: Course Status
	In order to control registration for sections
	As a scheduler
	I want to manage the status of courses

@domain
Scenario: Activate an inactive course
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have deactivated the course
	When I activate the course
	Then the course is active
	And it should do nothing else

@domain
Scenario: Activate an already-active course
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	When I activate the course
	Then it should do nothing

@domain
Scenario: Deactivate a course
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	When I deactivate the course
	Then the course is inactive
	And it should do nothing else

@domain
Scenario: Deactivate an already-inactive course
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have deactivated the course
	When I deactivate the course
	Then it should do nothing

@domain
Scenario: Make a course pending
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	When I make the course pending
	Then the course is pending
	And it should do nothing else

@domain
Scenario: Make an already-pending course pending
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have made the course pending
	When I make the course pending
	Then it should do nothing

@domain
Scenario: Make a course obsolete
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	When I make the course obsolete
	Then the course is obsolete
	And it should do nothing else

@domain
Scenario: Make an already-obsolete course obsolete
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have made the course obsolete
	When I make the course obsolete
	Then it should do nothing

