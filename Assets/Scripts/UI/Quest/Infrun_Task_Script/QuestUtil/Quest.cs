using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using UnityEngine;

//using Debug = UnityEngine.Debug;

public enum QuestState
{
    Inactive,
    Running,
    Complete,
    Cancel,
    WaitForCompletion,
}

[CreateAssetMenu(menuName = "Quest/Quest", fileName = "Quest_")]
public class Quest : ScriptableObject
{
    #region Events
    public delegate void TaskSuccessChangeHandler(Quest quest, Task task, int currentSuccess, int prevSuccess);
    public delegate void CompletedHandler(Quest quest);
    public delegate void CancelHandler(Quest quest);
    public delegate void NewTaskGroupHandler(Quest quest, TaskGroup currentTaskGroup, TaskGroup prevTaskGroup);
    #endregion

    [SerializeField]
    private Category category;
    [SerializeField]
    private Sprite icon;

    [Header("퀘스트 속성")]
    [SerializeField]
    private string codeName;
    [SerializeField]
    private string displayName;
    [SerializeField, TextArea]
    private string description;

    [Header("Task")]
    [SerializeField]
    private TaskGroup[] taskGroups;

    [Header("퀘스트 보상")]
    [SerializeField]
    private Reward[] rewards;

    [Header("옵션")]
    [SerializeField]
    private bool useAutoComplete;
    [SerializeField]
    private bool isCancelable;
    [SerializeField]
    private bool isSavable;     // save할 퀘인지 체크

    [Header("퀘스트 조건")]
    [SerializeField]
    private Condition[] acceptionConditions;
    [SerializeField]
    private Condition[] cancelConditions;

    private int currentTaskGroupIndex;

    public Category Category => category;
    public Sprite Icon => icon; 
    public string CodeName => codeName;
    public string DisplayName => displayName;
    public string Description => description;
    public QuestState State { get; private set; }
    // 슬라임을 10마리 잡아라
    //
    // 레드 슬라임을 10마리 잡아라
    // 블루 슬라임을 10마리 잡아라 -> 그린 슬라임을 10마리 잡아라

    public TaskGroup CurrentTaskGroup //=> taskGroups[currentTaskGroupIndex];
    {
        get 
        {
            return taskGroups[currentTaskGroupIndex]; 
        }
    }

    public IReadOnlyList<TaskGroup> TaskGroups => taskGroups;
    public IReadOnlyList<Reward> Rewards => rewards;
    public bool IsRegistered => State != QuestState.Inactive;
    public bool IsCompletable => State == QuestState.WaitForCompletion;
    public bool IsComplete => State == QuestState.Complete;
    public bool IsCancel => State == QuestState.Cancel;
    public virtual bool IsCancelable => isCancelable && cancelConditions.All(x => x.IsPass(this));
    public bool IsAcceptable => acceptionConditions.All(x => x.IsPass(this));
    public virtual bool IsSavable => isSavable;     // 업적은 세이브 되야 하니 가상으로 구현

    public event TaskSuccessChangeHandler onTaskSuccessChanged;
    public event CompletedHandler onCompleted;
    public event CancelHandler onCanceled;
    public event NewTaskGroupHandler onNewTaskGroup;

    public void OnRegister()
    {
        // 인자로 들어온 값이 false면 문장을 error로 띄어준다.
        Debug.Assert(!IsRegistered, "This quest has already been registered.");

        foreach(var taskGroup in taskGroups)
        {
            taskGroup.SetUp(this);
            foreach(var task in taskGroup.Tasks)
            {
                task.onSuccessChanged += OnSuccessChanged;
            }
        }

        State = QuestState.Running;
        CurrentTaskGroup.Start();
    }

    public void ReceiveReport(string _category, object _target, int _successCount)
    {
        Debug.Assert(IsRegistered, "This quest has already been registered.");
        Debug.Assert(!IsCancel, "This quest has been canceled");

        if (IsComplete)
            return;
        CurrentTaskGroup.ReceiveReport(_category, _target, _successCount);   

        if(CurrentTaskGroup.IsAllTaskComplete)
        {
            if(currentTaskGroupIndex + 1 == taskGroups.Length) // 다음 TaskGroup이 없다면
            {
                State = QuestState.WaitForCompletion;
                if(useAutoComplete) // 자동으로 퀘스트가 깨지는 useAutoComplete가 활성화 되있다면
                {
                    Complete();
                }
                else // // 다음 TaskGroup이 있다면
                {
                    var prevTaskGroup = taskGroups[currentTaskGroupIndex++]; // 다음 taskGroup을 가져오면서 인덱스 증가시키고
                    prevTaskGroup.End(); // 이전 taskGroup은 끝내고
                    CurrentTaskGroup.Start(); // 현재 taskGroup은 시작
                    onNewTaskGroup?.Invoke(this, CurrentTaskGroup, prevTaskGroup); // 새로운 taskGroup이 시작됐다는 것을 이벤트를 통해 알려준다.
                }
            }
        }
        else
        {
            // 다시 task가 안 깨진 상태가 될 수 있기 때문
            State = QuestState.Running;
        }
    }

    public void Complete()
    {
        // 퀘스트 즉시 완료해주는 아이템이나 시스템에 의해서 퀘스트의 task가 완료되지 않았지만 Complete하는 경우

        CheckIsRunning();

        foreach (var taskGroup in taskGroups)
        {
            taskGroup.Complete();
        }

        State = QuestState.Complete;

        foreach(var reward in rewards)
        {
            reward.Give(this);
        }

        onCompleted?.Invoke(this);

        onTaskSuccessChanged = null;
        onCompleted = null;
        onCanceled = null;
        onNewTaskGroup = null;
    }

    public virtual void Cancel()
    {
        CheckIsRunning();
        Debug.Assert(IsCancelable, "this quest can't be canceled");

        State = QuestState.Cancel;
        onCanceled?.Invoke(this);
    }

    // 클로닝 구현
    public Quest Clone()
    {
        var clone = Instantiate(this);
        clone.taskGroups = taskGroups.Select(x => new TaskGroup(x)).ToArray();

        return clone;
    }

    public QuestSaveData ToSaveData()
    {
        return new QuestSaveData
        {
            codeName = codeName,
            state = State,
            taskGroupIndex = currentTaskGroupIndex,
            taskSuccessCounts = CurrentTaskGroup.Tasks.Select(x => x.CurrentSuccess).ToArray()
        };
    }

    public void LoadFrom(QuestSaveData saveData)
    {
        State = saveData.state;
        currentTaskGroupIndex = saveData.taskGroupIndex;

        for (int i = 0; i < currentTaskGroupIndex; i++)
        {
            var taskGroup = taskGroups[i];
            taskGroup.Start();
            taskGroup.Complete();
        }

        for (int i = 0; i < saveData.taskSuccessCounts.Length; i++)
        {
            CurrentTaskGroup.Start();
            CurrentTaskGroup.Tasks[i].CurrentSuccess = saveData.taskSuccessCounts[i];
        }
    }

    private void OnSuccessChanged(Task task, int currentSuccess, int prevSuccess)
        => onTaskSuccessChanged?.Invoke(this, task, currentSuccess, prevSuccess);

    [Conditional("UNITY_EDITOR")] // 인자로 전달한 Simbol 값이 선언되어 있으면 함수 실행
    private void CheckIsRunning()
    {
        Debug.Assert(IsRegistered, "This quest has already been registered.");
        Debug.Assert(!IsCancel, "This quest has been canceled");
        Debug.Assert(!IsComplete, "This quest has already been Completed");
    }
}

