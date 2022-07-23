using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestDetailView : MonoBehaviour
{
    [SerializeField]
    private GameObject displayGroup;
    [SerializeField]
    private Button cancelButton;

    [Header("Quest Description")]
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI description;

    [Header("Task Description")]
    [SerializeField]
    private RectTransform taskDescriptorGroup;
    [SerializeField]
    private TaskDescriptor taskDescriptorPrefab;
    [SerializeField]
    private int taskDescriptorPoolCount;

    [Header("Reward Description")]
    [SerializeField]
    private RectTransform rewardDescriptionGroup;
    [SerializeField]
    private TextMeshProUGUI rewardDescriptionPrefab;
    [SerializeField]
    private int rewardDescriptionPoolCount;

    private List<TaskDescriptor> taskDescriptorPool;
    private List<TextMeshProUGUI> rewardDescriptionPool;

    public Quest Target { get; private set; }

    private void Awake()
    {
        taskDescriptorPool = CreatePool(taskDescriptorPrefab, taskDescriptorPoolCount, taskDescriptorGroup);
        rewardDescriptionPool = CreatePool(rewardDescriptionPrefab, rewardDescriptionPoolCount, rewardDescriptionGroup);
        displayGroup.SetActive(false);
    }

    private void Start()
    {
        cancelButton.onClick.AddListener(CancelQuest);
    }

    private List<T> CreatePool<T>(T prefab, int count, RectTransform parent)
        where T : MonoBehaviour
    {
        var pool = new List<T>(count);
        for (int i = 0; i < count; i++)
            pool.Add(Instantiate(prefab, parent));
        return pool;
    }

    private void CancelQuest()
    {
        if (Target.IsCancelable)
            Target.Cancel();
    }

    public void Show(Quest quest)
    {
        displayGroup.SetActive(true);
        Target = quest;

        title.text = quest.DisplayName;
        description.text = quest.Description;

        int taskIndex = 0;
        foreach (var taskGroup in quest.TaskGroups)
        {
            foreach (var task in taskGroup.Tasks)
            {
                var poolObject = taskDescriptorPool[taskIndex++];
                poolObject.gameObject.SetActive(true);

                if (taskGroup.IsComplete)
                    poolObject.UpdateTextUsingStrikeThrough(task);
                else if (taskGroup == quest.CurrentTaskGroup)
                    poolObject.UpdateText(task);
                else
                    poolObject.UpdateText("¡Ü ??????????");
            }
        }

        for (int i = taskIndex; i < taskDescriptorPool.Count; i++)
            taskDescriptorPool[i].gameObject.SetActive(false);

        var rewards = quest.Rewards;
        var rewardCount = rewards.Count;
        for (int i = 0; i < rewardDescriptionPoolCount; i++)
        {
            var poolObject = rewardDescriptionPool[i];
            if (i < rewardCount)
            {
                var reward = rewards[i];
                poolObject.text = $"¡Ü {reward.Description} +{reward.Quantity}";
                poolObject.gameObject.SetActive(true);
            }
            else
                poolObject.gameObject.SetActive(false);
        }

        cancelButton.gameObject.SetActive(quest.IsCancelable && !quest.IsComplete);
    }

    public void Hide()
    {
        Target = null;
        displayGroup.SetActive(false);
        cancelButton.gameObject.SetActive(false);
    }
}
