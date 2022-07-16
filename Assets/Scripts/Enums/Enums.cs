namespace HOGUS.Scripts.Enums
{
    #region ItemEnum
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
    #endregion
    #region Boss
    public enum BossState
    {
        Idle,
        Move,
        Attack,
        Damaged,
        Die,
        Skill,
        Skill1,
        Skill2
    }
    #endregion
}