using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using NSubstitute;

namespace as3mbus.Selfish.Source.Test.EditMode
{
    [TestFixture]
    public class GroundDetectionTest
    {
        [Test]
        public void _0_ifStateChangeEventCalled()
        {
            // arrange
            var test = new GroundDetectionControl();
            var eventCall = 0;
            test.OnStateChanges += (b) => { eventCall++; };
            // act
            test.OnGround = true;
            // assert
            Assert.IsTrue(eventCall == 1);

        }
    }
}