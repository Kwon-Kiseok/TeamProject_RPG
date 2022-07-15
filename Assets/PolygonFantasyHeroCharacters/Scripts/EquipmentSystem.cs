using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

using HOGUS.Scripts.Enums;
using HOGUS.Scripts.Object.Item;

namespace HOGUS.Scripts.CustomSystem
{
    /// <summary>
    /// 캐릭터의 장비 아이템의 장착, 해제를 담당해줄 시스템
    /// </summary>
    public class EquipmentSystem : MonoBehaviour
    {
        public Tester tester;
        private GameObject weaponGO;

        // 장비 아이템 장착 상태를 나타내는 딕셔너리
        public Dictionary<EquipPart, EquipmentItem> dictEquipmets = new();

        /// <summary>
        /// 방어구 아이템 장착 함수
        /// </summary>
        public void DoEquip(EquipPart part, ArmorItem armorItem)
        {
            // 현재 해당 부위를 장착하고 있다면 장착 해제 후 교환
            if (dictEquipmets.ContainsKey(part))
            {
                dictEquipmets.Remove(part);
            }
            else
            {
                tester.defense += armorItem.defense;
            }
            dictEquipmets.Add(part, armorItem);

        }

        /// <summary>
        /// 무기 아이템 장착 함수
        /// </summary>
        public void DoEquip(EquipPart part, WeaponItem weaponItem)
        {
            if (dictEquipmets.ContainsKey(part))
            {
                dictEquipmets.Remove(part);
                Destroy(weaponGO);
            }
            else
            {
                tester.minDamage += weaponItem.minDamage;
                tester.maxDamage += weaponItem.maxDamage;
                tester.attackSpeed += weaponItem.attackSpeed;
            }
            dictEquipmets.Add(part, weaponItem);

            if (part == EquipPart.WEAPON)
            {
                WeaponItem weaponPart = (WeaponItem)dictEquipmets[part];
                AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(weaponPart.refAddress);
                handle.Completed += AsyncOperationHandle_Complete;
            }
        }

        /// <summary>
        /// 입력된 파트의 장비 해제
        /// </summary>
        public void DoUnequip(EquipPart part)
        {
            if(dictEquipmets.ContainsKey(part))
            {
                dictEquipmets.Remove(part);
            }

            if(part == EquipPart.WEAPON)
            {
                if(weaponGO != null)
                {
                    Destroy(weaponGO);
                }
            }
        }

        private void AsyncOperationHandle_Complete(AsyncOperationHandle<GameObject> handle)
        {
            if(handle.Status == AsyncOperationStatus.Succeeded)
            {
                weaponGO = Instantiate(handle.Result, tester.weaponEquipPos);
            }
        }
    }
}
