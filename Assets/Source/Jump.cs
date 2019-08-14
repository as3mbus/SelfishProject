using System.Collections;
using UnityEngine;

namespace as3mbus.Selfish.Source
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Jump : MonoBehaviour
    {
        #region fields

        protected Rigidbody2D _rigidBd;

        [SerializeField] private GroundDetectionSystem _groundDetection;
        private GroundDetectionControl GroundDetection => _groundDetection.Control;
        [SerializeField] private JumpControl _control;

        #endregion

        #region Unity Messages

        protected virtual void Awake()
        {
            _control.GroundDetection = GroundDetection;
            _rigidBd = GetComponent<Rigidbody2D>();
        }

        protected virtual void Start()
        {
            _control.GroundDetection = GroundDetection;
        }

        protected virtual void OnDestroy()
        {
            _control.GroundDetection = null;
        }

        #endregion

        public void Action()
        {
            if (!_control.CanJump) return;
            _control.JumpCall();
            _rigidBd.velocity = Vector2.zero;
            _rigidBd.AddForce(Vector2.up * _control.Model.JumpForce, ForceMode2D.Impulse);
        }
    }
}