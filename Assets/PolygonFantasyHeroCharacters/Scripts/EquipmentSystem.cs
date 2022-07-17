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
        public Player player;
        private GameObject weaponGO;

        public WeaponItem equipWeapon;
        public ShieldItem equipShield;
        // 장비 아이템 장착 상태를 나타내는 딕셔너리
        public Dictionary<EquipPart, ArmorItem> dictEquipmets = new();

        /// <summary>
        /// 방어구 아이템 장착 함수
        /// </summary>
        public void DoEquip(EquipPart part, ArmorItem armorItem)
        {
            // 현재 해당 부위를 장착하고 있다면 장착 해제 후 교환
            if (dictEquipmets.ContainsKey(part))
            {
                DoUnequip(part);
            }
            else
            {
                //player.equipedDefense += armorItem.defense;
            }
            dictEquipmets.Add(part, armorItem);
        }

        /// <summary>
        /// 무기 아이템 장착 함수
        /// </summary>
        public void DoEquip(EquipPart part, WeaponItem weaponItem)
        {
            if (equipWeapon != null)
            {
                DoUnequip(part);
            }
            
            equipWeapon = weaponItem;

            if (part == EquipPart.WEAPON)
            {
                WeaponItem weaponPart = equipWeapon;
                AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(weaponPart.refAddress);
                handle.Completed += AsyncOperationHandle_Complete;
            }
        }

        /// <summary>
        /// 입력된 파트의 장비 해제
        /// </summary>
        public void DoUnequip(EquipPart part)
        {
            if(part == EquipPart.WEAPON)
            {
                // 인벤토리 구현 후 장착 무기 해제시 
                // 정보 저장해서 인벤토리에 넣기
                equipWeapon = null;
                if(weaponGO != null)
                {
                    Destroy(weaponGO);
                }
            }
            else
            {
                //player.equipedDefense -= dictEquipmets[part].defense;
                dictEquipmets.Remove(part);
            }

        }

        private void AsyncOperationHandle_Complete(AsyncOperationHandle<GameObject> handle)
        {
            if(handle.Status == AsyncOperationStatus.Succeeded)
            {
                weaponGO = Instantiate(handle.Result, player.weaponEquipPos);
            }
        }
    }
}
