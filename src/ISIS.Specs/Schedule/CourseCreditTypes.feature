Feature: Course Credit Types
	In order to manage the course schedule
	As a scheduler
	I want to set the credit type on a course

@domain
Scenario: Dont allow setting credit type on a credit course
	Given I have created an ACAD course BIOL 1301 "Introductory Biology"
	When I change the credit type to Grant Funded
	Then the aggregate state is invalid
	And the error is "Your attempt to change the credit type failed because this is a credit course. Credit type may only be set on Continuing Education courses."

@domain
Scenario: Set the credit type to the same credit type
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	When I change the credit type to Workforce Funded
	Then it should do nothing

@domain 
Scenario: Switch from funded to non-funded credit type
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	When I change the credit type to Workforce Non-Funded
	Then the credit type is Workforce Non-Funded
	And the CE course type is added
	And the CWECM course type is removed
	And the current course type is CE
	And it should do nothing else

@domain
Scenario: Switch from non-funded to funded credit type
	Given I have created a Workforce Non-Funded course AGEQ 1091 "Routine Management of Equine Health"
	When I change the credit type to Workforce Funded
	Then the credit type is Workforce Funded
	And the CWECM course type is added
	And the CE course type is removed
	And the current course type is CWECM
	And it should do nothing else

@domain
Scenario: Set the credit type to Contract Training Funded on a CE course
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	When I change the credit type to Contract Training Funded
	Then the credit type is Contract Training Funded
	And the course type is CWECM
	And the current course type is CWECM

@domain
Scenario: Set the credit type to Contract Training Non-Funded on a CE course
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	When I change the credit type to Contract Training Non-Funded
	Then the credit type is Contract Training Non-Funded
	And the course type is CE
	And the current course type is CE

@domain
Scenario: Set the credit type to Special Interests on a CE course
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	When I change the credit type to Special Interests
	Then the credit type is Special Interests
	And the course type is CE
	And the current course type is CE

@domain
Scenario: Set the credit type to Grant Funded on a CE course
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	When I change the credit type to Grant Funded
	Then the credit type is Grant Funded
	And the course type is CWECM
	And the current course type is CWECM

@domain
Scenario: Set the credit type to Grant Non-Funded on a CE course
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	When I change the credit type to Grant Non-Funded
	Then the credit type is Grant Non-Funded
	And the course type is CE
	And the current course type is CE

@domain
Scenario: Set the credit type to Workforce Funded on a CE course
	Given I have created a Special Interests course AGEQ 1091 "Routine Management of Equine Health"
	When I change the credit type to Workforce Funded
	Then the credit type is Workforce Funded
	And the course type is CWECM
	And the current course type is CWECM

@domain
Scenario: Set the credit type to Workforce Non-Funded on a CE course
	Given I have created a Workforce Funded course AGEQ 1091 "Routine Management of Equine Health"
	When I change the credit type to Workforce Non-Funded
	Then the credit type is Workforce Non-Funded
	And the course type is CE
	And the current course type is CE
