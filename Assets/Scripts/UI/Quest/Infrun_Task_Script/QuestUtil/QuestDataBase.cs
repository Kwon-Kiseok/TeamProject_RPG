using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Quest/QuestDataBase")]
public class QuestDataBase : ScriptableObject
{
    [SerializeField]
    private List<Quest> quests;

    public IReadOnlyList<Quest> Quests => quests;

    // codeName을 통해서 퀘스트 찾아오기
    public Quest FindQuestBy(string codeName) => quests.FirstOrDefault(x => x.CodeName == codeName);


#if UNITY_EDITOR

    [ContextMenu("FindQuests")]
    private void FindQuest()
    {
        FindQuestBy<Quest>();
    }

    [ContextMenu("FindAchievements")]
    private void FindAchievement()
    {
        FindQuestBy<Achievement>();
    }

    // 퀘스트를 찾아오는 Util
    // 제네릭 함수인 이유는 같은 코드로 퀘스트와 업적을 따로 찾기 위해서
    private void FindQuestBy<T>() where T : Quest
    {
        quests = new List<Quest>();

        // FindAssets는 Assets폴더에서 Filter에 맞는 에셋의 GUID를 가져오는 함수
        // GUID는 유니티가 에셋을 관리하기 위해 내부적으로 사용하는 ID
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
        foreach(var guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var quest = AssetDatabase.LoadAssetAtPath<T>(assetPath);

            // GetType해주는 이유는 만약에 T가 퀘스트 클래스라면 Achievement클래스도 퀘스트 클래스를 상속 받고 있기 때문에
            // 다 찾아올 수가 있어서 다시 한번 검사해준다
            if (quest.GetType() == typeof(T))
            {
                quests.Add(quest);
            }

            // SetDirty는 QuestDataBase객체가 가진 Serialize변수가 변화가 있으니 반영하라는 함수
            EditorUtility.SetDirty(this);

            // 에셋 저장
            AssetDatabase.SaveAssets();
        }
    }
#endif
}
