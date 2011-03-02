using NUnit.Framework;

namespace ISIS
{
    [TestFixture]
    public class PageOfTestFixture : BaseFixture
    {

        [Test]
        public void zero_results_returns_zero_pages()
        {
            var x = new PageOf<int>(0, 1, 5, new int[0]);
            Assert.That(x.PageCount, Is.EqualTo(0));
        }

        [Test]
        public void one_result_returns_one_page()
        {
            var x = new PageOf<int>(1, 1, 5, new int[0]);
            Assert.That(x.PageCount, Is.EqualTo(1));
        }

        [Test]
        public void perfectly_divisible_returns_no_partial_page()
        {
            var x = new PageOf<int>(5, 1, 5, new int[0]);
            Assert.That(x.PageCount, Is.EqualTo(1));
        }

        [Test]
        public void remainder_adds_a_page()
        {
            var x = new PageOf<int>(6, 1, 5, new int[0]);
            Assert.That(x.PageCount, Is.EqualTo(2));
        }

    }
}
