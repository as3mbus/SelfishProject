using System;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace as3mbus.Selfish.Source.Test.EditMode
{
    [TestFixture]
    [Category("Quiz")]
    public class QuizTest
    {
        private IQuiz testObject;
        [SetUp]
        public void SetUp()
        {
            IQuiz<bool,bool> quiz = Substitute.For<Quiz<bool,bool>>();
            testObject = quiz;
            testObject.IsCorrect(true).Returns(true);
            testObject.IsCorrect(false).Returns(false);
        }
        [Test]
        [Order(1)]
        public void Is_Correct_Return_True_On_Correct_Answer()
        {
            Assert.That(testObject.IsCorrect(true), Is.True);
        }
        [Test]
        [Order(2)]
        public void Is_Correct_Return_False_On_Wrong_Answer()
        {
            Assert.That(testObject.IsCorrect(false), Is.False);
        }

        [Test]
        [Order(3)]
        public void Is_Correct_Throws_Exception_On_Invalid_Answer()
        {
            Assert.Throws<ArgumentException>(() => testObject.IsCorrect(1));
        }
    }
}