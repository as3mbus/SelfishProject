using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace as3mbus.SelfishProject.Sources
{
    public class Jump2D : MonoBehaviour
    {
        [SerializeField]
        private float _jumpForece;
        public float JumpForce
        { get => _jumpForece; }
        public bool onAir;
        public bool IsJumping;
        public bool JumpAvailable
        { get => !onAir && !IsJumping; }
        public void Jump()
        {

        }
    }

    class JumpSystem : ComponentSystem
    {
        struct ReqComponents
        {
            public Jump2D jump;
            public Rigidbody2D rigidBody;
        }
        // [Inject] ReqComponents components;
        protected override void OnUpdate()
        {
            foreach (var e in GetEntities<ReqComponents>())
            {
                if (!e.jump.IsJumping) continue;
                e.rigidBody.AddForce(Vector2.up * e.jump.JumpForce);
                // e.rigidBody.velocity.y >
            }
        }
    }
}