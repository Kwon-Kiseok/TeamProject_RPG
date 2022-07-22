using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Data;
using HOGUS.Scripts.CustomSystem;
using HOGUS.Scripts.Character;

/// <summary>
/// ĳ���͵��� ���� ��Ʈ�ڽ�
/// </summary>
public class AttackCollision : MonoBehaviour
{
    private Stat CharacterStat;

    private void Start()
    {
        // ���Ͷ�� Stat
        if(gameObject.CompareTag("Enemy"))
            CharacterStat = GetComponentInParent<MonsterBase>().GetCurrentStat();
        // �˻� �� ���Ͱ� �ƴ϶�� �÷��̾�
        else if (gameObject.CompareTag("Player"))
            CharacterStat = GetComponentInParent<Player>().GetCurrentStatus();
    }

    void OnEnable()
    {
        StartCoroutine("coDisable");
    }

    private void OnTriggerEnter(Collider other)
    {
        // �ǰ� ��󿡰� �������� �ִ� �õ�
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
