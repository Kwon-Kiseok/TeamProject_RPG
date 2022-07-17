using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public Joystick joy;
    public float speed;
    float hAxis;
    float vAxis;

    bool isDodge;
    bool isSkill;

    Animator animator;
    Rigidbody rb;
    Vector3 moveVec;

    public GameObject skillPosition;
    public GameObject IceFactory;
    public float throwPower = 15f;

    public GameObject comboPTC;
    public GameObject hillPTC;

    public SkillBtn roading;
    public SkillBtn dashing;
    public SkillBtn comboing;
    public SkillBtn healing;

    public float player_Lv = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isSkill = false;
        comboPTC.gameObject.SetActive(false);
        hillPTC.gameObject.SetActive(false);
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();        
    }

    void FixedUpdate()
    {
        transform.position += moveVec * speed * Time.deltaTime;
    }

    void GetInput()
    {
        if (!isSkill)
        {
            hAxis = joy.GetAxisRaw("Horizontal");
            vAxis = joy.GetAxisRaw("Vertical");
        }
        else
        {
            moveVec = new Vector3(0, 0, 0);
        }
    }

    void Move()
    {
        if (!isSkill)
        {            
            if (hAxis != 0 || vAxis != 0)
            {
                animator.SetBool("isMove", true);
            }
            else
            {
                animator.SetBool("isMove", false);
            }

            moveVec = new Vector3(hAxis, 0, vAxis).normalized;            
        }

    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    public void Dodge()
    {    
        if (dashing.dash && moveVec != Vector3.zero)
        {
            speed *= 2f;
            animator.SetTrigger("doDodge");
            Invoke("DodgeOut", 0.4f);
        }
    }

    void DodgeOut()
    {
        speed *= 0.5f;
    }

    public void ComboAttack()
    {
        isSkill = true;
        //comboPTC.gameObject.SetActive(true);
        
        if (comboing.combo && isSkill)
        {
            animator.SetTrigger("doAttack");
            comboPTC.gameObject.SetActive(true);
        }
    }

    public void IceBall()
    {
        isSkill = true;
        
        if (roading.cool && isSkill)
        {
            animator.SetTrigger("doMasic");

            GameObject iceSkill = Instantiate(IceFactory);
            iceSkill.transform.position = skillPosition.transform.position;
            Rigidbody rb = iceSkill.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwPower, ForceMode.Impulse);
        }      
    }

    public void SkilHeal()
    {
        isSkill = true;
        //hillPTC.gameObject.SetActive(true);
        
        if (healing.heal && isSkill)
        {            
            animator.SetTrigger("doHeal");
            hillPTC.gameObject.SetActive(true);
        }
    }

    public void EndSkillAnim()
    {
        isSkill = false;
        comboPTC.gameObject.SetActive(false);
        hillPTC.gameObject.SetActive(false);
    }
}
