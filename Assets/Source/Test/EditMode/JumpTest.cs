using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

namespace as3mbus.Selfish.Source.Test.EditMode
{
    public class JumpTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void JumpTestSimplePasses()
        {
            // Use the Assert class to test conditions
            Assert.IsTrue(true);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator MovementTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [Test]
        public void _1_jump()
        {
            
        }
    }
}