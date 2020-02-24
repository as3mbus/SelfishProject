using System;
using NUnit.Framework;
using UnityEngine;

namespace as3mbus.Selfish.Source.Test.EditMode
{
    [TestFixture]
    [Category("Timer")]
    public class TimerTest
    {
        private Timer testObject;

        [SetUp]
        public void SetUp()
        {
            testObject = new Timer();
        }

        [Test]
        [Order(0)]
        public void Update_Running_Timer_Reduce_Remaining_Time([Random(5)] float delta)
        {
            testObject.Start();
            float preUpdateRemainingTime = testObject.RemainingTime;
            testObject.Update(delta);
            Assert.That(testObject.RemainingTime, Is.EqualTo(preUpdateRemainingTime - delta));
        }

        [Test]
        [Order(1)]
        public void Update_Not_Running_Timer_Throws_Exception()
        {
            Assert.Throws<Exception>(() => testObject.Update(0.1f));
        }

        [Test]
        [Order(2)]
        public void Start_Run_Timer()
        {
            testObject.Start();
            Assert.That(testObject.IsRunning, Is.True);
        }

        [Test]
        [Order(3)]
        public void Start_Set_Assigned_Time()
        {
            const float testValue = 50;
            testObject.Start(testValue);
            Assert.That(testObject.AssignedTime, Is.EqualTo(testValue));
        }

        [Test]
        [Order(4)]
        public void Start_Set_RemainingTime()
        {
            const float testValue = 50;
            testObject.Start(testValue);
            Assert.That(testObject.RemainingTime, Is.EqualTo(testValue));
        }

        [Test]
        [Order(5)]
        public void Remaining_Time_Never_Below_0()
        {
            testObject.Start(45);
            testObject.Update(float.MaxValue);
            Assert.That(testObject.RemainingTime, Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        [Order(6)]
        public void Stop_Timer_When_Remaining_Time_Reach_0()
        {
            testObject.Start(45);
            testObject.Update(float.MaxValue);
            Assert.That(!testObject.IsRunning);
        }

        [Test]
        [Order(7)]
        public void OnTimeOut_Event_Emitted_When_Time_Runs_Out()
        {
            bool eventInvoked = false;
            testObject.OnTimeOut += () => eventInvoked = true;
            testObject.Start(45);
            testObject.Update(float.MaxValue);
            Assert.That(eventInvoked);
        }

        [Test]
        [Order(7)]
        public void OnUpdate_Event_Emitted_When_Remaining_Time_Changes([Random(3)] float delta)
        {
            float eventValue = 0;
            testObject.OnUpdate += (timeDiff) => eventValue = timeDiff;
            testObject.Start(45);
            float preUpdateRemainingTime = testObject.RemainingTime;
            testObject.Update(delta);
            float calculatedDifference = preUpdateRemainingTime - testObject.RemainingTime;
            Debug.Log(calculatedDifference);
            Debug.Log(eventValue);
            Assert.That(calculatedDifference, Is.EqualTo(eventValue));
        }

        [Test]
        [Order(9)]
        public void Start_On_Running_Timer_Throws_Exception()
        {
            testObject.Start();
            Assert.Throws<Exception>(() => testObject.Start());
        }

        [Test]
        [Order(10)]
        [TestCase(true, true)]
        [TestCase(false, false)]
        public void Toggle_Pause_Sets_IsRunning_State(bool running, bool pause)
        {
            if(running) testObject.Start();
            bool previousState = testObject.IsRunning;
            testObject.TogglePause(pause);
            Assert.That(testObject.IsRunning,Is.Not.EqualTo(previousState));
        }
        [Test]
        [Order(10)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        public void Toggle_Pause_Throw_Exception_On_Invalid_Call(bool running, bool pause)
        {
            if(running) testObject.Start();
            bool previousState = testObject.IsRunning;
            Assert.Throws<Exception>(() =>testObject.TogglePause(pause));
        }
    }
}