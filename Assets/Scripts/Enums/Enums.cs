namespace HOGUS.Scripts.Enums
{
    public enum Test
    {
        TEST1,
        TEST2,
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