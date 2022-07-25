using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Enums;
using HOGUS.Scripts.Data;
using HOGUS.Scripts.CustomSystem;
using HOGUS.Scripts.Character;

/// <summary>
/// 캐릭터들의 공격 히트박스
/// </summary>
public class AttackCollision : MonoBehaviour
{
    private Stat CharacterStat;
    private readonly MonsterBase monster;

    private void Start()
    {
        // 몬스터라면 Stat
        if(gameObject.CompareTag("Enemy"))
            CharacterStat = GetComponentInParent<MonsterBase>().GetCurrentStat();
        // 검사 후 몬스터가 아니라면 플레이어
        else if (gameObject.CompareTag("Player"))
            CharacterStat = GetComponentInParent<Player>().GetCurrentStatus();
    }

    void OnEnable()
    {
        StartCoroutine("coDisable");
    }

    private void OnTriggerEnter(Collider other)
    {
        // 피격 대상에게 데미지를 주는 시도
        if(other.CompareTag("Enemy"))
        {
            var damage = Random.Range(CharacterStat.MinDamage, CharacterStat.MaxDamage);
            Debug.Log(other.name + " " + damage);
            other.GetComponent<MonsterBase>().Damaged(damage);
        }
        else if (other.CompareTag("Player"))
        {
            var damage = Random.Range(CharacterStat.MinDamage, CharacterStat.MaxDamage);
            Debug.Log(other.name + " " + damage);
            other.GetComponent<Player>().Damaged(damage);
        }
    }

    private IEnumerator coDisable()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}
