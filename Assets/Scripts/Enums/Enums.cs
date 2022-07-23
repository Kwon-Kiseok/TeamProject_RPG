namespace HOGUS.Scripts.Enums
{
    #region ItemEnum
    /// <summary>
    /// 아이템 종류
    /// </summary>
    public enum ItemType
    {
        Equipment,
        Consumables,
        Etc,
        // 0~99 소모 아이템
        // 100~199 무기 아이템
        // 200~299 방어구 아이템
        Cons = 0,
        weapon = 100,
        armor = 200,
        None = 300
    }
    /// <summary>
    /// 아이템 희귀도등급
    /// </summary>
    public enum ItemRarity
    {
        COMMON,     // 일반 (흰)
        MAGIC,      // 매직 (파랑)
        RARE,       // 희귀 (노랑)
        UNIQUE,     // 고유 (물빠진 노랑)
        SET,        // 세트 (초록)
        CRAFT,      // 제작 (주황)
        RUNEWORDS   // 룬워드 (물빠진 노랑)
    }
    /// <summary>
    /// 아이템 품질등급
    /// </summary>
    public enum ItemQuality
    {
        NORMAL,         // 노말
        EXCEPTIONAL,    // 익셉셔널
        ELITE           // 엘리트
    }
    /// <summary>
    /// 무기 종류
    /// </summary>
    public enum WeaponType
    {
        SWORD,
        AXE,
        STAFF
    }
    /// <summary>
    /// 방어구 종류
    /// </summary>
    public enum ArmorType
    {
        HELM,
        ARMOR,
        GLOVE,
        BELT,
        BOOTS
    }
    #endregion

    /// <summary>
    /// 방패종류
    /// </summary>
    
    public enum ShieldType
    {
        Shield
    }

    // ...
    public enum TestEventEnum
    {
        TEST1,
        TEST2,
    }
#region Player
    public enum PlayerState
    {
        Idle,
        Move,
        Attack,
        Damaged,
        Die
    }

    public enum EquipPart
    {
        HELM,
        ARMOR,
        GLOVE,
        BELT,
        BOOTS,
        WEAPON,
        SHIELD
    }
    #endregion
    #region Enemy
    public enum EnemyState
    {
        Idle,
        Move,
        Chase,
        Attack,
        Damaged,
        Die
    }
    public enum Status
    {
        Idle,
        Trace,
        Attack,
        GameOver,
    }

    public enum EnemyType
    {
        PunchMonster,
        SwordMonster,
        MagicMonster,
        WarChief,
    }

    #endregion


    #region System
    public enum AttackType
    {
        MELEE,
        RANGE
    }
#endregion
}