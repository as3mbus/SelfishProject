using UnityEngine;

namespace as3mbus.Selfish.Source
{
    public class DefaultGroundDetection : GroundDetectionSystem
    {
        private Collider2D _cld;
        [SerializeField]
        private float _offset;
        [SerializeField]
        private LayerMask groundLayer;
        private void Awake()
        { _cld = GetComponent<Collider2D>(); }
        protected override bool GroundCheck()
        {
            return Physics2D.OverlapArea(
                new Vector2(_cld.bounds.min.x + _offset, _cld.bounds.min.y - 0.1f),
                new Vector2(_cld.bounds.max.x - _offset, _cld.bounds.min.y - _offset),
                groundLayer
            );
        }
    }
}