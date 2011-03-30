Feature: Track Course CIP and Approval Number
	In order to properly integrate with Datatel
	As a scheduler
	I want to set the course CIP and approval number

@domain
Scenario: Set CIP on a new course
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	When I set the CIP to 123456
	Then the CIP is 123456
	And it should do nothing else

@domain
Scenario: Set approval number on a new course
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	When I set the approval number to 1234567890
	Then the approval number is 1234567890
	And the CIP is 123456
	And it should do nothing else

@domain
Scenario: Set CIP on a course with an approval number
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have set the approval number to 1234567890
	When I set the CIP to 111111
	Then the CIP is 111111
	And the approval number is blank
	And it should do nothing else

@domain
Scenario: The same approval number is assigned
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have set the approval number to 1234567890
	When I set the approval number to 1234567890
	Then it should do nothing

@domain
Scenario: The same CIP is assigned
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have set the CIP to 123456
	When I set the CIP to 123456
	Then it should do nothing
	
@domain
Scenario: Set approval number on a course with a CIP
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have set the CIP to 123456
	When I set the approval number to 1111111111
	Then the CIP is 111111
	And the approval number is 1111111111
	And it should do nothing else

@domain
Scenario: The same CIP is assigned after an approval number
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have set the approval number to 1234567890
	When I set the CIP to 123456
	Then the approval number is blank
	And it should do nothing else

