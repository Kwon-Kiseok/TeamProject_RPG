using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Action/ContinousCount", fileName = "ContinousCount")]
public abstract class InitialSuccessValue : ScriptableObject
{
    // Task의 초기 성공 값을 결정해주는 클래스
    // 몬스터 수에 따라 초기 성공 값을 달리준다거나..
    public abstract int GetValue(Task task);
}
