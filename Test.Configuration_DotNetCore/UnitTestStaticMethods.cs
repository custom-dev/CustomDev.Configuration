using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CustomDev.Configuration;

namespace Test.Configuration_DotNetCore
{
    [TestClass]
    public class UnitTestStaticMethods
    {
        #region String
        [TestMethod]
        public void TestExistingKeyString()
        {
            Assert.AreEqual("This is a string value", ConfigurationManager.GetString("ExistingStringKey"));
        }

        [TestMethod]
        public void TestExistingKeyStringWithDefaultValue()
        {
            Assert.AreEqual("This is a string value", ConfigurationManager.GetString("ExistingStringKey", "Default value"));
        }

        [TestMethod]
        public void TestNonExistingKeyString()
        {
            Assert.IsTrue(String.IsNullOrEmpty(ConfigurationManager.GetString("NonExistingStringKey")));
        }

        [TestMethod]
        public void TestNonExistingKeyStringWithDefaultValue()
        {
            Assert.AreEqual("Default value", ConfigurationManager.GetString("NonExistingStringKey", "Default value"));
        }
        #endregion

        #region Bool
        public void TestExistingKeyBool()
        {
            Assert.AreEqual(true, ConfigurationManager.GetBool("ExistingBoolKey_true_1"));
            Assert.AreEqual(true, ConfigurationManager.GetBool("ExistingBoolKey_True_2"));
            Assert.AreEqual(true, ConfigurationManager.GetBool("ExistingBoolKey_TRUE_3"));

            Assert.AreEqual(false, ConfigurationManager.GetBool("ExistingBoolKey_false_1"));
            Assert.AreEqual(false, ConfigurationManager.GetBool("ExistingBoolKey_False_2"));
            Assert.AreEqual(false, ConfigurationManager.GetBool("ExistingBoolKey_FALSE_3"));
        }

        [TestMethod]
        public void TestExistingKeyBoolWithDefaultValue()
        {
            Assert.AreEqual(true, ConfigurationManager.GetBool("ExistingBoolKey_true_1", false));
            Assert.AreEqual(true, ConfigurationManager.GetBool("ExistingBoolKey_True_2", false));
            Assert.AreEqual(true, ConfigurationManager.GetBool("ExistingBoolKey_TRUE_3", false));

            Assert.AreEqual(false, ConfigurationManager.GetBool("ExistingBoolKey_false_1", true));
            Assert.AreEqual(false, ConfigurationManager.GetBool("ExistingBoolKey_False_2", true));
            Assert.AreEqual(false, ConfigurationManager.GetBool("ExistingBoolKey_FALSE_3", true));
        }

        [TestMethod]
        public void TestNonExistingKeyBool()
        {
            Assert.AreEqual(null, ConfigurationManager.GetBool("NonExistingBoolKey"));            
        }

        [TestMethod]
        public void TestNonExistingKeyBoolWithDefaultValue()
        {
            Assert.AreEqual(true, ConfigurationManager.GetBool("NonExistingBoolKey", true));
            Assert.AreEqual(false, ConfigurationManager.GetBool("NonExistingBoolKey", false));
        }

        [TestMethod]
        public void TestInvalidKeyBool()
        {
            Assert.AreEqual(null, ConfigurationManager.GetBool("ExistingBoolKey_InvalidValue"));
            Assert.AreEqual(true, ConfigurationManager.GetBool("ExistingBoolKey_InvalidValue", true));
            Assert.AreEqual(false, ConfigurationManager.GetBool("ExistingBoolKey_InvalidValue", false));
        }
        #endregion

        #region Enum
        public void TestExistingKeyEnum()
        {
            Assert.AreEqual(TestEnum.Value1, ConfigurationManager.GetEnum<TestEnum>("ExistingEnumKey_Value1"));
            Assert.AreEqual(TestEnum.Value2, ConfigurationManager.GetEnum<TestEnum>("ExistingEnumKey_Value2"));
            Assert.AreEqual(TestEnum.Value3, ConfigurationManager.GetEnum<TestEnum>("ExistingEnumKey_Value3"));                       
        }

