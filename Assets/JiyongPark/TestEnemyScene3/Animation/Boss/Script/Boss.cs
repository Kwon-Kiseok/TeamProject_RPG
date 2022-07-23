using HOGUS.Scripts.DP;
using HOGUS.Scripts.Interface;
using HOGUS.Scripts.Manager;
using HOGUS.Scripts.State;
using HOGUS.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace HOGUS.Scripts.Character
{
    public class Boss : MonsterController
    {
        public GameObject spell;
        public GameObject secondSpell;
        public Transform spellPort;
        public Transform spellPort2;
        public Transform arrowSpawnPort;

        void Awake()
        {
            StartCoroutine(Think());
            rigid = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();
            meshes = GetComponentsInChildren<MeshRenderer>();
        }


        IEnumerator Think()
        {
            yield return new WaitForSeconds(0.1f);

            int randomAction = Random.Range(0, 5);
            switch (randomAction)
            {
                case 0:
                case 1:
                    StartCoroutine(electricBall());
                    break;
                case 2:
                case 3:
                    StartCoroutine(electricArrow());
                    break;
                case 4:
                    //
                    break;
            }
        }
        

        // 패턴 3가지 

        IEnumerator electricBall()
        {
            animator.SetTrigger("Pattern_1");
            yield return new WaitForSeconds(0.5f);
            GameObject instantSpell = Instantiate(spell, spellPort.transform.position, spellPort.transform.rotation);
            BossShot bossSpell = instantSpell.GetComponent<BossShot>();
            bossSpell.target = player;
            yield return new WaitForSeconds(0.6f);
            GameObject instantSpell2 = Instantiate(spell, spellPort2.transform.position, spellPort2.transform.rotation);
            BossShot bossSpell2 = instantSpell2.GetComponent<BossShot>();
            bossSpell2.target = player;
            yield return new WaitForSeconds(2.0f);
            StartCoroutine(Think());
        }

        IEnumerator electricArrow()
        {
            animator.SetTrigger("Pattern_2");
            yield return new WaitForSeconds(1.0f);
            GameObject instantSpell3 = Instantiate(secondSpell, arrowSpawnPort.transform.position
                , arrowSpawnPort.transform.rotation);
            BossArrow bossSkill = instantSpell3.GetComponent<BossArrow>();
            yield return new WaitForSeconds(2.0f);
            StartCoroutine(Think());
        }
    }
}