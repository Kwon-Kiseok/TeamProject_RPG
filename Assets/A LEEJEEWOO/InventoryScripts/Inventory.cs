using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.DP;
using HOGUS.Scripts.Object.Item;

using TMPro;
using UnityEngine.UI;

namespace HOGUS.Scripts.Inventory
{
    public class Inventory : MonoSingleton<Inventory>
    {
        public GameObject itemstatus;
        private BaseItem _baseItem;
        
        [SerializeField] Image image; //
        [SerializeField] TextMeshProUGUI _name; //
        [SerializeField] TextMeshProUGUI _Rarity;//
        [SerializeField] TextMeshProUGUI _level; //
        [SerializeField] TextMeshProUGUI _Quality;//
        [SerializeField] TextMeshProUGUI _arm_wea_shi_Type;//        
        [SerializeField] TextMeshProUGUI _Defend; //
        [SerializeField] TextMeshProUGUI _Durability; //
        [SerializeField] TextMeshProUGUI _contens; //

        [SerializeField] TextMeshProUGUI _minpower;
        [SerializeField] TextMeshProUGUI _maxpower;

        public List<Image> _images = new List<Image>();
        private List<Sprite> _originImages = new List<Sprite>();

        public Image _Wimage;
        private Sprite _originWimage;
        public Image _Simage;
        private Sprite _originSimage;

        public void Wear()
        {
            if (_baseItem is ArmorItem)
            {
                var arm = (ArmorItem)_baseItem;
                _images[(int)arm.type].sprite = _baseItem.sprite;                
            }

            if (_baseItem is WeaponItem)
            {
                var wea = (WeaponItem)_baseItem;
                _Wimage.sprite = _baseItem.sprite;                
            }

            if (_baseItem is ShieldItem)
            {
                var shi = (ShieldItem)_baseItem;
                _Simage.sprite = _baseItem.sprite;
            }
        }

        public void TakeOff()
        {
            if (_baseItem is ArmorItem)
            {
                var arm = (ArmorItem)_baseItem;
                _images[(int)arm.type].sprite = _originImages[(int)arm.type];
            }

            if (_baseItem is WeaponItem)
            {
                var wea = (WeaponItem)_baseItem;
                _Wimage.sprite = _originWimage;
            }

            if (_baseItem is ShieldItem)
            {
                var shi = (ShieldItem)_baseItem;
                _Simage.sprite = _originSimage;
            }
        }
        
        public BaseItem BaseItem
        {
            get { return _baseItem; }
            set
            {
                _baseItem = value;
                if (_baseItem != null /*&& _baseItem is EquipmentItem && _baseItem is ArmorItem*/)
                {                    
                    image.sprite = _baseItem.sprite;
                    _name.text = _baseItem.itemName;
                    _contens.text = _baseItem.itemDescription;
                    _Rarity.text = $"희귀도 : {_baseItem.rarity}";//_baseItem.rarity.ToString();
                    _Quality.text = $"종류 : {_baseItem.quality}";//_baseItem.quality.ToString();

                    image.color = new Color(1, 1, 1, 1);
                    
                    if (_baseItem is EquipmentItem)
                    {
                        var equipment = (EquipmentItem) _baseItem;
                        
                        _level.text = $"레벨 : {equipment.requireLevel}";//equipment.requireLevel.ToString();
                        _Durability.text = $"내구도 : {equipment.durability}";//equipment.durability.ToString();
                    }
                    if (_baseItem is ArmorItem)
                    {
                        var arm = (ArmorItem)_baseItem;
                        
                        _Defend.text = $"방어력 : {arm.defense}";//arm.defense.ToString();
                        _arm_wea_shi_Type.text = $"타입 : {arm.type}";//arm.type.ToString();
                    }
                    if (_baseItem is WeaponItem)
                    {
                        var wea = (WeaponItem)_baseItem;

                        _arm_wea_shi_Type.text = $"타입 : {wea.type}";//wea.type.ToString();
                        _Defend.text = $"공격력 : {wea.minDamage}";//wea.minDamage.ToString();
                        //_maxpower.text = $"최대공격력 : {wea.maxDamage}";//wea.maxDamage.ToString();
                    }
                    if (_baseItem is ShieldItem)
                    {
                        var shi = (ShieldItem)_baseItem;

                        _arm_wea_shi_Type.text = $"타입 : {shi.type}";
                        _Defend.text = $"방어력 : {shi.defense}";

                    }

                }
                else
                {
                    image.color = new Color(1, 1, 1, 0);
                }
            }
        }


        /// <summary>
        /// 인벤토리 칸 내부 아이템
        /// </summary>
        public List<BaseItem> baseitems;
        [SerializeField]
        private Transform slotParent;
        [SerializeField]
        private Slot[] slots;

        private void OnValidate()
        {
            slots = slotParent.GetComponentsInChildren<Slot>();
        }
        private void Start()
        {            
            FreshSlot();
        }

        public void FreshSlot()
        {
            int i = 0;
            for (; i < baseitems.Count && i < slots.Length; i++)
            {
                slots[i].BaseItem = baseitems[i];
            }
            for (; i < slots.Length; i++)
            {
                slots[i].BaseItem = null;
            }

            foreach(var image in _images)
            {
                _originImages.Add(image.sprite);
            }
            _originWimage = _Wimage.sprite;
            _originSimage = _Simage.sprite;
        }

        public void AddItem(BaseItem _baseItem)
        {
            if (baseitems.Count < slots.Length)
            {
                baseitems.Add(_baseItem);
                FreshSlot();
            }
            else
            {
                print("슬롯이 가득 찼습니다.");
            }
        }
    }
}

