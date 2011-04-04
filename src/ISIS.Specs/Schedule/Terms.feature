Feature: Terms
	In order to manage the course schedule
	As a scheduler
	I want to set up terms

@domain
Scenario: Create a term
	When I create the term 211FA Fall 2011 from 8/15/2011 to 12/15/2011 
	Then the term is created
	And the term abbreviation is 211FA
	And the term name is Fall 2011
	And the term start date is 8/15/2011
	And the term end date is 12/15/2011
	And it should do nothing else 
