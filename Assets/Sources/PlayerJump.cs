using UnityEngine;
namespace as3mbus.Selfish.Source
{
    public class PlayerJump : Jump
    {
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            base.Invoke();
        }

    }
}