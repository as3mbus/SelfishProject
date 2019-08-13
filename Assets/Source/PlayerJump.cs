using UnityEngine;
namespace as3mbus.Selfish.Source
{
    public class PlayerJump : Jump
    {
        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                base.Invoke();
        }

    }
}