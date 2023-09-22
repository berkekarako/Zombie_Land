using System;
using Sc.Weapon;
using Sc.Weapon.Weapons;
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
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
            
            WeaponChange(circleSw);
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
            
            _horizontal = joystick.Horizontal;
            _vertical = joystick.Vertical;
            
            if(Input.GetKeyDown(KeyCode.Space))
                Attack();
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

        #region Attack

        [Header("Attack")] [SerializeField] private WeaponBase weaponBase;
        [SerializeField] private LayerMask enemyLayer;
        
        [SerializeField] private CircleSw circleSw;
        [SerializeField] private Bow bow;

        public void WeaponChange(WeaponBase newWeapon) // eğer silahı değiştirmek istyosan bunu kullan
        {
            weaponBase = newWeapon;
            weaponBase.changeEvent.Invoke();
        }
        
        public void Attack()
        {
            weaponBase.Attack(enemyLayer);
        }
        
        #endregion
    }
}
