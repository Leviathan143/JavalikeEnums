﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JavalikeEnums;

namespace JavalikeEnums.Tests
{
    [TestClass]
    public class JavalikeEnumTest
    {
        [TestMethod]
        public void TestNames()
        {
            Assert.AreEqual("UPPERCASE_TEST", NameTestEnum.UPPERCASE_TEST.Name);
            Assert.AreEqual("lowercase_test", NameTestEnum.lowercase_test.Name);
            Assert.AreEqual("camelCaseTest", NameTestEnum.camelCaseTest.Name);
            Assert.AreEqual("mIxEdCaSeTeSt", NameTestEnum.mIxEdCaSeTeSt.Name);
        }

        [TestMethod]
        public void TestOrdinals()
        {
            Assert.AreEqual(0, OrdinalTestEnum.TEST0.Ordinal);
            Assert.AreEqual(1, OrdinalTestEnum.TEST1.Ordinal);
            Assert.AreEqual(0, OrdinalTestEnum2.TEST0.Ordinal);
            Assert.AreEqual(1, OrdinalTestEnum2.TEST1.Ordinal);
        }

        [TestMethod]
        public void TestTypeMismatch()
        {
            try
            {
                TypeMismatchTestEnumB.DummyInit();
            }
            catch(TypeInitializationException e)
            {
                Assert.IsInstanceOfType(e.InnerException, typeof(TypeMismatchException));
                return;
            }
            Assert.Fail("Initialisation of TypeMismatchTestEnumB failed to throw a TypeMismatchException.");
        }

        [TestMethod]
        public void TestInvalidModifiers()
        {
            bool privateTestPassed, instanceMemberTestPassed, mutableTestPassed;
            privateTestPassed = instanceMemberTestPassed = mutableTestPassed = false;
            try
            {
                PrivateConstantInvalidityTestEnum.DummyInit();
            }
            catch (TypeInitializationException e)
            {
                Assert.IsInstanceOfType(e.InnerException, typeof(InvalidModifiersException), "Expected inner exception to be of type InvalidModifiersException");
                privateTestPassed = true;
                Assert.AreEqual((e.InnerException as InvalidModifiersException).ModifierType, InvalidModifiersException.InvalidModifierType.PRIVATE);
            }
            try
            {
                InstanceMemberConstantInvalidityTestEnum.DummyInit();
            }
            catch (InvalidModifiersException e)
            {
                instanceMemberTestPassed = true;
                Assert.AreEqual((e as InvalidModifiersException).ModifierType, InvalidModifiersException.InvalidModifierType.INSTANCE_MEMBER);
            }
            try
            {
                MutableConstantInvalidityTestEnum.DummyInit();
            }
            catch (TypeInitializationException e)
            {
                Assert.IsInstanceOfType(e.InnerException, typeof(InvalidModifiersException), "Expected inner exception to be of type InvalidModifiersException");
                mutableTestPassed = true;
                Assert.AreEqual((e.InnerException as InvalidModifiersException).ModifierType, InvalidModifiersException.InvalidModifierType.MUTABLE);
            }
            if (!privateTestPassed) Assert.Fail("Expected PrivateConstantInvalidityTestEnum.DummyInit() to throw an exception of type InvalidModifiersException");
            if (!instanceMemberTestPassed) Assert.Fail("Expected InstanceMemberInvalidityTestEnum.DummyInit() to throw an exception of type InvalidModifiersException");
            if (!mutableTestPassed) Assert.Fail("Expected MutableConstantInvalidityTestEnum.DummyInit() to throw an exception of type InvalidModifiersException");
        }

        [TestMethod]
        public void TestValues()
        {
            Assert.AreEqual(ValuesTestEnum.TEST1, ValuesTestEnum.Values[0]);
            Assert.AreEqual(ValuesTestEnum.TEST2, ValuesTestEnum.Values[1]);
            Assert.AreEqual(ValuesTestEnum.TEST3, ValuesTestEnum.Values[2]);

            Assert.AreEqual(ValuesTestEnum.TEST1, ValuesTestEnum.GetConstant("TEST1"));
            Assert.AreEqual(ValuesTestEnum.TEST2, ValuesTestEnum.GetConstant("TEST2"));
            Assert.AreEqual(ValuesTestEnum.TEST3, ValuesTestEnum.GetConstant("TEST3"));
        }
    }

    public class NameTestEnum : JavalikeEnum<NameTestEnum>
    {
        public static readonly NameTestEnum UPPERCASE_TEST = NewConstant().Create();
        public static readonly NameTestEnum lowercase_test = NewConstant().Create();
        public static readonly NameTestEnum camelCaseTest = NewConstant().Create();
        public static readonly NameTestEnum mIxEdCaSeTeSt = NewConstant().Create();
    }

    public class OrdinalTestEnum : JavalikeEnum<OrdinalTestEnum>
    {
        public static readonly OrdinalTestEnum TEST0 = NewConstant().Create();
        public static readonly OrdinalTestEnum TEST1 = NewConstant().Create();
    }

    public class OrdinalTestEnum2 : JavalikeEnum<OrdinalTestEnum2>
    {
        public static readonly OrdinalTestEnum2 TEST0 = NewConstant().Create();
        public static readonly OrdinalTestEnum2 TEST1 = NewConstant().Create();
    }

    public class TypeMismatchTestEnumA : JavalikeEnum<TypeMismatchTestEnumA> {}

    public class TypeMismatchTestEnumB : JavalikeEnum<TypeMismatchTestEnumA>
    {
        public static readonly TypeMismatchTestEnumA TEST0 = NewConstant().Create();
        public static readonly TypeMismatchTestEnumA TEST1 = NewConstant().Create();

        public static void DummyInit()
        {
            // Dummy method to initialise class
        }
    }

    public class PrivateConstantInvalidityTestEnum : JavalikeEnum<PrivateConstantInvalidityTestEnum>
    {
        private static readonly PrivateConstantInvalidityTestEnum PRIVATE_TEST = NewConstant().Create();

        public static void DummyInit()
        {
            // Dummy method to initialise class
        }
    }

    public class InstanceMemberConstantInvalidityTestEnum : JavalikeEnum<InstanceMemberConstantInvalidityTestEnum>
    {
        public readonly InstanceMemberConstantInvalidityTestEnum INSTANCE_MEMBER_TEST = NewConstant().Create();

        public static void DummyInit()
        {
            // Dummy method to initialise class
            new InstanceMemberConstantInvalidityTestEnum();
        }
    }

    public class MutableConstantInvalidityTestEnum : JavalikeEnum<MutableConstantInvalidityTestEnum>
    {
        public static MutableConstantInvalidityTestEnum MUTABLE_TEST = NewConstant().Create();

        public static void DummyInit()
        {
            // Dummy method to initialise class
        }
    }

    public class ValuesTestEnum : JavalikeEnum<ValuesTestEnum>
    {
        public static readonly ValuesTestEnum TEST1 = NewConstant().Create();
        public static readonly ValuesTestEnum TEST2 = NewConstant().Create();
        public static readonly ValuesTestEnum TEST3 = NewConstant().Create();
    }
}
