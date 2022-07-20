using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Task", fileName = "Task_")]
public abstract class TaskAction : ScriptableObject
{
    public abstract int Run(Task task, int currentSuccess, int successCount);
}