        [TestMethod]
        public void TestExistingKeyEnumWithDefaultValue()
        {
            Assert.AreEqual(TestEnum.Value1, ConfigurationManager.GetEnum<TestEnum>("ExistingEnumKey_Value1", TestEnum.Value2));
            Assert.AreEqual(TestEnum.Value2, ConfigurationManager.GetEnum<TestEnum>("ExistingEnumKey_Value2", TestEnum.Value2));
            Assert.AreEqual(TestEnum.Value3, ConfigurationManager.GetEnum<TestEnum>("ExistingEnumKey_Value3", TestEnum.Value1));
        }

        [TestMethod]
        public void TestNonExistingKeyEnum()
        {
            Assert.AreEqual(null, ConfigurationManager.GetEnum<TestEnum>("NonExistingEnumKey"));
        }

        [TestMethod]
        public void TestNonExistingKeyEnumWithDefaultValue()
        {
            Assert.AreEqual(TestEnum.Value1, ConfigurationManager.GetEnum<TestEnum>("NonExistingEnumKey", TestEnum.Value1));
            Assert.AreEqual(TestEnum.Value2, ConfigurationManager.GetEnum<TestEnum>("NonExistingEnumKey", TestEnum.Value2));
            Assert.AreEqual(TestEnum.Value3, ConfigurationManager.GetEnum<TestEnum>("NonExistingEnumKey", TestEnum.Value3));
        }

        [TestMethod]
        public void TestInvalidKeyEnum()
        {
            Assert.AreEqual(null, ConfigurationManager.GetEnum<TestEnum>("ExistingEnumKey_Value1_InvalidCase"));
            Assert.AreEqual(null, ConfigurationManager.GetEnum<TestEnum>("ExistingEnumKey_Value2_InvalidCase"));
            Assert.AreEqual(null, ConfigurationManager.GetEnum<TestEnum>("ExistingEnumKey_Value3_InvalidCase"));
            Assert.AreEqual(null, ConfigurationManager.GetBool("ExistingEnumKey_InvalidValue"));            
        }
        #endregion

        #region Integer
        public void TestExistingKeyInt32()
        {
            Assert.AreEqual(0, ConfigurationManager.GetInt32("ExistingInt32Key_Value_0"));
            Assert.AreEqual(1, ConfigurationManager.GetInt32("ExistingInt32Key_Value_1"));
            Assert.AreEqual(-1, ConfigurationManager.GetInt32("ExistingInt32Key_Value_m1"));
        }

        [TestMethod]
        public void TestExistingKeyInt32WithDefaultValue()
        {
            Assert.AreEqual(0, ConfigurationManager.GetInt32("ExistingInt32Key_Value_0", 999));
            Assert.AreEqual(1, ConfigurationManager.GetInt32("ExistingInt32Key_Value_1", 999));
            Assert.AreEqual(-1, ConfigurationManager.GetInt32("ExistingInt32Key_Value_m1", 999));
        }

        [TestMethod]
        public void TestNonExistingKeyInt32()
        {
            Assert.AreEqual(null, ConfigurationManager.GetEnum<TestEnum>("NonExistingEnumInt32"));
        }

        [TestMethod]
        public void TestNonExistingKeyInt32WithDefaultValue()
        {
            Assert.AreEqual(0, ConfigurationManager.GetInt32("NonExistingInt32Key", 0));
            Assert.AreEqual(-123, ConfigurationManager.GetInt32("NonExistingInt32Key", -123));
            Assert.AreEqual(456, ConfigurationManager.GetInt32("NonExistingInt32Key", 456));
        }

        [TestMethod]
        public void TestInvalidKeyInt32()
        {
            Assert.AreEqual(null, ConfigurationManager.GetInt32("ExistingInt32Key_Value_InvalidInt32"));
            Assert.AreEqual(null, ConfigurationManager.GetInt32("ExistingInt32Key_InvalidValue"));            
        }
        #endregion

    }
}
