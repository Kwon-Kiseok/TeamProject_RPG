using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    [Header("옵션")]
    [SerializeField]
    private bool useAutoComplete;

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

    public TaskGroup CurrentTaskGroup => taskGroups[currentTaskGroupIndex];
    public IReadOnlyList<TaskGroup> TaskGroups => taskGroups;
    public bool IsRegistered => State != QuestState.Inactive;
    public bool IsCompletable => State == QuestState.WaitForCompletion;
    public bool IsComplete => State == QuestState.Complete;
    public bool IsCancel => State == QuestState.Cancel;

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
        Debug.Assert(!IsRegistered, "This quest has already been registered.");
    }

    public void Complete()
    {

    }

    public void Cancel()
    {

    }

    private void OnSuccessChanged(Task task, int currentSuccess, int prevSuccess)
        => onTaskSuccessChanged?.Invoke(this, task, currentSuccess, prevSuccess);
}

