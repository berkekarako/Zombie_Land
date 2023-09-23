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
        
        private int _interactableCount = 0;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IInteractable iInteractable))
            {
                interactItemUI.SetActive(true); // HATALI YAKINLAŞINCA ALMASINI YAP
                _interactableCount++;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out IInteractable iInteractable))
            {
                _interactableCount--;
                if(_interactableCount == 0)
                    interactItemUI.SetActive(false);
            }
        }
    }
}
