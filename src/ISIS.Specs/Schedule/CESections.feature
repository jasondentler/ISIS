Feature: CE Sections
	In order to manage the course schedule
	As a scheduler
	I want to manage sections

@domain
Scenario: Create a CE section without a topic code
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	And I have changed the approval number to 1234567890
	And I have created a term CE211Q1 CE Q1 2011 from 9/1/2011 to 11/30/2011
	When I create section 01 from the course with term CE211Q1
	Then the section's rubric is AGEQ
	And the section's course number is 1091
	And the section's section number is 01
	And the section's term is CE211Q1
	And the section's start date is not set
	And the section's end date is not set
	And the section's title is "Routine Management of Equine H"
	And the section's credit type is Workforce Funded
	And the section's course type is CWECM
	And the section's current course type is CWECM
	And the section's approval number is 1234567890
	And the section's CIP is 123456
	And the section's status is pending
	And the location is blank
	And the section's CEUs is 0
	And it should do nothing else

@domain
Scenario: Create a CE section with a topic code
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	And I have changed the approval number to 1234567890
	And I have created a topic code BANK Banking/Finance
	And I have changed the course's topic code to BANK Banking/Finance
	And I have created a term CE211Q1 CE Q1 2011 from 9/1/2011 to 11/30/2011
	When I create section 01 from the course with term CE211Q1
	Then the section's topic code is BANK

@domain
Scenario: Can create a CE section from a special interests course without an approval or CIP
	Given I have created a Special Interests course AGEQ 1091 "Routine Management of Equine Health"
	And I have created a term CE211Q1 CE Q1 2011 from 9/1/2011 to 11/30/2011
	When I create section 01 from the course with term CE211Q1
	Then the section is created

@domain
Scenario: Cant create a CE section from a non-special interests course without an approval or CIP
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	And I have created a term CE211Q1 CE Q1 2011 from 9/1/2011 to 11/30/2011
	When I create section 01 from the course with term CE211Q1
	Then the aggregate state is invalid
	And the error is "Your attempt to create a section failed. The course doesn't have an approval number or CIP, and it's not a special interests course."

@domain
Scenario: Change the section number
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	And I have changed the approval number to 1234567890
	And I have created a term CE211Q1 CE Q1 2011 from 9/1/2011 to 11/30/2011
	And I have created a section 01 from the course with term CE211Q1
	When I change the section number to 02
	Then the section number is 02
	And it should do nothing else

@domain
Scenario: Change the section number when the section number is taken
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	And I have changed the approval number to 1234567890
	And I have created a term CE211Q1 CE Q1 2011 from 9/1/2011 to 11/30/2011
	And I have created a section 01 from the course with term CE211Q1
	And I have created a section 02 from the course with term CE211Q1
	When I change the section number to 02
	Then the command is invalid
	And the error is "Your attempt to create a section failed. That section number is already used."

@domain
Scenario: Change the section term
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	And I have changed the approval number to 1234567890
	And I have created a term CE211Q1 CE Q1 2011 from 9/1/2011 to 11/30/2011
	And I have created a term CE211Q2 CE Q2 2011 from 12/1/2011 to 2/28/2012
	And I have created a section 01 from the course with term CE211Q1
	When I change the section's term to CE211Q2
	Then the section's term is CE211Q2
	And it should do nothing else

@domain
Scenario: Change the section term when the section number is taken
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	And I have changed the approval number to 1234567890
	And I have created a term CE211Q1 CE Q1 2011 from 9/1/2011 to 11/30/2011
	And I have created a term CE211Q2 CE Q2 2011 from 12/1/2011 to 2/28/2012
	And I have created a section 01 from the course with term CE211Q1
	And I have created a section 01 from the course with term CE211Q2
	When I change the section's term to CE211Q2
	Then the command is invalid
	And the error is "Your attempt to create a section failed. That section number is already used."

@domain
Scenario: Change the section dates
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	And I have changed the approval number to 1234567890
	And I have changed the approval number to 1234567890
	And I have created a term CE211Q1 CE Q1 2011 from 9/1/2011 to 11/30/2011
	And I have created a section 01 from the course with term CE211Q1
	When I change the start date to 10/1/2011 and the end date to 11/1/2011
	Then the section start date is 10/1/2011
	And the section end date is 11/1/2011
	And it should do nothing else

