using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

namespace as3mbus.Selfish.Source.Test.EditMode
{
    public class JumpTest
    {
        private JumpModel? _testModel;
        private JumpControl _testObj;
        private GroundDetectionControl _testGroundCheck;

        private void ArrangeTestEnvironment(short airJump, float jumpForce, bool onGround)
        {
            _testModel = new JumpModel() {AirJumpLimit = airJump, JumpForce = jumpForce};
            _testObj = new JumpControl(_testModel.Value);
            _testGroundCheck = new GroundDetectionControl {OnGround = onGround};
            _testObj.GroundDetection = _testGroundCheck;
        }

        [TearDown]
        public void TearDown()
        {
            _testModel = null;
            _testObj = null;
            _testGroundCheck = null;
        }

        [Test]
        public void _0_0_When_OnGround_CanJump()
        {
            ArrangeTestEnvironment(0, 1, true);

            Assert.That(_testObj.CanJump, Is.True);
        }

        [Test]
        public void _0_1_When_NotOnGround_CannotJump()
        {
            ArrangeTestEnvironment(0, 1, false);

            Assert.That(_testObj.CanJump, Is.False);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void _1_0_When_NotOnGround_and_HaveAirJump_CanJump(short airJump)
        {
            ArrangeTestEnvironment(airJump, 1, false);

            Assert.That(_testObj.CanJump, Is.True);
        }

        [Test]
        public void _1_1_When_NotOnGround_and_DontHaveAirJump_CannotJump()
        {
            ArrangeTestEnvironment(0, 1, false);

            Assert.That(_testObj.CanJump, Is.False);
        }

        [Test]
        public void _2_0_When_JumpCalledOnGround_NotIncreaseAirJumpCount()
        {
            ArrangeTestEnvironment(3, 1, true);
            var lastJumpCount = _testObj.AirJumpCount;
            _testObj.JumpCall();
            Assert.That(_testObj.AirJumpCount, Is.EqualTo(lastJumpCount));
        }

        [Test]
        public void _2_1_When_JumpCalledNotOnGround_IncreaseAirJumpCount()
        {
            ArrangeTestEnvironment(3, 1, false);
            var lastJumpCount = _testObj.AirJumpCount;
            _testObj.JumpCall();
            Assert.That(_testObj.AirJumpCount, Is.Not.EqualTo(lastJumpCount));
        }

        [TestCase(1, 2)]
        public void _3_0_When_LandEventHappens_AirJumpCountReset(short airJump, int jumpCallTimes)
        {
            ArrangeTestEnvironment(airJump, 1, false);
            var lastJumpCount = _testObj.AirJumpCount;

            for (var i = 0; i < jumpCallTimes; i++)
                _testObj.JumpCall();
            _testGroundCheck.OnGround = true;

            Assert.That(_testObj.AirJumpCount, Is.Zero);
        }
    }
}