using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Object.Item;

namespace HOGUS.Scripts.DP
{
    public abstract class Decorator : EquipmentItem
    {
        protected EquipmentItem equipment = null;
    }
}