﻿Feature: Credit Sections
	In order to manage the course schedule
	As a scheduler
	I want to manage sections
	
@domain
Scenario: Change the credit section location to CLEM
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course with term 211FA
	When I change the section location to CLEM
	Then the section location is CLEM
	And the topic code is A
	And it should do nothing else

@domain
Scenario: Change the credit section location from TDCJ back to non-TDCJ
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course with term 211FA
	And I have changed the location to CLEM
	When I change the section location to ACC
	Then the section location is ACC
	And the topic code is blank
	And it should do nothing else

@domain
Scenario: Change the credit section location to CV
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course with term 211FA
	When I change the section location to CV
	Then the section location is CV
	And the topic code is A
	And it should do nothing else

@domain
Scenario: Change the credit section location to DAR
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course with term 211FA
	When I change the section location to DAR
	Then the section location is DAR
	And the topic code is A
	And it should do nothing else

@domain
Scenario: Change the credit section location to J1
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course with term 211FA
	When I change the section location to J1
	Then the section location is J1
	And the topic code is A
	And it should do nothing else

@domain
Scenario: Change the credit section location to J2
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course with term 211FA
	When I change the section location to J2
	Then the section location is J2
	And the topic code is A
	And it should do nothing else

@domain
Scenario: Change the credit section location to J3
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course with term 211FA
	When I change the section location to J3
	Then the section location is J3
	And the topic code is A
	And it should do nothing else

@domain
Scenario: Change the credit section location to TER
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course with term 211FA
	When I change the section location to TER
	Then the section location is TER
	And the topic code is A
	And it should do nothing else

@domain
Scenario: Change the credit section location to R1
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course with term 211FA
	When I change the section location to R1
	Then the section location is R1
	And the topic code is A
	And it should do nothing else

@domain
Scenario: Change the credit section location to R2
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have created a topic code A Academic TDC Course Code
	And I have created a term 211FA from 8/25/2011 to 12/7/2011
	And I have created a section 01 from the course with term 211FA
	When I change the section location to R2
	Then the section location is R2
	And the topic code is A
	And it should do nothing else

@domain
Scenario: Create a credit section without a topic code
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have changed the approval number to 1234567890
	And I have created a term 211FA from 9/1/2011 to 11/30/2011
	When I create section 01 from the course with term 211FA
	Then the section's rubric is BIOL
	And the section's course number is 1301
	And the section's section number is 01
	And the section's term is 211FA
	And the section's start date is 9/1/2011
	And the section's end date is 11/30/2011
	And the section's title is Introductory Biology
	And the section's course type is ACAD
	And the section's current course type is ACAD
	And the section's approval number is 1234567890
	And the section's CIP is 123456
	And the location is blank
	And it should do nothing else

@domain
Scenario: Cant create a credit section without an approval number or CIP
	Given I have created an ACAD course BIOL 1301 Introductory Biology
	And I have created a term 211FA from 9/1/2011 to 11/30/2011
	When I create section 01 from the course with term 211FA
	Then the command is invalid
	And the error is "Your attempt to create the section failed. Set approval number or CIP at the course level first."

