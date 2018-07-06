﻿using System;
using NUnit.Framework;

namespace SettingsMapper.Tests
{
    [TestFixture]
    public class MapersTest
    {
        private const string PocoMapperTestName = "Should set public property {0} to value {1}";
        [Test(TestOf = typeof(PocoMapper))]
        [TestCase("Int", 5, TestName = PocoMapperTestName)]
        [TestCase("String", "foobar", TestName = PocoMapperTestName)]
        [TestCase("NullableDouble", null, TestName = PocoMapperTestName)]
        public void PocoMapperTest(string propName, object value)
        {
            var pocoMapper = new PocoMapper();
            Type targetType = typeof(Model.Poco);
            object target = pocoMapper.CreateTarget(targetType);

            pocoMapper.Map(target, targetType, propName, value);

            var prop = target.GetType().GetProperty(propName);
            var result = prop.GetValue(target);

            Assert.AreEqual(value, result);
        }

        private const string StaticMapperTestName = "Should set public static property {0} to value {1}";
        [Test(TestOf = typeof(StaticMapper))]
        [TestCase("Int", 5, TestName = StaticMapperTestName)]
        [TestCase("String", "foobar", TestName = StaticMapperTestName)]
        [TestCase("NullableDouble", null, TestName = StaticMapperTestName)]
        public void StaticMapperTest(string propName, object value)
        {
            var staticMapper = new StaticMapper();
            var targetType = typeof(Model.Static);
            var target = staticMapper.CreateTarget(targetType);

            staticMapper.Map(target, targetType, propName, value);

            var prop = typeof(Model.Static).GetProperty(propName);
            var result = prop.GetValue(null);

            Assert.AreEqual(value, result);
        }
    }
}