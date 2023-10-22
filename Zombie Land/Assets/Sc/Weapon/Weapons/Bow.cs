using System;
using Sc.GeneralSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sc.Weapon.Weapons
{
    public class Bow : WeaponBase
    {
        [SerializeField] private int ammoCount = 50;
        [SerializeField] private float maxArrowSpeed = 20;
        [SerializeField] private float maxChargeTime = 2;
        [SerializeField] private GameObject arrowPrefab;

        private FixedJoystick _attackJoystick;
        private float _chargeTime = -0.01f;

        private Vector2 _previousJoystickDir;

        public override void Equip()
        {
            base.Equip();
            
            _attackJoystick = JoystickSystem.Instance.attackJoystick;
        }

        protected void SetChargeTime()
        {
            if (_attackJoystick.Direction != Vector2.zero)
            {
                _chargeTime += Time.deltaTime;
            }
            
            if(_chargeTime > 0 && _attackJoystick.Direction == Vector2.zero)
            {
                Attack();
                _chargeTime = 0;
            }
            
            _previousJoystickDir = _attackJoystick.Direction;
        }

        public override void Attack()
        {
            base.Attack();
            
            if(ammoCount <= 0) return;
            var clamp = Mathf.Clamp(_chargeTime, 0, maxChargeTime);
            
            var angle = Mathf.Atan2(_previousJoystickDir.y, _previousJoystickDir.x) * Mathf.Rad2Deg;
            if (angle < 0)
                angle += 360;
    
            var arrow = Instantiate(arrowPrefab, transform.position,
                Quaternion.Euler(new Vector3(0, 0, angle)));

            arrow.GetComponent<Rigidbody2D>().velocity = arrow.transform.right * (maxArrowSpeed * clamp);
            
            ammoCount--;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            SetChargeTime();
        }
    }
}
