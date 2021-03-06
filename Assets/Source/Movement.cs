﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace as3mbus.Selfish.Source
{
    public class Movement : MonoBehaviour
    {
        public float _movementValue = 0;
        private Rigidbody2D rigidBd;
        [SerializeField]
        protected float _movementMultiplier = 2;
        // Start is called before the first frame update
        void Start()
        { rigidBd = GetComponent<Rigidbody2D>(); }

        // Update is called once per frame
        protected virtual void Update()
        {
            rigidBd.velocity = new Vector2(0, rigidBd.velocity.y);
            rigidBd.position += _movementValue * _movementMultiplier * Time.deltaTime * Vector2.right;
        }
    }
}