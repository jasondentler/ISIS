Feature: Create A Credit Course
	As a scheduler
	I want to set up a new credit course

@domain
Scenario: Create a credit course
	When I create an ACAD course BIOL 1301 Introductory Biology
	Then the course should be created
	And the course rubric should be BIOL
	And the course number should be 1301
	And the course title should be Introductory Biology
	And the course long title should be Introductory Biology
	And the course should be active
	And the course type should be ACAD
	And it should do nothing else