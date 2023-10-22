using System.Collections;
using Sc.GeneralSystem;
using UnityEngine;
using DG.Tweening;

namespace Sc.Weapon.Weapons
{
    public class Guns : WeaponBase
    {
        [SerializeField] private float speed = 25;
        [SerializeField] private int maxAmmoCount = 100;
        [SerializeField] private int maxMagazine = 15;
        [SerializeField] private float changingMagazineTime = 1;
        [SerializeField] private float fireTime = 1/25f;
        [SerializeField] private GameObject bullet;
        
        private float _lastFireTime;
        private FixedJoystick _joystick;

        private int _currentAmmoCount;
        private bool _isChanging;

        public override void OnUpdate()
        {
            base.OnUpdate();
            TakeControl();
        }

        public override void Equip()
        {
            base.Equip();
            
            _joystick = JoystickSystem.Instance.attackJoystick;
            
            if(_currentAmmoCount == 0)
            {
                _isChanging = true;
                StartCoroutine(ChangeMagazine());
            }
        }

        public override void UnEquip()
        {
            base.UnEquip();
            
            print("Kapat");

            if (_isChanging)
            {
                _isChanging = false;
                StopCoroutine(ChangeMagazine());
                SetReloadAnim(false);
            }
        }

        void TakeControl()
        {
            if (_joystick.Direction != Vector2.zero && Time.time > _lastFireTime + fireTime && _currentAmmoCount > 0)
            {
                Attack();
                _lastFireTime = Time.time;
            }
            else if (_currentAmmoCount == 0 && !_isChanging)
            { 
                StartCoroutine(ChangeMagazine());
            }
        }

        public override void Attack()
        {
            base.Attack();
            
            var angle = Mathf.Atan2(_joystick.Direction.y, _joystick.Direction.x) * Mathf.Rad2Deg;
            if (angle < 0)
                angle += 360;
            
            var newBullet = Instantiate(bullet, transform.position, 
                Quaternion.Euler(new Vector3(0, 0, angle)));
            
            newBullet.GetComponent<Rigidbody2D>().velocity = newBullet.transform.right * speed;
            
            _currentAmmoCount--;
        }
        
        protected virtual IEnumerator ChangeMagazine()
        {
            _isChanging = true;
            
            SetReloadAnim(true);
            
            yield return new WaitForSeconds(changingMagazineTime);
            
            if (maxAmmoCount >= maxMagazine)
            {
                _currentAmmoCount = maxMagazine;
                maxAmmoCount -= maxMagazine;
            }
            else if (maxAmmoCount > 0)
            {
                _currentAmmoCount = maxAmmoCount;
                maxAmmoCount = 0;
            }
            
            _isChanging = false;
        }

        private void SetReloadAnim(bool val)
        {
            JoystickSystem.Instance.playerReloadSlider.value = 0;
                
            if (val)
            {
                JoystickSystem.Instance.playerReloadImage.color = Color.cyan;
                JoystickSystem.Instance.playerReloadSlider.gameObject.SetActive(true);
                JoystickSystem.Instance.playerReloadSlider.DOValue(0.9f, changingMagazineTime * 0.9f).OnComplete(() =>
                {
                    JoystickSystem.Instance.playerReloadImage.color = Color.yellow;
                    JoystickSystem.Instance.playerReloadSlider.DOValue(1, changingMagazineTime * 0.1f)
                        .OnComplete(() =>
                        {
                            JoystickSystem.Instance.playerReloadSlider.gameObject.SetActive(false);
                        });
                });
            }
            else
            {
                JoystickSystem.Instance.playerReloadSlider.gameObject.SetActive(false);
            }
        }
    }
}
