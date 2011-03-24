using System;
using FluentValidation;
using NUnit.Framework;

namespace ISIS.Schedule
{
    [TestFixture]
    public class CreateContinuingEducationCourseCommandFixture : ValidationFixture<CreateContinuingEducationCourseCommand>
    {
        protected override AbstractValidator<CreateContinuingEducationCourseCommand> CreateValidator()
        {
            return new CreateContinuingEducationCourseCommandValidator();
        }

        protected override CreateContinuingEducationCourseCommand GetValidInstance()
        {
            return new CreateContinuingEducationCourseCommand()
                       {
                           CourseId = Guid.NewGuid(),
                           Rubric = "BIOL",
                           CourseNumber = "1034",
                           Title = "Cuttin' up frogs",
                           Type = CreditTypes.WorkforceFunded
                       };
        }

        [Test]
        public void CourseId_must_not_be_default_guid()
        {
            GetFailure(new CreateContinuingEducationCourseCommand()
            {
                CourseId = default(Guid)
            }, cmd => cmd.CourseId);
        }

        
        [Test]
        public void Rubric_must_be_4_characters_long()
        {
            GetFailure(new CreateContinuingEducationCourseCommand()
                           {
                               Rubric = "ABC"
                           }, cmd => cmd.Rubric);
        }

        [Test]
        public void Rubric_must_contain_all_letters()
        {
            GetFailure(new CreateContinuingEducationCourseCommand()
            {
                Rubric = "ABC1"
            }, cmd => cmd.Rubric);
        }

        [Test]
        public void Rubric_must_be_all_uppercase()
        {
            GetFailure(new CreateContinuingEducationCourseCommand()
            {
                Rubric = "ABCd"
            }, cmd => cmd.Rubric);
        }

        [Test]
        public void Course_number_must_be_4_digits_long()
        {
            GetFailure(new CreateContinuingEducationCourseCommand()
            {
                CourseNumber = "002"
            }, cmd => cmd.CourseNumber);
        }

        [Test]
        public void Course_number_must_be_all_digits()
        {
            GetFailure(new CreateContinuingEducationCourseCommand()
            {
                CourseNumber = "002A"
            }, cmd => cmd.CourseNumber);
        }

        [Test]
        public void The_2nd_digit_of_the_course_number_must_be_zero()
        {
            GetFailure(new CreateContinuingEducationCourseCommand()
            {
                CourseNumber = "1234"
            }, cmd => cmd.CourseNumber);
        }

        [Test]
        public void Title_cant_be_null()
        {
            GetFailure(new CreateContinuingEducationCourseCommand()
                           {
                               Title = null
                           }, cmd => cmd.Title);
        }

        [Test]
        public void Title_cant_be_empty()
        {
            GetFailure(new CreateContinuingEducationCourseCommand()
            {
                Title = ""
            }, cmd => cmd.Title);
        }

        [Test]
        public void Type_must_be_a_valid_credit_type()
        {
            GetFailure(new CreateContinuingEducationCourseCommand()
                           {
                               Type = 0
                           }, cmd => cmd.Type);
        }

    }
}
