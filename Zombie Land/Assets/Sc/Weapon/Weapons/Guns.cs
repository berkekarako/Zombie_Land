using System.Collections;
using Sc.GeneralSystem;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

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
        
        private Slider _reloadSlider;
        
        private float _lastFireTime;
        private FixedJoystick _joystick;

        private int _currentAmmoCount;
        private bool _isChanging;
        
        private Tween _reloadSliderTween;
        private float _angle;

        public override void OnUpdate()
        {
            TakeControl();
        }   

        public override void Equip()
        {
            base.Equip();
            
            transform.eulerAngles = Vector3.zero;
            
            _joystick = CUISystem.Instance.attackJoystick;
            _reloadSlider = CUISystem.Instance.playerReloadSlider;
            
            _reloadSlider.gameObject.SetActive(true);
            
            CheckReloadSlider();
            
            if(_currentAmmoCount == 0 && maxAmmoCount > 0) StartCoroutine(ChangeMagazine());
        }

        public override void UnEquip()
        {
            base.UnEquip();

            _reloadSlider.gameObject.SetActive(false);
                
            if (_isChanging)
            {
                _isChanging = false;
                StopCoroutine(ChangeMagazine());
                _reloadSliderTween.Kill();
            }
        }

        void TakeControl()
        {
            SetDirection();
            
            if (_joystick.Direction != Vector2.zero && Time.time > _lastFireTime + fireTime && _currentAmmoCount > 0)
            {
                Attack();
                _lastFireTime = Time.time;
            }
            
            else if (_currentAmmoCount == 0 && !_isChanging && maxAmmoCount > 0)
            { 
                StartCoroutine(ChangeMagazine());
            }
        }

        public override void Attack()
        {
            
            
            var newBullet = Instantiate(bullet, transform.position, 
                Quaternion.Euler(new Vector3(0, 0, _angle)));
            
            newBullet.GetComponent<Rigidbody2D>().velocity = newBullet.transform.right * speed;
            
            _currentAmmoCount--;

            CheckReloadSlider();
        }
        
        protected virtual IEnumerator ChangeMagazine()
        {
            _isChanging = true;
            
            PlayReloadAnim();
            
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
            
            CheckReloadSlider();
            
            _isChanging = false;
        }

        private void PlayReloadAnim()
        {
            _reloadSlider.value = 0;
            
            CUISystem.Instance.playerReloadImage.color = Color.green;
                
            _reloadSliderTween = _reloadSlider.DOValue(0.9f, changingMagazineTime * 0.9f).OnComplete(() =>
            {   
                CUISystem.Instance.playerReloadImage.color = Color.yellow;
                _reloadSlider.DOValue(1, changingMagazineTime * 0.1f)
                    .OnComplete(() =>
                    {
                        CUISystem.Instance.playerReloadImage.color = Color.cyan;
                    });
            });
        }

        private void PlayNoMagazineAnim()
        {
            _reloadSlider.value = 1;
            CUISystem.Instance.playerReloadImage.color = Color.red;
        }
        
        private void CheckReloadSlider()
        {
            if (_currentAmmoCount == 0)
            {
                if(maxAmmoCount <= 0) PlayNoMagazineAnim();
            }
            else
            {
                _reloadSlider.value = _currentAmmoCount * 100 / (float)maxMagazine / 100;
                CUISystem.Instance.playerReloadImage.color = Color.cyan;
            }
        }

        private void SetDirection()
        {
            if (_joystick.Direction != Vector2.zero)
            {
                _angle = Mathf.Atan2(_joystick.Direction.y, _joystick.Direction.x) * Mathf.Rad2Deg;
                if (_angle < 0)
                    _angle += 360;
                transform.eulerAngles = new Vector3(0, 0, _angle);
            }
        }
    }
}
