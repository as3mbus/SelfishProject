using System.Collections;
using UnityEngine;

namespace as3mbus.Selfish.Source
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Jump : MonoBehaviour
    {
        #region fields

        protected Rigidbody2D _rigidBd;

        [SerializeField] private short _airJumpCount;
        public short AirJumpLimit;
        public float JumpForce;
        [SerializeField] private GroundDetectionSystem _groundDetection;

        public GroundDetectionSystem GroundDetection
        {
            get => _groundDetection;
            set
            {
                if (_groundDetection)
                    _groundDetection.OnStateChanges -= OnGroundDetectEvent;
                if (value)
                    _groundDetection.OnStateChanges += OnGroundDetectEvent;
                _groundDetection = value;
            }
        }

        #endregion

        #region properties

        private bool CanJump => (_airJumpCount < AirJumpLimit || OnLand);

        private bool OnLand => _groundDetection.OnGround;

        #endregion

        #region Unity Messages

        protected virtual void Awake()
        {
            _rigidBd = GetComponent<Rigidbody2D>();
        }

        protected virtual void Start()
        {
            if (_groundDetection)
                _groundDetection.OnStateChanges += OnGroundDetectEvent;
        }

        protected virtual void OnDestroy()
        {
            GroundDetection = null;
        }

        #endregion

        public void Action()
        {
            if (!CanJump) return;
            if (!OnLand) _airJumpCount++;
            _rigidBd.velocity = Vector2.zero;
            _rigidBd.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }

        private void OnGroundDetectEvent(bool onGround)
        {
            if (onGround) _airJumpCount = 0;
        }
    }
}