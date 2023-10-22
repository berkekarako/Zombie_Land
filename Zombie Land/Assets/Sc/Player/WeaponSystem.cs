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
        public WeaponBase currentWeapon = null;
        [SerializeField] private int maxWeapon = 3;
        [SerializeField] private LayerMask itemLayer;
        [SerializeField] private LayerMask enemyLayer;

        [SerializeField] private GameObject weaponsObj;
        [SerializeField] private GameObject interactItemUI;
        
        
        private void Start()
        {
            ChangeWeapon(weapons[0]);
        }

        private void Update()
        {
            currentWeapon.OnUpdate();
        }

        public void Attack()
        {
            currentWeapon.Attack();
        }

        public void GoToNextWeapon()
        {
            var index = weapons.IndexOf(currentWeapon);
            index++;
            if(index >= weapons.Count) index = 0;
            ChangeWeapon(weapons[index]);
        }

        public void InteractItem()
        {
            var asd = new Collider2D[2];
            Physics2D.OverlapCircleNonAlloc(transform.position, 3f, asd, itemLayer);
            
            if (asd[0].gameObject != currentWeapon.gameObject)
            {
                print(asd[0].name);
                asd[0].GetComponent<IInteractable>().Interact();
            }
            else
            {
                print(asd[1].name);
                asd[1].GetComponent<IInteractable>().Interact();
            }
            
            interactItemUI.SetActive(false);
        }

        private void ChangeWeapon(WeaponBase newWeapon)
        {
            currentWeapon?.UnEquip();
            currentWeapon = newWeapon;
            currentWeapon.changeEvent.Invoke();
            currentWeapon.Equip();
        }

        public void AddWeapon(WeaponItem item, WeaponBase newWeapon)
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
            else
            {
                weapons.Remove(currentWeapon);
                currentWeapon.transform.SetParent(null);
                currentWeapon.gameObject.SetActive(true);
                
                currentWeapon.gameObject.GetComponent<Collider2D>().enabled = true;
                currentWeapon.GetComponent<WeaponItem>().isPickedUp = false;
                
                currentWeapon = null;
                
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
