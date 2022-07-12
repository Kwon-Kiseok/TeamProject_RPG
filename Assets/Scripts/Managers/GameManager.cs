using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.DP;

namespace HOGUS.Scripts.Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public bool IsGameOver { get; set; }
        public int Index { get; set; } = 0;
        protected GameManager() { }

        // ObjectPool delegate
        public Func<int, GameObject> objectPool;
        public Action<int, GameObject> returnPool;
    }
}
