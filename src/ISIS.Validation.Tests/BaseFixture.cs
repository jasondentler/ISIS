using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ISIS.Validation.Tests
{
    public abstract class BaseFixture
    {

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            OnFixtureSetup();
        }

        protected virtual void OnFixtureSetup()
        {
            
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            OnFixtureTearDown();
        }

        protected virtual void OnFixtureTearDown()
        {
        }

        [SetUp]
        public void TestSetup()
        {
            OnTestSetup();
        }

        protected virtual void OnTestSetup()
        {
            
        }

        [TearDown]
        public void TestTearDown()
        {
            OnTestTearDown();
        }

        protected virtual void OnTestTearDown()
        {
            
        }

    }
}
