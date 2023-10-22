using System;
using UnityEngine;

namespace Sc.Weapon.Weapons.Bows
{
    public class NormalBow : Bow
    {
        public override void OnUpdate()
        {
            base.OnUpdate();
            print("Normal Bow");
        }
    }
}
