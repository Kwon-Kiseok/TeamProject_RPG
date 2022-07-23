using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    #region Save Path
    private const string kSaveRootPath = "questSystem";
    private const string kActiveQuestsSavePath = "activeQuests";
    private const string kCompletedQuestsSavePath = "completedQuests";
    private const string kActiveAchievementsSavePath = "activeAchievement";
    private const string kCompletedAchievementsSavePath = "completedAchievement";
    #endregion

    #region Events
    public delegate void QuestRegisteredHandler(Quest newQuest);
    public delegate void QuestCompletedHandler(Quest quest);
    public delegate void QuestCanceledHandler(Quest quest);
    #endregion

    private static QuestSystem instance;
    private static bool isApplicationQuitting;

    public static QuestSystem Instance
    {
        get
        {
            if (!isApplicationQuitting && instance == null)
            {
                instance = FindObjectOfType<QuestSystem>();
                if (instance == null)
                {
                    instance = new GameObject("Quest System").AddComponent<QuestSystem>();
                    DontDestroyOnLoad(instance.gameObject);
                }
            }
            return instance;
        }
    }

    private List<Quest> activeQuests = new List<Quest>();
    private List<Quest> completedQuests = new List<Quest>();

    private List<Quest> activeAchievements = new List<Quest>();
    private List<Quest> completedAchievements = new List<Quest>();

    private QuestDatabase questDatatabase;
    private QuestDatabase achievementDatabase;

    public event QuestRegisteredHandler onQuestRegistered;
    public event QuestCompletedHandler onQuestCompleted;
    public event QuestCanceledHandler onQuestCanceled;

    public event QuestRegisteredHandler onAchievementRegistered;
    public event QuestCompletedHandler onAchievementCompleted;

    public IReadOnlyList<Quest> ActiveQuests => activeQuests;
    public IReadOnlyList<Quest> CompletedQuests => completedQuests;
    public IReadOnlyList<Quest> ActiveAchievements => activeAchievements;
    public IReadOnlyList<Quest> CompletedAchievements => completedAchievements;

    private void Awake()
    {
        questDatatabase = Resources.Load<QuestDatabase>("QuestDatabase");
        achievementDatabase = Resources.Load<QuestDatabase>("AchievementDatabase");

        if (!Load())
        {
            foreach (var achievement in achievementDatabase.Quests)
            {
                Register(achievement);
            }
        }
    }

    private void OnApplicationQuit()
    {
        isApplicationQuitting = true;
        Save();
    }

    public Quest Register(Quest _quest)
    {
        var newQuest = _quest.Clone();
        if (newQuest is Achievement)
        {
            newQuest.onCompleted += OnAchievementCompleted;

            activeAchievements.Add(newQuest);

            newQuest.OnRegister();
            onAchievementRegistered?.Invoke(newQuest);
        }
        else
        {
            newQuest.onCompleted += OnQuestCompleted;
            newQuest.onCanceled += OnQuestCanceled;

            activeQuests.Add(newQuest);
            Debug.Log(activeQuests.Count);

            newQuest.OnRegister();
            onQuestRegistered?.Invoke(newQuest);
        }

        return newQuest;
    }

    public void ReceiveReport(string category, object target, int successCount)
    {
        ReceiveReport(activeQuests, category, target, successCount);
        ReceiveReport(activeAchievements, category, target, successCount);
    }

    public void ReceiveReport(Category category, TaskTarget target, int successCount)
        => ReceiveReport(category.CodeName, target.Value, successCount);

    private void ReceiveReport(List<Quest> quests, string category, object target, int successCount)
    {
        foreach (var quest in quests.ToArray())
            quest.ReceiveReport(category, target, successCount);
    }

    public void CompleteWaitingQuests()
    {
        foreach (var quest in activeQuests.ToList())
        {
            if (quest.IsComplatable)
                quest.Complete();
        }
    }

    public bool ContainsInActiveQuests(Quest quest) => activeQuests.Any(x => x.CodeName == quest.CodeName);

    public bool ContainsInCompleteQuests(Quest quest) => completedQuests.Any(x => x.CodeName == quest.CodeName);

    public bool ContainsInActiveAchievements(Quest quest) => activeAchievements.Any(x => x.CodeName == quest.CodeName);

    public bool ContainsInCompletedAchievements(Quest quest) => completedAchievements.Any(x => x.CodeName == quest.CodeName);

    public void Save()
    {
        var root = new JObject();
        root.Add(kActiveQuestsSavePath, CreateSaveDatas(activeQuests));
        root.Add(kCompletedQuestsSavePath, CreateSaveDatas(completedQuests));
        root.Add(kActiveAchievementsSavePath, CreateSaveDatas(activeAchievements));
        root.Add(kCompletedAchievementsSavePath, CreateSaveDatas(completedAchievements));

        PlayerPrefs.SetString(kSaveRootPath, root.ToString());
        PlayerPrefs.Save();
        Debug.Log("Save");
    }

    public bool Load()
    {
        if (PlayerPrefs.HasKey(kSaveRootPath))
        {
            var root = JObject.Parse(PlayerPrefs.GetString(kSaveRootPath));

            LoadSaveDatas(root[kActiveQuestsSavePath], questDatatabase, LoadActiveQuest);
            LoadSaveDatas(root[kCompletedQuestsSavePath], questDatatabase, LoadCompletedQuest);

            LoadSaveDatas(root[kActiveAchievementsSavePath], achievementDatabase, LoadActiveQuest);
            LoadSaveDatas(root[kCompletedAchievementsSavePath], achievementDatabase, LoadCompletedQuest);

            return true;
        }
        else
        {
            return false;
        }
    }

    private JArray CreateSaveDatas(IReadOnlyList<Quest> quests)
    {
        var saveDatas = new JArray();
        foreach (var quest in quests)
        {
            if (quest.IsSavable)
                saveDatas.Add(JObject.FromObject(quest.ToSaveData()));
        }
        return saveDatas;
    }

    private void LoadSaveDatas(JToken datasToken, QuestDatabase database, System.Action<QuestSaveData, Quest> onSuccess)
    {
        var datas = datasToken as JArray;
        foreach (var data in datas)
        {
            var saveData = data.ToObject<QuestSaveData>();
            var quest = database.FindQuestBy(saveData.codeName);
            onSuccess.Invoke(saveData, quest);
        }
    }
    
    private void LoadActiveQuest(QuestSaveData saveData, Quest quest)
    {
        var newQuest = Register(quest);
        newQuest.LoadFrom(saveData);
    }

    private void LoadCompletedQuest(QuestSaveData saveData, Quest quest)
    {
        var newQuest = quest.Clone();
        newQuest.LoadFrom(saveData);

        if (newQuest is Achievement)
            completedAchievements.Add(newQuest);
        else
            completedQuests.Add(newQuest);
    }

    #region Callback
    private void OnQuestCompleted(Quest quest)
    {
        activeQuests.Remove(quest);
        completedQuests.Add(quest);

        onQuestCompleted?.Invoke(quest);
    }

    private void OnQuestCanceled(Quest quest)
    {
        activeQuests.Remove(quest);
        onQuestCanceled?.Invoke(quest);

        Destroy(quest, Time.deltaTime);
    }

    private void OnAchievementCompleted(Quest achievement)
    {
        activeAchievements.Remove(achievement);
        completedAchievements.Add(achievement);

        onAchievementCompleted?.Invoke(achievement);
    }
    #endregion
}
