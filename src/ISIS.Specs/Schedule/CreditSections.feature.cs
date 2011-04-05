// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.5.0.0
//      Runtime Version:4.0.30319.225
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
namespace ISIS.Schedule
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.5.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Credit Sections")]
    public partial class CreditSectionsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "CreditSections.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Credit Sections", "In order to manage the course schedule\nAs a scheduler\nI want to manage sections", GenerationTargetLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change the credit section location")]
        [NUnit.Framework.CategoryAttribute("domain:")]
        public virtual void ChangeTheCreditSectionLocation()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change the credit section location", new string[] {
                        "domain:"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("I have created an ACAD course BIOL 1301 \"Introductory Biology\"");
#line 9
 testRunner.And("I have set the approval number to 1234567890");
#line 10
 testRunner.And("I have created a topic code A Academic TDC Course Code");
#line 11
 testRunner.And("I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011");
#line 12
 testRunner.And("I have created a section 01 from the course and term");
#line 13
 testRunner.And("I have created a location ACC Main Campus");
#line 14
 testRunner.When("I change the section location to ACC");
#line 15
 testRunner.Then("the section location is ACC");
#line 16
 testRunner.And("the section location abbreviation is ACC");
#line 17
 testRunner.And("the section location name is Main Campus");
#line 18
 testRunner.And("it should do nothing else");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change the credit section location to CLEM")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void ChangeTheCreditSectionLocationToCLEM()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change the credit section location to CLEM", new string[] {
                        "domain"});
#line 21
this.ScenarioSetup(scenarioInfo);
#line 22
 testRunner.Given("I have created an ACAD course BIOL 1301 \"Introductory Biology\"");
#line 23
 testRunner.And("I have set the approval number to 1234567890");
#line 24
 testRunner.And("I have created a topic code A Academic TDC Course Code");
#line 25
 testRunner.And("I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011");
#line 26
 testRunner.And("I have created a section 01 from the course and term");
#line 27
 testRunner.And("I have created a location CLEM Clemens Unit");
#line 28
 testRunner.When("I change the section location to CLEM");
#line 29
 testRunner.Then("the section location is CLEM");
#line 30
 testRunner.And("the topic code is A Academic TDC Course Code");
#line 31
 testRunner.And("it should do nothing else");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change the credit section location from TDCJ back to non-TDCJ")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void ChangeTheCreditSectionLocationFromTDCJBackToNon_TDCJ()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change the credit section location from TDCJ back to non-TDCJ", new string[] {
                        "domain"});
#line 34
this.ScenarioSetup(scenarioInfo);
#line 35
 testRunner.Given("I have created an ACAD course BIOL 1301 \"Introductory Biology\"");
#line 36
 testRunner.And("I have set the approval number to 1234567890");
#line 37
 testRunner.And("I have created a topic code A Academic TDC Course Code");
#line 38
 testRunner.And("I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011");
#line 39
 testRunner.And("I have created a section 01 from the course and term");
#line 40
 testRunner.And("I have created a location CLEM Clemens Unit");
#line 41
 testRunner.And("I have created a location ACC Main Campus");
#line 42
 testRunner.And("I have set the section location to CLEM Clemens Unit");
#line 43
 testRunner.When("I change the section location to ACC");
#line 44
 testRunner.Then("the section location is ACC");
#line 45
 testRunner.And("the topic code is blank");
#line 46
 testRunner.And("it should do nothing else");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change the credit section location from one TDCJ to another")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void ChangeTheCreditSectionLocationFromOneTDCJToAnother()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change the credit section location from one TDCJ to another", new string[] {
                        "domain"});
#line 49
this.ScenarioSetup(scenarioInfo);
#line 50
 testRunner.Given("I have created an ACAD course BIOL 1301 \"Introductory Biology\"");
#line 51
 testRunner.And("I have set the approval number to 1234567890");
#line 52
 testRunner.And("I have created a topic code A Academic TDC Course Code");
#line 53
 testRunner.And("I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011");
#line 54
 testRunner.And("I have created a section 01 from the course and term");
#line 55
 testRunner.And("I have created a location CLEM Clemens Unit");
#line 56
 testRunner.And("I have created a location DAR Darrington Unit");
#line 57
 testRunner.And("I have set the section location to CLEM Clemens Unit");
#line 58
 testRunner.When("I change the section location to DAR");
#line 59
 testRunner.Then("the section location is DAR");
#line 60
 testRunner.And("it should do nothing else");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change the credit section location from one non-TDCJ to another")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void ChangeTheCreditSectionLocationFromOneNon_TDCJToAnother()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change the credit section location from one non-TDCJ to another", new string[] {
                        "domain"});
#line 63
this.ScenarioSetup(scenarioInfo);
#line 64
 testRunner.Given("I have created an ACAD course BIOL 1301 \"Introductory Biology\"");
#line 65
 testRunner.And("I have set the approval number to 1234567890");
#line 66
 testRunner.And("I have created a topic code A Academic TDC Course Code");
#line 67
 testRunner.And("I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011");
#line 68
 testRunner.And("I have created a section 01 from the course and term");
#line 69
 testRunner.And("I have created a location ACC Main Campus");
