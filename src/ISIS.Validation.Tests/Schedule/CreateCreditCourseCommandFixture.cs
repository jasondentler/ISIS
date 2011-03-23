using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class CreateCreditCourseCommandFixture : ValidationFixture<CreateCreditCourseCommand>
    {
        protected override AbstractValidator<CreateCreditCourseCommand> CreateValidator()
        {
            return new CreateCreditCourseCommandValidator();
        }

        protected override CreateCreditCourseCommand GetValidInstance()
        {
            return new CreateCreditCourseCommand()
                       {
                           CourseId = Guid.NewGuid(),
                           Rubric = "BIOL",
                           CourseNumber = "1234",
                           Title = "Cuttin' up frogs",
                           Types = new[] {CourseTypes.ACAD, CourseTypes.TECH}
                       };
        }

        [Test]
        public void CourseId_must_not_be_default_guid()
        {
            GetFailure(new CreateCreditCourseCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }

        
        [Test]
        public void Rubric_must_be_4_characters_long()
        {
            GetFailure(new CreateCreditCourseCommand()
                           {
                               Rubric = "ABC"
                           }, cmd => cmd.Rubric);
        }

        [Test]
        public void Rubric_must_contain_all_letters()
        {
            GetFailure(new CreateCreditCourseCommand()
            {
                Rubric = "ABC1"
            }, cmd => cmd.Rubric);
        }

        [Test]
        public void Rubric_must_be_all_uppercase()
        {
            GetFailure(new CreateCreditCourseCommand()
            {
                Rubric = "ABCd"
            }, cmd => cmd.Rubric);
        }

        [Test]
        public void Course_number_must_be_4_digits_long()
        {
            GetFailure(new CreateCreditCourseCommand()
            {
                CourseNumber = "012"
            }, cmd => cmd.CourseNumber);
        }

        [Test]
        public void Course_number_must_be_all_digits()
        {
            GetFailure(new CreateCreditCourseCommand()
            {
                CourseNumber = "012A"
            }, cmd => cmd.CourseNumber);
        }

        [Test]
        public void Title_cant_be_null()
        {
            GetFailure(new CreateCreditCourseCommand()
                           {
                               Title = null
                           }, cmd => cmd.Title);
        }

        [Test]
        public void Title_cant_be_empty()
        {
            GetFailure(new CreateCreditCourseCommand()
            {
                Title = ""
            }, cmd => cmd.Title);
        }
        
        [Test]
        public void Types_cant_be_empty()
        {
            GetFailure(new CreateCreditCourseCommand()
            {
                Types = new CourseTypes[0]
            }, cmd => cmd.Types);
        }

        [Test]
        public void Types_cant_be_null()
        {
            GetFailure(new CreateCreditCourseCommand()
            {
                Types = null
            }, cmd => cmd.Types);
        }

    }
}
