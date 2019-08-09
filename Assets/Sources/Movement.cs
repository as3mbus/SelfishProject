using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace as3mbus.Selfish.Source
{
    public abstract class Movement : MonoBehaviour
    {
        protected float _movementValue = 0;
        private Rigidbody2D rigidBd;
        [SerializeField]
        protected float _movementMultiplier = 2;
        // Start is called before the first frame update
        void Start()
        {
            rigidBd = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            // rigidBd.MovePosition(transform.position + Vector3.right * Time.deltaTime);
            // rigidBd.velocity += Vector2.right * _movementMultiplier * Time.deltaTime;
            _movementValue = Input.GetAxis("Horizontal");
            rigidBd.position += Vector2.right * _movementValue * _movementMultiplier * Time.deltaTime;
        }
    }
}