using System;
using Sc.Interfaces;
using Sc.Player;
using Sc.Weapon;
using UnityEngine;

namespace Sc.Items
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Item : MonoBehaviour, IInteractable
    {
        public WeaponBase weapon;
        public bool isPickedUp = false;
        
        public void Interact()
        {
            if(isPickedUp) return;
            WeaponSystem player = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponSystem>();
                
            player.AddWeapon(this, weapon);
        }
    }
}