#line 70
 testRunner.And("I have created a location AHS Alvin High School");
#line 71
 testRunner.And("I have set the section location to ACC Main Campus");
#line 72
 testRunner.When("I change the section location to AHS");
#line 73
 testRunner.Then("the section location is AHS");
#line 74
 testRunner.And("it should do nothing else");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change the credit section location to CV")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void ChangeTheCreditSectionLocationToCV()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change the credit section location to CV", new string[] {
                        "domain"});
#line 77
this.ScenarioSetup(scenarioInfo);
#line 78
 testRunner.Given("I have created an ACAD course BIOL 1301 \"Introductory Biology\"");
#line 79
 testRunner.And("I have set the approval number to 1234567890");
#line 80
 testRunner.And("I have created a topic code A Academic TDC Course Code");
#line 81
 testRunner.And("I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011");
#line 82
 testRunner.And("I have created a section 01 from the course and term");
#line 83
 testRunner.And("I have created a location CV Carol Vance");
#line 84
 testRunner.When("I change the section location to CV");
#line 85
 testRunner.Then("the section location is CV");
#line 86
 testRunner.And("the topic code is A Academic TDC Course Code");
#line 87
 testRunner.And("it should do nothing else");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change the credit section location to DAR")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void ChangeTheCreditSectionLocationToDAR()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change the credit section location to DAR", new string[] {
                        "domain"});
#line 90
this.ScenarioSetup(scenarioInfo);
#line 91
 testRunner.Given("I have created an ACAD course BIOL 1301 \"Introductory Biology\"");
#line 92
 testRunner.And("I have set the approval number to 1234567890");
#line 93
 testRunner.And("I have created a topic code A Academic TDC Course Code");
#line 94
 testRunner.And("I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011");
#line 95
 testRunner.And("I have created a section 01 from the course and term");
#line 96
 testRunner.And("I have created a location DAR Darrington Unit");
#line 97
 testRunner.When("I change the section location to DAR");
#line 98
 testRunner.Then("the section location is DAR");
#line 99
 testRunner.And("the topic code is A Academic TDC Course Code");
#line 100
 testRunner.And("it should do nothing else");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change the credit section location to J1")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void ChangeTheCreditSectionLocationToJ1()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change the credit section location to J1", new string[] {
                        "domain"});
#line 103
this.ScenarioSetup(scenarioInfo);
#line 104
 testRunner.Given("I have created an ACAD course BIOL 1301 \"Introductory Biology\"");
#line 105
 testRunner.And("I have set the approval number to 1234567890");
#line 106
 testRunner.And("I have created a topic code A Academic TDC Course Code");
#line 107
 testRunner.And("I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011");
#line 108
 testRunner.And("I have created a section 01 from the course and term");
#line 109
 testRunner.And("I have created a location J1 Jester 1 Unit");
#line 110
 testRunner.When("I change the section location to J1");
#line 111
 testRunner.Then("the section location is J1");
#line 112
 testRunner.And("the topic code is A Academic TDC Course Code");
#line 113
 testRunner.And("it should do nothing else");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change the credit section location to J2")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void ChangeTheCreditSectionLocationToJ2()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change the credit section location to J2", new string[] {
                        "domain"});
#line 116
this.ScenarioSetup(scenarioInfo);
#line 117
 testRunner.Given("I have created an ACAD course BIOL 1301 \"Introductory Biology\"");
#line 118
 testRunner.And("I have set the approval number to 1234567890");
#line 119
 testRunner.And("I have created a topic code A Academic TDC Course Code");
#line 120
 testRunner.And("I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011");
#line 121
 testRunner.And("I have created a section 01 from the course and term");
#line 122
 testRunner.And("I have created a location J2 Jester 2 Unit");
#line 123
 testRunner.When("I change the section location to J2");
#line 124
 testRunner.Then("the section location is J2");
#line 125
 testRunner.And("the topic code is A Academic TDC Course Code");
#line 126
 testRunner.And("it should do nothing else");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change the credit section location to J3")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void ChangeTheCreditSectionLocationToJ3()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change the credit section location to J3", new string[] {
                        "domain"});
#line 129
this.ScenarioSetup(scenarioInfo);
#line 130
 testRunner.Given("I have created an ACAD course BIOL 1301 \"Introductory Biology\"");
#line 131
 testRunner.And("I have set the approval number to 1234567890");
#line 132
 testRunner.And("I have created a topic code A Academic TDC Course Code");
#line 133
 testRunner.And("I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011");
#line 134
 testRunner.And("I have created a section 01 from the course and term");
#line 135
 testRunner.And("I have created a location J3 Jester 3 Unit");
#line 136
 testRunner.When("I change the section location to J3");
#line 137
 testRunner.Then("the section location is J3");
#line 138
 testRunner.And("the topic code is A Academic TDC Course Code");
#line 139
 testRunner.And("it should do nothing else");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change the credit section location to TER")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void ChangeTheCreditSectionLocationToTER()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change the credit section location to TER", new string[] {
                        "domain"});
#line 142
this.ScenarioSetup(scenarioInfo);
#line 143
 testRunner.Given("I have created an ACAD course BIOL 1301 \"Introductory Biology\"");
