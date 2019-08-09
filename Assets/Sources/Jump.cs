using UnityEngine;
namespace as3mbus.Selfish.Source
{
    public class Jump : MonoBehaviour
    {
        Collider2D _cld;
        Rigidbody2D _rigidBd;
        [SerializeField]
        private uint _jumpCount;
        [SerializeField]
        protected uint _jumpLimit;
        [SerializeField]
        protected float _jumpForce;
        [SerializeField]
        protected float _offset;
        private bool _onLand;
        private bool OnLand
        {
            get => _onLand;
            set
            {
                if (value) _jumpCount = 0;
                _onLand = value;
            }
        }
        private bool _canJump
        { get => _jumpCount < _jumpLimit; }
        private void Awake()
        {
            _cld = GetComponent<Collider2D>();
            _rigidBd = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            if (_onLand) return;
            if (_rigidBd.velocity.y != 0) return;
            Debug.Log("something below");
            OnLand = true;
        }
        protected void Invoke()
        {
            if (!_canJump) return;
            OnLand = false;
            _jumpCount++;
            _rigidBd.velocity = new Vector2(_rigidBd.velocity.x, 1);
            _rigidBd.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}