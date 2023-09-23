using System;
using System.Collections.Generic;
using Sc.Interfaces;
using Sc.Items;
using Sc.Weapon;
using UnityEngine;

namespace Sc.Player
{
    public class WeaponSystem : MonoBehaviour
    {
        public List<WeaponBase> weapons;
        [SerializeField] private WeaponBase currentWeapon = null;
        [SerializeField] private int maxWeapon = 3;
        [SerializeField] private LayerMask itemLayer;
        [SerializeField] private LayerMask enemyLayer;

        [SerializeField] private GameObject weaponsObj;
        [SerializeField] private GameObject interactItemUI;
        
        
        private void Start()
        {
            ChangeWeapon(weapons[0]);
        }

        public void Attack()
        {
            currentWeapon.Attack(enemyLayer);
        }

        public void GoToNextWeapon()
        {
            int index = weapons.IndexOf(currentWeapon);
            index++;
            if(index >= weapons.Count) index = 0;
            ChangeWeapon(weapons[index]);
        }

        public void InteractItem()
        {
            Collider2D asd = Physics2D.OverlapCircle(transform.position, 3f, itemLayer);
            asd.GetComponent<IInteractable>().Interact();
            interactItemUI.SetActive(false);
        }

        private void ChangeWeapon(WeaponBase newWeapon)
        {
            currentWeapon?.UnEquip();
            currentWeapon = newWeapon;
            currentWeapon.changeEvent.Invoke();
            currentWeapon.Equip();
        }

        public void AddWeapon(Item item, WeaponBase newWeapon)
        {
            if (weapons.Count < maxWeapon)
            {
                weapons.Add(newWeapon);
                ChangeWeapon(newWeapon);
                
                newWeapon.transform.SetParent(weaponsObj.transform);
                newWeapon.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
                
                item.isPickedUp = true;
                item.gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}