#line 144
 testRunner.And("I have set the approval number to 1234567890");
#line 145
 testRunner.And("I have created a topic code A Academic TDC Course Code");
#line 146
 testRunner.And("I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011");
#line 147
 testRunner.And("I have created a section 01 from the course and term");
#line 148
 testRunner.And("I have created a location TER T. C. Terrell Unit");
#line 149
 testRunner.When("I change the section location to TER");
#line 150
 testRunner.Then("the section location is TER");
#line 151
 testRunner.And("the topic code is A Academic TDC Course Code");
#line 152
 testRunner.And("it should do nothing else");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change the credit section location to R1")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void ChangeTheCreditSectionLocationToR1()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change the credit section location to R1", new string[] {
                        "domain"});
#line 155
this.ScenarioSetup(scenarioInfo);
#line 156
 testRunner.Given("I have created an ACAD course BIOL 1301 \"Introductory Biology\"");
#line 157
 testRunner.And("I have set the approval number to 1234567890");
#line 158
 testRunner.And("I have created a topic code A Academic TDC Course Code");
#line 159
 testRunner.And("I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011");
#line 160
 testRunner.And("I have created a section 01 from the course and term");
#line 161
 testRunner.And("I have created a location R1 Ramsey 1 Unit");
#line 162
 testRunner.When("I change the section location to R1");
#line 163
 testRunner.Then("the section location is R1");
#line 164
 testRunner.And("the topic code is A Academic TDC Course Code");
#line 165
 testRunner.And("it should do nothing else");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change the credit section location to R2")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void ChangeTheCreditSectionLocationToR2()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change the credit section location to R2", new string[] {
                        "domain"});
#line 168
this.ScenarioSetup(scenarioInfo);
#line 169
 testRunner.Given("I have created an ACAD course BIOL 1301 \"Introductory Biology\"");
#line 170
 testRunner.And("I have set the approval number to 1234567890");
#line 171
 testRunner.And("I have created a topic code A Academic TDC Course Code");
#line 172
 testRunner.And("I have created a term 211FA Fall 2011 from 8/25/2011 to 12/7/2011");
#line 173
 testRunner.And("I have created a section 01 from the course and term");
#line 174
 testRunner.And("I have created a location R2 Stringfellow Unit");
#line 175
 testRunner.When("I change the section location to R2");
#line 176
 testRunner.Then("the section location is R2");
#line 177
 testRunner.And("the topic code is A Academic TDC Course Code");
#line 178
 testRunner.And("it should do nothing else");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create a credit section without a topic code")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void CreateACreditSectionWithoutATopicCode()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create a credit section without a topic code", new string[] {
                        "domain"});
#line 181
this.ScenarioSetup(scenarioInfo);
#line 182
 testRunner.Given("I have created an ACAD course BIOL 1301 \"Introductory Biology\"");
#line 183
 testRunner.And("I have set the approval number to 1234567890");
#line 184
 testRunner.And("I have created a term 211FA Fall 2011 from 9/1/2011 to 11/30/2011");
#line 185
 testRunner.When("I create section 01 from the course and term");
#line 186
 testRunner.Then("the section\'s course is BIOL 1301");
#line 187
 testRunner.And("the section\'s term is 211FA");
#line 188
 testRunner.And("the section\'s rubric is BIOL");
#line 189
 testRunner.And("the section\'s course number is 1301");
#line 190
 testRunner.And("the section\'s section number is 01");
#line 191
 testRunner.And("the section\'s term abbreviation is 211FA");
#line 192
 testRunner.And("the section\'s term name is Fall 2011");
#line 193
 testRunner.And("the section\'s start date is 9/1/2011");
#line 194
 testRunner.And("the section\'s end date is 11/30/2011");
#line 195
 testRunner.And("the section\'s title is \"Introductory Biology\"");
#line 196
 testRunner.And("the section\'s course type is ACAD");
#line 197
 testRunner.And("the section\'s approval number is 1234567890");
#line 198
 testRunner.And("the section\'s CIP is 123456");
#line 199
 testRunner.And("the section\'s status is pending");
#line 200
 testRunner.And("it should do nothing else");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Cant create a credit section without an approval number or CIP")]
        [NUnit.Framework.CategoryAttribute("domain")]
        public virtual void CantCreateACreditSectionWithoutAnApprovalNumberOrCIP()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Cant create a credit section without an approval number or CIP", new string[] {
                        "domain"});
#line 203
this.ScenarioSetup(scenarioInfo);
#line 204
 testRunner.Given("I have created an ACAD course BIOL 1301 \"Introductory Biology\"");
#line 205
 testRunner.And("I have created a term 211FA Fall 2011 from 9/1/2011 to 11/30/2011");
#line 206
 testRunner.When("I create section 01 from the course and term");
#line 207
 testRunner.Then("the aggregate state is invalid");
#line 208
 testRunner.And("the error is \"Your attempt to create the section failed. Set approval number or C" +
                    "IP at the course level first.\"");
#line hidden
            testRunner.CollectScenarioErrors();
        }
    }
}
#endregion
