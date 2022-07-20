using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Action/ContinousCount", fileName = "ContinousCount")]
public class ContinuousCount : TaskAction
{
    // 성공 값으로 양수가 들어오면 count 아니면 0으로 초기화
    public override int Run(Task task, int currentSuccess, int successCount)
    {
        return successCount > 0 ? currentSuccess + successCount : 0;
    }
}
