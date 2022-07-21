using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TaskGroupState
{
    Inactive,
    Running,
    Complete,
}

[System.Serializable]
public class TaskGroup
{
    [SerializeField]
    private Task[] tasks;

    public IReadOnlyList<Task> Tasks => tasks;
    public Quest Owner { get; private set; }
    public bool IsAllTaskComplete => tasks.All(x => x.IsComplete);
    public bool IsComplete => State == TaskGroupState.Complete;

    public TaskGroupState State { get; private set; }
    
    // 클로닝 구현
    // 다른 TaskGroup을 카피하는 생성자
    public TaskGroup(TaskGroup _CopyTarget)
    {
        tasks = _CopyTarget.tasks.Select(x => Object.Instantiate(x)).ToArray();
    }

    public void SetUp(Quest _owner)
    {
        Owner = _owner;
        foreach (var task in tasks)
        {
            task.SetUp(_owner);
        }
    }

    public void Start()
    {
        // Quest가 가진 여러 개의 TaskGroup 중에 현재 작동해야 하는 TaskGroup 시작될 때 실행
        State = TaskGroupState.Running;
        foreach (var task in tasks)
        {
            task.Start();
        }
    }

    public void End()
    {
        foreach(var task in tasks)
        {
            task.End();
        }
    }

    public void ReceiveReport(string _category, object _target, int _successCount)
    {
        // task가 해당 카테고리와 타겟을 갖고 있으면 보고를 받는다.
        foreach (var task in tasks)
        {
            if(task.IsTarget(_category, _target))
            {
                task.ReceiveReport(_successCount);
            }
        }
    }
    
    public void Complete()
    {
        if(IsComplete)
        {
            return;
        }
        State = TaskGroupState.Complete;
        foreach(var task in tasks)
        {
            if(!task.IsComplete)
            {
                task.Complete();
            }
        }
    }

}
