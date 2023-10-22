using System;
using Sc.Interfaces;
using Sc.Player;
using Sc.Weapon;
using UnityEngine;

namespace Sc.Items
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class WeaponItem : MonoBehaviour, IInteractable
    {
        public bool isPickedUp = false;
        
        private WeaponBase _weapon;

        private void Start()
        {
            _weapon = GetComponent<WeaponBase>();
        }

        public void Interact()
        {
            if(isPickedUp) return;
            
            GameObject.FindWithTag("Player").GetComponent<WeaponSystem>().AddWeapon(this, _weapon);
        }
    }
}
