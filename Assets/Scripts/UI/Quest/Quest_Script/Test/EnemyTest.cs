using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent onDead;

    // 피가 까이는 함수 안에 구현

    public void Got()
    {
        onDead.Invoke();
    }
}
