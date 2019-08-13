using System;
using UnityEngine;

namespace as3mbus.Selfish.Source
{
    public class PlayerBasicControl : MonoBehaviour
    {
        [SerializeField]
        private Movement _movement = null;
        [SerializeField]
        private Jump _jump = null;

        private void Update()
        {
            _movement._movementValue = Input.GetAxis("Horizontal");
            if (Input.GetKeyDown(KeyCode.Space)) _jump.Action();
        }
    }
}