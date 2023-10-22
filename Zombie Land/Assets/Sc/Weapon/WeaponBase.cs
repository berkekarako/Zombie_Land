using System;
using Sc.GeneralSystem;
using Sc.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Sc.Weapon
{
    public class WeaponBase : MonoBehaviour
    {
        [Serializable] public enum WeaponUseType
        {
            Button,
            Joystick
        }
        
        public UnityEvent changeEvent;
        [SerializeField] protected WeaponUseType weaponUseType;
        
        private WeaponSystem _weaponSystem;

        private void Start()
        {
            changeEvent.AddListener(SetWeaponUseType);
        }

        public virtual void Attack()
        {
            print("Attack");
        }

        public virtual void Equip()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnUpdate() {}
        
        public virtual void UnEquip()
        {
            gameObject.SetActive(false);
        }

        private void SetWeaponUseType()
        {
            JoystickSystem.SetAttackJoystick(weaponUseType != WeaponUseType.Button);
        }
    }
}
