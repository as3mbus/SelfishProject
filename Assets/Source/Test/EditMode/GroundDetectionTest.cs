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
        public void GroundDetectionTestSimplePasses()
        {
            // Use the Assert class to test conditions
            Assert.IsTrue(true);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator GroundDetectionTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [Test]
        public void GroundDetection()
        {
            var test = new GroundDetectionControl();
            
        }
    }
}