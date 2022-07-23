using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementView : MonoBehaviour
{
    [SerializeField]
    private RectTransform achievementGroup;
    [SerializeField]
    private AchievementDetailView achievementDetailViewPrefab;

    private void Start()
    {
        var questSystem = QuestSystem.Instance;
        CreateDetailViews(questSystem.ActiveAchievements);
        CreateDetailViews(questSystem.CompletedAchievements);

        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            gameObject.SetActive(false);
    }

    private void CreateDetailViews(IReadOnlyList<Quest> achievements)
    {
        foreach (var achievement in achievements)
            Instantiate(achievementDetailViewPrefab, achievementGroup).Setup(achievement);
    }
}