@domain
Scenario: Change the section dates where the section dates are before the term
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	And I have changed the approval number to 1234567890
	And I have changed the approval number to 1234567890
	And I have created a term CE211Q1 CE Q1 2011 from 9/1/2011 to 11/30/2011
	And I have created a section 01 from the course with term CE211Q1
	When I change the start date to 7/1/2011 and the end date to 8/1/2011
	Then the aggregate state is invalid
	And the error is "Your attempt to create a section failed. The section census date is outside the term dates."

@domain
Scenario: Change the section dates where the section dates are after the term
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	And I have changed the approval number to 1234567890
	And I have changed the approval number to 1234567890
	And I have created a term CE211Q1 CE Q1 2011 from 9/1/2011 to 11/30/2011
	And I have created a section 01 from the course with term CE211Q1
	When I change the start date to 12/1/2011 and the end date to 12/25/2011
	Then the aggregate state is invalid
	And the error is "Your attempt to create a section failed. The section census date is outside the term dates."

@domain
Scenario: Change the section location
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	And I have changed the approval number to 1234567890
	And I have created a term CE211Q1 CE Q1 2011 from 9/1/2011 to 11/30/2011
	And I have created a section 01 from the course and term
	And I have created the location ACC Main Campus
	When I change the section location to ACC
	Then the section location is ACC
	And it should do nothing else

@domain
Scenario: Change the section location to the same location
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	And I have changed the approval number to 1234567890
	And I have created a term CE211Q1 CE Q1 2011 from 9/1/2011 to 11/30/2011
	And I have created a section 01 from the course and term
	And I have created the location ACC Main Campus
	And I have set the section location to ACC Main Campus
	When I change the section location to ACC
	Then it should do nothing
	
@domain
Scenario: Changing the location of a CE section to a TDCJ location doesn't change the topic code
	Given I have created a Workforce Non-Funded course AGEQ 1091 "Routine Management of Equine Health"
	And I have changed the approval number to 1234567890
	And I have created a term CE211Q1 CE Q1 2011 from 9/1/2011 to 11/30/2011
	And I have created the topic code A Academic TDC Course Code
	And I have created the location CLEM Clemson Unit
	And I have created a section 01 from the course and term
	When I change the section's location to CLEM
	Then the section's location should be CLEM
	And it should do nothing else 


@domain
Scenario: Change the section credit type to non-funded
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	And I have changed the approval number to 1234567890
	And I have created a term CE211Q1 CE Q1 2011 from 9/1/2011 to 11/30/2011
	And I have created a section 01 from the course and term
	When I change the section credit type to Workforce Non-Funded
	Then the section credit type should be Workforce Non-Funded
	And the course type CE is added to the section
	And the course type CWECM is removed from the section
	And the section's current course type is CE
	And it should do nothing else

@domain
Scenario: Change the section credit type to funded
	Given I have created a Workforce Non-Funded course AGEQ 1091 "Routine Management of Equine Health"
	And I have changed the approval number to 1234567890
	And I have created a term CE211Q1 CE Q1 2011 from 9/1/2011 to 11/30/2011
	And I have created a section 01 from the course and term
	When I change the section credit type to Workforce Funded
	Then the section credit type should be Workforce Funded
	And the course type CWECM is added to the section
	And the course type CE is removed from the section
	And the section's current course type is CWECM
	And it should do nothing else

@domain
Scenario: Change the section's CEUs
	Given I have created a Workforce Non-Funded course AGEQ 1091 "Routine Management of Equine Health"
	And I have changed the approval number to 1234567890
	And I have created a term CE211Q1 CE Q1 2011 from 9/1/2011 to 11/30/2011
	And I have created a section 01 from the course and term
	When I change the section's CEUs to 0.70
	Then the section's CEUs is 0.70
	And it should do nothing else

@domain
Scenario: Change the section's title
	Given I have created a Workforce Non-Funded course AGEQ 1091 "Routine Management of Equine Health"
	And I have changed the approval number to 1234567890
	And I have created a term CE211Q1 CE Q1 2011 from 9/1/2011 to 11/30/2011
	And I have created a section 01 from the course and term
	When I change the section's title to Horses
	Then the section's title is Horses
	And it should do nothing else
