using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TaskState
{
    Inactive,
    Running,
    Complete,
}

[CreateAssetMenu(menuName = "Quest/Task/Task", fileName = "Task_")]
public class Task : ScriptableObject
{
    #region Events
    public delegate void StateChangedHandler(Task task, TaskState currentState, TaskState prevState);
    public delegate void SuccessChangedHandler(Task task, int currentSuccess, int prevSuccess);
    #endregion

    [SerializeField]
    private Category category;

    [Header("Task 정의")]
    [SerializeField]
    private string codeName;        // 검색과 같은 어떠한 기능을 위해 내부적으로 사용하는 이름
    [SerializeField]
    private string description;     // task에 대한 설명

    [Header("Action")]
    [SerializeField]
    private TaskAction action;

    [Header("Target")]
    [SerializeField]
    private TaskTarget[] targets;   // 타겟이 여러 개일 수도 있기 때문

    [Header("Task 세팅")]
    [SerializeField]
    private InitialSuccessValue initialSuccessValue;
    [SerializeField]
    private int needSuccessToComplete;
    [SerializeField]
    private bool canReceiveReportsDuringCompleTion; // 퀘스트가 완료되었을 때에도 계속 성공횟수를 보고 받을 것인지?

    private TaskState state;
    private int currentSuccess;

    public event StateChangedHandler onStateChanged;
    public event SuccessChangedHandler onSuccessChanged;    

    public int CurrentSuccess
    {
        get => currentSuccess;
        set
        {
            int prevSuccess = currentSuccess;
            currentSuccess = Mathf.Clamp(value, 0, needSuccessToComplete);
            if(currentSuccess != prevSuccess)
            {
                state = currentSuccess == needSuccessToComplete ? TaskState.Complete : TaskState.Running;
                onSuccessChanged?.Invoke(this, currentSuccess, prevSuccess);
            }

        }
    }

    public Category Category => category;
    public string CodeName => codeName;
    public string Description => description;
    public int NeedSuccessToComlete => needSuccessToComplete;
    
    public TaskState State
    {
        get => state;
        set
        {
            var prevState = state;
            state = value;
            onStateChanged?.Invoke(this, state, prevState); // ?. 이 변수가 null이면 null 반환 아님 뒤 함수 실행
        }
    }

    public bool IsComplete => State == TaskState.Complete;

    public Quest Owner { get; private set; }

    public void SetUp(Quest owner)
    {
        Owner = owner;
    }

    public void Start()
    {
        State = TaskState.Running;
        if(initialSuccessValue)
        {
            CurrentSuccess = initialSuccessValue.GetValue(this);
        }
    }

    public void End()
    {
        onStateChanged = null;
        onSuccessChanged = null;
    }

    public void Complete()
    {
        CurrentSuccess = needSuccessToComplete;
    }

    public void ReceiveReport(int _successCount)
    {
        // action.Run()함수는 Logic을 실행한 결과값 반환
        // Count하는 로직일 경우 CurrentSuccess와 _successCount가 더해져서 반환
        CurrentSuccess = action.Run(this, CurrentSuccess, _successCount);
    }

    // Any() https://docs.microsoft.com/ko-kr/dotnet/api/system.linq.enumerable.any?view=net-6.0
    public bool IsTarget(string _category ,object target)
        => Category == _category &&
        (!IsComplete || (IsComplete && canReceiveReportsDuringCompleTion)) &&
        targets.Any(x => x.IsEqual(target));     // 시퀀스에 요소가 하나라도 있는지 또는 특정 조건에 맞는 요소가 있는지 확인
                                                    // Task가 성공횟수를 보고 받은 대상인지 확인하는 함수
                                                    // 세팅해놓은 타겟들 중에 대상이 있으면 트루 아니면 펄스
}
