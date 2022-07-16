using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using HOGUS.Scripts.Manager;
using HOGUS.Scripts.DP;
using HOGUS.Scripts.Interface;


public class FPS_Check : MonoBehaviour, IUpdatableObject
{
    float deltaTime = 0.0f;

    public TextMeshProUGUI counterUI;

    // 키값으로 사용할 현재 상태
    private enum State
    {
        STOP,
        RUN,
    }
    // 객체의 상태들을 키, 밸류로 접근할 딕셔너리
    private Dictionary<State, IState> dictState = new();
    // fsm
    StateMachine stateMachine;

    private void Start()
    {
        // 정의된 상태 클래스들 객체 생성
        var stateStop = new StateStop(this);
        var stateRun = new StateRun(this);

        // 생성된 상태 객체들을 딕셔너리에 키, 밸류로 저장
        dictState.Add(State.STOP, stateStop);
        dictState.Add(State.RUN, stateRun);

        // fsm 생성 및 Initial state 초기화
        stateMachine = new StateMachine(stateRun);
    }

    //void OnGUI()
    //{
    //    int w = Screen.width, h = Screen.height;

    //    GUIStyle style = new GUIStyle();

    //    Rect rect = new Rect(0, 0, w, h * 2 / 100);
    //    style.alignment = TextAnchor.UpperLeft;
    //    style.fontSize = h * 2 / 100;
    //    style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
    //    float msec = deltaTime * 1000.0f;
    //    float fps = 1.0f / deltaTime;
    //    string text = string.Format("{0:0.0}?ms?({1:0.}?fps)", msec, fps);
    //    GUI.Label(rect, text, style);
    //}

    public void OnEnable()
    {
        UpdateManager.Instance.RegisterUpdatableObject(this);
    }

    public void OnDisable()
    {
        // 어플리케이션 종료 시 싱글턴 매니저 객체가 먼저 소멸될 경우는 실행되면 안됨
        if(UpdateManager.Instance != null)
        {
            UpdateManager.Instance.DeregisterUpdatableObject(this);
        }
    }

    public void OnFixedUpdate(float deltaTime)
    {
    }

    public void OnUpdate(float deltaTime)
    {
        // fsm에 저장된 현재 상태가 StateRun
        if (stateMachine.CurrentState == dictState[State.RUN])
        {
            this.deltaTime += (deltaTime - this.deltaTime) * 0.1f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.SetState(dictState[State.STOP]);
            }
        }
        // fsm에 저장된 현재 상태가 StateStop
        else if (stateMachine.CurrentState == dictState[State.STOP])
        {
            this.deltaTime = 0f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.SetState(dictState[State.RUN]);
            }
        }

        float msec = this.deltaTime * 1000.0f;
        float fps = 1.0f / this.deltaTime;
        counterUI.text = string.Format("{0:0.0}?ms?({1:0.}?fps)", msec, fps);
    }
}

#region states
// 멈춰있는 상태
public class StateStop : IState
{
    // 상태를 소유하고 있는 클래스 객체 생성
    private FPS_Check fps_Check;

    // 생성자를 통해 상태를 소유하고 있는 객체에 접근
    public StateStop(FPS_Check FC)
    {
        fps_Check = FC;
    }

    public void StateEnter()
    {
        Debug.Log("State Stop Enter");
    }

    public void StateExit()
    {
        Debug.Log("State Stop Exit");
    }

    public void StateFixedUpdate()
    {
    }

    public void StateUpdate()
    {
    }
}

public class StateRun : IState
{
    private FPS_Check fps_Check;

    public StateRun(FPS_Check FC)
    {
        fps_Check = FC;
    }

    public void StateEnter()
    {
        Debug.Log("State Run Enter");
    }

    public void StateExit()
    {
        Debug.Log("State Run Exit");
    }

    public void StateFixedUpdate()
    {
    }

    public void StateUpdate()
    {
    }
}
#endregion