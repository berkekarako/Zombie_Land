using System.Collections.Generic;
using Sc.Interfaces;
using UnityEngine;

namespace Sc.GeneralSystem
{
    public class HealthDamageSys
    {
        #region TakeDamage
        
        public static void TakeDamage(GameObject obj, float damage)
        {
            if (obj.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
        }
        
        public static void TakeDamage(GameObject[] objs, float damage)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage);
                }
            }
        }
        
        public static void TakeDamage(List<GameObject> objs, float damage)
        {
            for (int i = 0; i < objs.Count; i++)
            {
                if (objs[i].TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage);
                }
            }
        }
        
        public static void TakeDamage(Collider2D obj, float damage)
        {
            if (obj.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
        }
        
        public static void TakeDamage(Collider2D[] objs, float damage)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                if(!objs[i]) return;
                if (objs[i].gameObject.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage);
                }   
            }
        }
        
        public static void TakeDamage(List<Collider2D> objs, float damage)
        {
            for (int i = 0; i < objs.Count; i++)
            {
                if (objs[i].gameObject.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage);
                }
            }
        }
        
        #endregion
    }
}
