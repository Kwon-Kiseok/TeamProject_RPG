using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent onDead;

    // �ǰ� ���̴� �Լ� �ȿ� ����

    public void Got()
    {
        onDead.Invoke();
    }
}
