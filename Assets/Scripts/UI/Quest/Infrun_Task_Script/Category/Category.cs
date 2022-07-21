using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Category/Category", fileName = "Category_")]
public class Category : ScriptableObject, IEquatable<Category>
{
    [SerializeField]
    private string codeName;
    [SerializeField]
    private string displayName;

    public string CodeName => codeName;
    public string DisplayName => displayName;


    #region Opetator
    // 카테고리는 주로 codeName으로 비교를 한다.
    public bool Equals(Category other)
    {
        // other이 null이 아니고 other과 오브젝트의 인스턴스가 같고 타입도 같고 이름도 같다면 true

        if (other is null)
        {
            return false;
        }
        if(ReferenceEquals(other, this))    // ReferenceEquals(other, this) 2오브젝트 인스턴스가 같은지 검사
        {
            return true;
        }
        if(GetType() != other.GetType())    // 인스턴스의 타입을 가져온다.
        {
            return false;
        }
        return codeName == other.codeName;
    }

    public override int GetHashCode() => (codeName, DisplayName).GetHashCode();

    public override bool Equals(object other) => base.Equals(other);

    public static bool operator == (Category lhs, string rhs)
    {
        if(lhs is null)
        {
            return ReferenceEquals(rhs, null);
        }
        else
        {
            return lhs.codeName == rhs || lhs.DisplayName == rhs;
        }
    }

    public static bool operator !=(Category lhs, string rhs) => !(lhs == rhs);
    // category.CodeName == "Kill" x
    // category == "Kill"
    #endregion
}
