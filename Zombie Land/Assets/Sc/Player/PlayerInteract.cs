using System;
using Sc.Interfaces;
using Sc.Items;
using UnityEngine;

namespace Sc.Player
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private LayerMask interactLayer;
        [SerializeField] private GameObject interactItemUI;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IInteractable iInteractable))
            {
                interactItemUI.SetActive(true); // HATALI YAKINLAŞINCA ALMASINI YAP
            }
            else
            {
                interactItemUI.SetActive(false);
            }
        }
    }
}
