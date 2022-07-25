using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPlayerLv : MonoBehaviour
{
    public float Lv = 1f;

    public UnityEngine.Events.UnityEvent onDead;

    // 스피드 조정 변수
    [SerializeField]
    private float walkSpeed;

    private float applySpeed;

    // 상태 변수
    private bool isWalk = false;

    // 땅 착지 여부
    private CapsuleCollider capsuleCollider;

    // 민감도
    [SerializeField]
    private float lookSensitivity;

    private Rigidbody myRigid;

    RaycastHit hitInfo;
    public LayerMask layerMask;

    public UIManager uIManager;
    public GameObject scanObject;

    public QuestManager questManager;

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
    }

    private void Update()
    {
        Move();
        CharacterRotation();
        NpcAction();
        TestMonsterKill();
    }

    // 움직임 실행
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * h;
        Vector3 _moveVertical = transform.forward * v;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        isWalk = true;
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    // 좌우 캐릭터 회전
    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    public void NpcAction()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 10f, layerMask))
            {
                if(hitInfo.collider != null)
                {
                    scanObject = hitInfo.collider.gameObject;
                    uIManager.Action(scanObject);
                }
                else
                {
                    scanObject = null;
                }
            }
        }
    }

    public void TestMonsterKill()
    {
        if(Input.GetKeyDown (KeyCode.Alpha1))
        {
            onDead.Invoke();
            uIManager.talkQuestIndex++;
            questManager.questId += 10;
        }
    }

}
