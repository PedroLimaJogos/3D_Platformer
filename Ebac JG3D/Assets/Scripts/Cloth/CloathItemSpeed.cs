using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth{
    public class CloathItemSpeed : ClothItemBase
    {

        public float targetSpeed=2f;
        public override void Collect()
        {
            base.Collect();
            Player.Instance.ChangeSpeed(targetSpeed,duration);
        }
    }
}