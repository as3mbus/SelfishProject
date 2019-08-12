using System.Collections;
using UnityEngine;
namespace as3mbus.Selfish.Source
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public abstract class Jump : MonoBehaviour
    {
        #region  fields

        protected Collider2D _cld;
        protected Rigidbody2D _rigidBd;

        #region MultiJump
        [SerializeField]
        private short _airJumpCount;
        [SerializeField]
        protected short _airJumpLimit;
        private bool _canJump
        { get => (_airJumpCount < _airJumpLimit || _onLand); }
        #endregion

        [SerializeField]
        private LayerMask groundLayer;

        [SerializeField]
        protected float _jumpForce;
        [SerializeField]
        protected float _offset;

        [SerializeField]
        private bool _onLand;
        private bool OnLand
        {
            get => _onLand;
            set
            {
                if (value == _onLand) return;
                if (value) _airJumpCount = 0;
                _onLand = value;
            }
        }


        #endregion

        private void Awake()
        {
            _cld = GetComponent<Collider2D>();
            _rigidBd = GetComponent<Rigidbody2D>();
        }
        protected virtual void FixedUpdate()
        { OnLand = GroundCheck(); }

        protected void Invoke()
        {
            if (!_canJump) return;
            if (!_onLand) _airJumpCount++;
            _rigidBd.velocity = Vector2.zero;
            _rigidBd.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        // Velocity Ground Check
        //// private bool groundCheck()
        //// {
        ////     if (_rigidBd.velocity.y != 0) return false;
        ////     return true;
        //// }
        // CircleCast Groun dCheck
        //// private bool groundCheck2()
        //// {
        ////     var phys = Physics2D.CircleCastAll(_cld.bounds.center, _cld.bounds.extents.x, Vector2.down, _offset);
        ////     if (phys.Length <= 1) return false;
        ////     return true;
        //// }
        protected virtual bool GroundCheck()
        {
            return Physics2D.OverlapArea(
                new Vector2(_cld.bounds.min.x + _offset, _cld.bounds.min.y - 0.1f),
                new Vector2(_cld.bounds.max.x - _offset, _cld.bounds.min.y - _offset),
                groundLayer
            );
        }
    }
}