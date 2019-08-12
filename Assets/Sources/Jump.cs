using System.Collections;
using UnityEngine;
namespace as3mbus.Selfish.Source
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public abstract class Jump : MonoBehaviour
    {
        #region  fields

        protected Rigidbody2D _rigidBd;

        [SerializeField]
        private short _airJumpCount;
        [SerializeField]
        protected short _airJumpLimit;
        private bool _canJump
        { get => (_airJumpCount < _airJumpLimit || OnLand); }

        [SerializeField]
        protected float _jumpForce;

        [SerializeField]
        private GroundDetectionSystem _groundDetection;
        private bool OnLand
        { get => _groundDetection.OnGround; }

        #endregion

        #region Unity Messages
        protected virtual void Awake()
        {
            _groundDetection = GetComponent<GroundDetectionSystem>();
            _rigidBd = GetComponent<Rigidbody2D>();
        }

        protected virtual void Start()
        { _groundDetection._onStateChanges += OnGroundDetectEvent; }

        protected virtual void OnDestroy()
        { _groundDetection._onStateChanges -= OnGroundDetectEvent; }
        #endregion

        protected void Invoke()
        {
            if (!_canJump) return;
            if (!OnLand) _airJumpCount++;
            _rigidBd.velocity = Vector2.zero;
            _rigidBd.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
        private void OnGroundDetectEvent(bool onGround)
        { if (onGround) _airJumpCount = 0; }
    }
}