﻿using Ncqrs;
using NUnit.Framework;

namespace ISIS
{
    public abstract class BaseFixture
    {

        static BaseFixture()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        protected readonly ILog Log;

        protected BaseFixture()
        {
            Log = LogManager.GetLogger(GetType());
        }

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            OnFixtureSetup();
        }

        protected virtual void OnFixtureSetup()
        {
            if (!NcqrsEnvironment.IsConfigured)
                NcqrsEnvironment.Configure(new TestEnvironmentConfiguration());
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
