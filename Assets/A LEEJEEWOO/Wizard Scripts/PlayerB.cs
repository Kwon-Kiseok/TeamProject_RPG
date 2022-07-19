using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerB : MonoBehaviour
{
    public JoystickB joysitck;
    public float speed;
    float hAxis;
    float vAxis;

    Rigidbody rb;
    bool isSkill;
    Vector3 moveVec;
    Animator animator;
    public GameObject skillpos;
    public float throwPower = 15f;

    public SkillBtnB basicFireBall;
    public SkillBtnB middleSunder;
    public SkillBtnB blizzard;
    public SkillBtnB plazmaBall;
    public SkillBtnB meteor;

    public GameObject BasicFactory;
    public GameObject middleFactory;
    public GameObject blizzardFactory;
    public GameObject plazmaBallFactory;
    public GameObject meteorFactory;


    private void Awake()
    {
        isSkill = false;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
    }

    private void FixedUpdate()
    {
        transform.position += moveVec * speed * Time.deltaTime;
    }

    void GetInput()
    {
        if (!isSkill)
        {
            hAxis = joysitck.GetAxisRaw("Horizontal");
            vAxis = joysitck.GetAxisRaw("Vertical");
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

    public void BasicFireBall()
    {
        isSkill = true;

        if (isSkill)
        {
            animator.SetTrigger("doNomalFire");
            GameObject BasicFireball0 = Instantiate(BasicFactory);
            BasicFireball0.transform.position = skillpos.transform.position;
            Rigidbody rb = BasicFireball0.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwPower, ForceMode.Impulse);

        }
    }

    public void MiddleSunder()
    {
        isSkill = true;

        if (isSkill)
        {
            animator.SetTrigger("doSunder");
            GameObject MiddleSunder0 = Instantiate(middleFactory);
            MiddleSunder0.transform.position = skillpos.transform.position;
            Rigidbody rb = MiddleSunder0.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwPower, ForceMode.Impulse);
        }
    }

    public void Blizzard()
    {
        isSkill = true;

        if (isSkill)
        {
            animator.SetTrigger("doBlizzard");
            GameObject Blizzard0 = Instantiate(blizzardFactory);
            Blizzard0.transform.position = skillpos.transform.position;
            Rigidbody rb = Blizzard0.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwPower, ForceMode.Impulse);            
        }
        
    }

    public void PlazmaBall()
    {
        isSkill = true;

        if (isSkill)
        {
            animator.SetTrigger("doPlazma");
            GameObject PlazmaBall0 = Instantiate(plazmaBallFactory);
            PlazmaBall0.transform.position = skillpos.transform.position;
            Rigidbody rb = PlazmaBall0.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwPower, ForceMode.Impulse);
        }
        
    }

    public void Meteor()
    {
        isSkill = true;

        if (isSkill)
        {
            animator.SetTrigger("doMeteor");
            GameObject Meteor0 = Instantiate(meteorFactory);
            Meteor0.transform.position = skillpos.transform.position;
            Rigidbody rb = Meteor0.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwPower, ForceMode.Impulse);
        }
        
    }

    public void EndSkillAnum()
    {
        isSkill = false;
    }
}
