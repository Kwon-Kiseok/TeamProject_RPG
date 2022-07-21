using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Action/PositiveCount", fileName = "PositiveCount")]
public class PositiveCount : TaskAction
{
    public override int Run(Task task, int currentSuccess, int successCount)
    {
        // 넘어오는 successCount가 양수면 현재 성공횟수에 성공횟수를 추가해주고 아니면 그냥 현재 성공횟수만 반환
        return successCount > 0 ? currentSuccess + successCount : currentSuccess;
    }
}
