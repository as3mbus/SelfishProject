using UnityEngine;

namespace as3mbus.Selfish.Source
{
    public class PlayerMovement : Movement
    {
        protected override void Update() {
            _movementValue = Input.GetAxis("Horizontal");
            base.Update();
        }
    }
}