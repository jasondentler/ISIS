Feature: Credit Sections
	In order to manage the course schedule
	As a scheduler
	I want to manage sections
	
@domain:
Scenario: Change the credit section location
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course and term
	And I have created a location ACC Main Campus
	When I change the section location to ACC
	Then the section location is ACC
	And the section location abbreviation is ACC
	And the section location name is Main Campus
	And it should do nothing else

@domain
Scenario: Change the credit section location to CLEM
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course and term
	And I have created a location CLEM Clemens Unit
	When I change the section location to CLEM
	Then the section location is CLEM
	And the topic code is A Academic TDC Course Code
	And it should do nothing else

@domain
Scenario: Change the credit section location from TDCJ back to non-TDCJ
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course and term
	And I have created a location CLEM Clemens Unit
	And I have created a location ACC Main Campus
	And I have set the section location to CLEM Clemens Unit
	When I change the section location to ACC
	Then the section location is ACC
	And the topic code is blank
	And it should do nothing else

@domain
Scenario: Change the credit section location from one TDCJ to another
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course and term
	And I have created a location CLEM Clemens Unit
	And I have created a location DAR Darrington Unit
	And I have set the section location to CLEM Clemens Unit
	When I change the section location to DAR
	Then the section location is DAR
	And it should do nothing else

@domain
Scenario: Change the credit section location from one non-TDCJ to another
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course and term
	And I have created a location ACC Main Campus
	And I have created a location AHS Alvin High School
	And I have set the section location to ACC Main Campus
	When I change the section location to AHS
	Then the section location is AHS
	And it should do nothing else

@domain
Scenario: Change the credit section location to CV
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course and term
	And I have created a location CV Carol Vance
	When I change the section location to CV
	Then the section location is CV
	And the topic code is A Academic TDC Course Code
	And it should do nothing else

@domain
Scenario: Change the credit section location to DAR
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course and term
	And I have created a location DAR Darrington Unit
	When I change the section location to DAR
	Then the section location is DAR
	And the topic code is A Academic TDC Course Code
	And it should do nothing else

@domain
Scenario: Change the credit section location to J1
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course and term
	And I have created a location J1 Jester 1 Unit
	When I change the section location to J1
	Then the section location is J1
	And the topic code is A Academic TDC Course Code
	And it should do nothing else

@domain
Scenario: Change the credit section location to J2
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course and term
	And I have created a location J2 Jester 2 Unit
	When I change the section location to J2
	Then the section location is J2
	And the topic code is A Academic TDC Course Code
	And it should do nothing else

@domain
Scenario: Change the credit section location to J3
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course and term
	And I have created a location J3 Jester 3 Unit
	When I change the section location to J3
	Then the section location is J3
	And the topic code is A Academic TDC Course Code
	And it should do nothing else

@domain
Scenario: Change the credit section location to TER
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course and term
	And I have created a location TER T. C. Terrell Unit
	When I change the section location to TER
	Then the section location is TER
	And the topic code is A Academic TDC Course Code
	And it should do nothing else

@domain
Scenario: Change the credit section location to R1
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course and term
	And I have created a location R1 Ramsey 1 Unit
	When I change the section location to R1
	Then the section location is R1
	And the topic code is A Academic TDC Course Code
	And it should do nothing else

@domain
Scenario: Change the credit section location to R2
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course and term
	And I have created a location R2 Stringfellow Unit
	When I change the section location to R2
	Then the section location is R2
	And the topic code is A Academic TDC Course Code
	And it should do nothing else

@domain
Scenario: Create a credit section without a topic code
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have set the approval number to 1234567890
	And I have created a term 211FA Fall 2011 from 9/1/2011 to 11/30/2011
	When I create section 01 from the course and term
	Then the section's course is BIOL 1301
	And the section's term is 211FA
	And the section's rubric is BIOL
	And the section's course number is 1301
	And the section's section number is 01
	And the section's term abbreviation is 211FA
	And the section's term name is Fall 2011
	And the section's start date is 9/1/2011
	And the section's end date is 11/30/2011
	And the section's title is "Introductory Biology"
	And the section's course type is ACAD
	And the section's approval number is 1234567890
	And the section's CIP is 123456
	And it should do nothing else

@domain
Scenario: Cant create a credit section without an approval number or CIP
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	And I have created a term 211FA Fall 2011 from 9/1/2011 to 11/30/2011
	When I create section 01 from the course and term
	Then the aggregate state is invalid
	And the error is "Your attempt to create the section failed. Set approval number or CIP at the course level first."

