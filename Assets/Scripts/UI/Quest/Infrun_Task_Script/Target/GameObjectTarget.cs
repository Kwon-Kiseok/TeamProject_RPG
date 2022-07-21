using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Target/GameObject", fileName = "Target_")]
public class GameObjectTarget : TaskTarget
{
    [SerializeField]
    private GameObject value;

    public override object Value => value;

    public override bool IsEqual(object target)
    {
        // 넘어오는 것이 Prefab일수도 실제 게임 Object일 수도 있다.
        // 그 차이로 false가 나올 수도 있어서 Object의 이름으로 비교
        // object가 많아지면 뒤에 (Clone)같은 문자가 붙기 때문에 원본의 이름으로 비교

        var targetAsGameObject = target as GameObject;
        if(targetAsGameObject == null)
        {
            return false;
        }
        return targetAsGameObject.name.Contains(value.name);
    }
}
