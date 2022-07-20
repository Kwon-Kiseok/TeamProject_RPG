using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskTarget : ScriptableObject
{
    // 퀘스트 시스템에 보고된 타겟이 task에 설정한 타겟과 같은지 확인
    public abstract bool IsEqual(object target);

    // 타겟을 외부로 가져올 수 있게
    // object형인 이유는 실제 타겟의 자료형은 이 클래스를 상속받는 자식에서 구현할 것이기 때문
    public abstract object Value { get; }
}
