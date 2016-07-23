using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringCalculator.UnitTests
{
    [TestClass]
    public class StringCalculatorTest
    {
        [TestMethod]
        public void TestAdd_EmptyString()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("");
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestAdd_OneDigit()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("1");
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestAdd_TwoDigits()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("1,2");
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void TestAdd_SeveralDigits()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("1,2,3,5,9,5");
            Assert.AreEqual(25, result);
        }

        [TestMethod]
        public void TestAdd_NewLineBetweenDigits()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("1,2\n5");
            Assert.AreEqual(8, result);
        }


        [TestMethod]
        public void TestAdd_CustomDelimiter()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("//;\n1;2;5");
            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void TestAdd_NegativeNumbers()
        {
            var calculator = new StringCalculator();

            try
            {
                calculator.Add("-1,2,3,-4");
            }
            catch (NegativeNumberException e)
            {
                var message = e.Message;
                Assert.IsTrue(message.Contains("-1"));
                Assert.IsTrue(message.Contains("-4"));
                Assert.IsTrue(message.Contains("negative not supported"));
            }
            catch (Exception)
            {
                Assert.Fail("No Exception");
            }
        }

        [TestMethod]
        public void TestAdd_IgnoreBigNumbers()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("1,1001,2");
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void TestAdd_DelimiterOfAnyLength()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("//*#*\n1*#*2*#*5");
            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void TestAdd_MultiplyDelimiters()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("//[*][%]\n1*2%3");
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void TestAdd_MultiplyDelimitersWithDifferentLength()
        {
            var calculator = new StringCalculator();
            var result = calculator.Add("//[*][%$%]\n1*2%$%3");
            Assert.AreEqual(6, result);
        }
    }
}
