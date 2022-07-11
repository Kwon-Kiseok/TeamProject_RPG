using UnityEngine;

namespace HOGUS.Scripts.DP
{
    public abstract class Observer : MonoBehaviour
    {
        public abstract void Notify(Subject subject);
    }
}
