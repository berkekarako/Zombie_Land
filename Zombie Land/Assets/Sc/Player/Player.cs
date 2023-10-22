using System;
using Sc.GeneralSystem;
using Sc.Weapon;
using Sc.Weapon.Weapons;
using UnityEngine;

namespace Sc.Player
{
    public class Player : MonoBehaviour
    {
        public static  Player Instance { get; private set; }
        
        private Rigidbody2D _rb;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider2D;
        
        public PlayerHealth PlayerHealth { get; private set; }
        public WeaponSystem WeaponSystem { get; private set; }
        
        private FixedJoystick _joystick;
        
        private void Awake()
        {
            Instance = this;
            
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
            
            PlayerHealth = GetComponent<PlayerHealth>();
            WeaponSystem = GetComponent<WeaponSystem>();
        }

        private void Start()
        {
            _joystick = JoystickSystem.Instance.movementJoystick;
        }

        private void Update()
        {
            GetInput();
            Flip();
        }

        private void LateUpdate()
        {
            Anim();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void GetInput()
        {
            //_horizontal = Input.GetAxisRaw("Horizontal");
            //_vertical = Input.GetAxisRaw("Vertical");
            
            _horizontal = _joystick.Horizontal;
            _vertical = _joystick.Vertical;
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

        #region Other
        
        private void Anim()
        {
            _animator.SetFloat("Horz", Mathf.Abs(_horizontal));
            _animator.SetFloat("Vert", Mathf.Abs(_vertical));
        }

        void Flip()
        {
            if(_horizontal > 0)
                _spriteRenderer.flipX = false;
            else if(_horizontal < 0)
                _spriteRenderer.flipX = true;
        }
        
        #endregion
    }
}
