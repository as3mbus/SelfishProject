using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace as3mbus.SelfishProject.Sources
{
    public class Movement2D : MonoBehaviour
    {
        public void Move(bool rightDir, float movVal)
        {
            _rightDirection = rightDir;
            _movementValue = movVal;
        }
        private bool rightMovementDirection
        {
            get => _movementValue > 0;
        }
        private bool _rightDirection;
        private float _movementValue = 0;
        [SerializeField]
        private float _moveSpeed = 3;
        public Vector2 MovementVector2
        { get => Vector2.right * _moveSpeed * _movementValue; }
    }

    class MovementSystem : ComponentSystem
    {
        struct ReqComponents
        {
            public Movement2D movement;
            public Rigidbody2D rigidBody;
        }
        protected override void OnUpdate()
        {
            foreach (var e in GetEntities<ReqComponents>())
            {
                e.rigidBody.velocity = new Vector2(e.movement.MovementVector2.x, e.rigidBody.velocity.y);
            }
        }
    }
    class MovementInputSystem : ComponentSystem
    {
        struct ReqComponents
        {
            public Movement2D movement;
            public AxisInput axisInput;
        }
        protected override void OnUpdate()
        {
            Debug.Log("movement input");
            foreach (var e in GetEntities<ReqComponents>())
            {
                Debug.Log("entity");
                e.movement.Move(e.axisInput.InputVector.x > 0, e.axisInput.InputVector.x);
                // if (Input.GetKeyDown(KeyCode.Space))
                // e.
            }

        }
    }
}