using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Character;
using HOGUS.Scripts.Object.Item;
using HOGUS.Scripts.Data;

namespace HOGUS.Scripts.CustomSystem
{
    /// <summary>
    /// ĳ������ ���� ���� �ý���
    /// </summary>
    public class CombatSystem : MonoBehaviour
    {
        public GameObject attackCollider;

        // ���� ������ �����ϴ� Ÿ�̹��� �ִϸ��̼ǿ� �̺�Ʈ�� ���
        public void OnAttackCollision()
        {
            attackCollider.SetActive(true);
        }
    }
}