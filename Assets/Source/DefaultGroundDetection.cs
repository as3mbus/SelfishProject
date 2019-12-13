using UnityEngine;

namespace as3mbus.Selfish.Source
{
    public class DefaultGroundDetection : GroundDetectionSystem
    {
        private Collider2D _cld;
        [SerializeField] private float _offset = 0;
        [SerializeField] private LayerMask _groundLayer = ~0;

        private void Awake()
        {
            _cld = GetComponent<Collider2D>();
        }

        protected override bool GroundCheck()
        {
            var bounds = _cld.bounds;
            return Physics2D.OverlapArea(
                new Vector2(bounds.min.x + _offset, bounds.min.y - 0.1f),
                new Vector2(bounds.max.x - _offset, bounds.min.y - _offset),
                _groundLayer
            );
        }
    }
}