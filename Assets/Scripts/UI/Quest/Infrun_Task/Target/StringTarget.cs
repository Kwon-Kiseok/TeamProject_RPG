using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Target/String", fileName = "Target_")]
public class StringTarget : TaskTarget
{
    // 문자 혹은 아이디로 받는 타겟
    [SerializeField]
    private string value;

    public override object Value => value;

    public override bool IsEqual(object target)
    {
        string targetAsString = target as string; // 타겟을 string형으로 캐스팅
        if(targetAsString == null)
        {
            // 같은 타입이 아니라는 뜻
            return false;
        }
        // value 값과 비교하여 캐스팅
        return value == targetAsString;

        // 예를 들어 내가 설정한 value가 Slime이라는 문자열이고
        // 들어온 target이 Slime이라는 문자열이라면 true가 리턴되어
        // 목표로 하는 target이 맞다는 뜻
    }
}
