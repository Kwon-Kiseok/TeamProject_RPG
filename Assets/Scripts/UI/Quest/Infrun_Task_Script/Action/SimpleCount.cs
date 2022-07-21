using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Action/SimpleCount", fileName = "Simple Count")]
public class SimpleCount : TaskAction
{
    // 현재 성공 Count에 받은 성공 Count를 더해서 반환하는 모듈
    public override int Run(Task task, int currentSuccess, int successCount)
    {
        return currentSuccess + successCount;
    }
}
