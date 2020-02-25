using System;
using NSubstitute;
using NUnit.Framework;

namespace as3mbus.Selfish.Source.Test.EditMode
{
    [TestFixture]
    [Category("Quiz")]
    public class QuizSystemTest
    {
        private QuizSystem testObject;

        [SetUp]
        public void SetUp()
        {
            testObject = new QuizSystem();
        }

        private void PopulateSystem(int times)
        {
            for (int i = 0; i < times; i++)
            {
                IQuiz newQuiz = Substitute.For<IQuiz>();
                newQuiz.IsCorrect(true).Returns(true);
                newQuiz.IsCorrect(false).Returns(false);
                testObject.AddQuiz(newQuiz);
            }
        }

        [Test]
        [Order(0)]
        public void Add_Quiz_Register_Quiz_To_Quiz_Set()
        {
            IQuiz newQuiz = Substitute.For<IQuiz>();
            testObject.AddQuiz(newQuiz);
            Assert.That(testObject.Quizzes.Contains(newQuiz));
        }

        [Test]
        [Order(0)]
        [TestCase(true)]
        [TestCase(false)]
        public void Add_Quiz_Only_Register_Unique_Quiz(bool unique)
        {
            IQuiz newQuiz = Substitute.For<IQuiz>();
            IQuiz testQuiz = unique ? Substitute.For<IQuiz>() : newQuiz;
            testObject.AddQuiz(newQuiz);
            testObject.AddQuiz(testQuiz);
            Assert.That(testObject.Quizzes.Count, Is.EqualTo(unique ? 2 : 1));
        }

        [Test]
        [Order(1)]
        public void Select_Quiz_Assign_Active_Quiz()
        {
            IQuiz newQuiz = Substitute.For<IQuiz>();
            testObject.AddQuiz(newQuiz);
            testObject.SelectQuiz(newQuiz);
            Assert.That(testObject.ActiveQuiz, Is.EqualTo(newQuiz));
        }

        [Test]
        [Order(2)]
        public void Selecting_Unregistered_Quiz_Throws_Exception()
        {
            IQuiz testQuiz = Substitute.For<IQuiz>();
            IQuiz newQuiz = Substitute.For<IQuiz>();
            testObject.AddQuiz(newQuiz);
            Assert.Throws<Exception>(() => testObject.SelectQuiz(testQuiz));
        }

        [Test]
        [Order(3)]
        [TestCase(true)]
        [TestCase(false)]
        public void Answer_Quiz_Return_Active_Quiz_IsCorrect(bool answer)
        {
            IQuiz newQuiz = Substitute.For<IQuiz>();
            newQuiz.IsCorrect(true).Returns(true);
            newQuiz.IsCorrect(false).Returns(false);
            testObject.AddQuiz(newQuiz);
            testObject.SelectQuiz(newQuiz);
            Assert.That(testObject.AnswerQuiz(true), Is.EqualTo(true));
        }
        
        [Test]
        [Order(4)]
        public void Answer_Quiz_Thross_Exception_When_No_Active_Quiz()
        {
            IQuiz newQuiz = Substitute.For<IQuiz>();
            testObject.AddQuiz(newQuiz);
            Assert.Throws<NullReferenceException>(()=> testObject.AnswerQuiz(true));
        }

        [Test]
        [Order(5)]
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(4)]
        public void NextQuiz_Navigate_To_Next_Registered_Quiz(int index)
        {
            PopulateSystem(5);
            testObject.SelectQuiz(testObject.Quizzes[index]);
            testObject.NextQuiz();
            Assert.That(testObject.ActiveQuizIndex,
                Is.EqualTo((index + 1 < testObject.Quizzes.Count) ? index + 1 : 0));
        }
        
        [Test]
        [Order(6)]
        public void Next_Quiz_Select_First_Quiz_When_Active_Quiz_Is_Null()
        {
            PopulateSystem(5);
            testObject.NextQuiz();
            Assert.That(testObject.ActiveQuizIndex, Is.EqualTo(0));
        }
        
        [Test]
        [Order(7)]
        public void Removing_Active_Quiz_Set_Active_Quiz_To_Null()
        {
            PopulateSystem(5);
            testObject.SelectQuiz(testObject.Quizzes[3]);
            testObject.RemoveQuiz(testObject.ActiveQuiz);
            Assert.That(testObject.ActiveQuiz, Is.Null);
        }
        [Test]
        [Order(8)]
        public void On_Active_Quiz_Changed_Emit_Event()
        {
            PopulateSystem(5);
            IQuiz newQuiz = null;
            testObject.OnActiveQuizChanges += (quiz) => newQuiz = quiz; 
            testObject.SelectQuiz(testObject.Quizzes[3]);
            Assert.That(newQuiz, Is.Not.Null);
        }
        [Test]
        [Order(9)]
        public void On_Answering_Quiz_Emit_Event()
        {
            PopulateSystem(5);
            object answerInvoked = null;
            bool eventEmitted = false;
            testObject.OnAnswerQuiz += (answer, isCorrect) =>
            {
                eventEmitted = true;
                answerInvoked = answer;
            }; 
            testObject.SelectQuiz(testObject.Quizzes[3]);
            testObject.AnswerQuiz(true);
            Assert.That(eventEmitted);
            Assert.That(answerInvoked, Is.Not.Null);
        }

    }
}