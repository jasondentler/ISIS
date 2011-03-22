Feature: Create A Credit Course
	As a scheduler
	I want to set up a new credit course

@domain
Scenario: Create a credit course
	When I create an ACAD course BIOL 1301 Introductory Biology
	Then the course is created
	And the course rubric is BIOL
	And the course number is 1301
	And the course title is Introductory Biology
	And the course long title is Introductory Biology
	And the course is active
	And the course type is ACAD
	And it should do nothing else

@domain
Scenario: Create a credit course without a course type
	When I create a course BIOL 1301 Introductory Biology without a course type
	Then the command is invalid

