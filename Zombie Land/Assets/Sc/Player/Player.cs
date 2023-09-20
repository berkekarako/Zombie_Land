using System;
using UnityEngine;

namespace Sc.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private FixedJoystick joystick;
        private Rigidbody2D _rb;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider2D;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            //_animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
        }

        private void Update()
        {
            GetInput();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void GetInput()
        {
            /*_horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");*/
            
            _horizontal = joystick.Horizontal;
            _vertical = joystick.Vertical;
        }

        #region Movement

        [Header("Movement")]
        [SerializeField] private float speed = 5f;
        
        private float _horizontal;
        private float _vertical;
        private Vector2 _getNormalizedVec() => new Vector2(_horizontal, _vertical).normalized;

        private void Move()
        {
            _rb.velocity = _getNormalizedVec() * speed;
        }

        #endregion
    }
}
