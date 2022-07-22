using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class QuestView : MonoBehaviour
//{
//    [SerializeField]
//    private QuestListViewController questListViewController;
//    [SerializeField]
//    private QuestDetailView questDetailView;

//    private void Start()
//    {
//        var questSystem = QuestSystem.Instance;

//        foreach (var quest in questSystem.ActiveQuests)
//            AddQuestToActiveListView(quest);

//        foreach (var quest in questSystem.CompletedQuests)
//            AddQuestToCompletedListView(quest);

//        questSystem.onQuestRegistered += AddQuestToActiveListView;
//        questSystem.onQuestCompleted += RemoveQuestFromActiveListView;
//        questSystem.onQuestCompleted += AddQuestToCompletedListView;
//        questSystem.onQuestCompleted += HideDetailIfQuestCanceled;
//        questSystem.onQuestCanceled += HideDetailIfQuestCanceled;
//        questSystem.onQuestCanceled += RemoveQuestFromActiveListView;

//        foreach (var tab in questListViewController.Tabs)
//            tab.onValueChanged.AddListener(HideDetail);

//        gameObject.SetActive(false);
//    }

//    private void OnDestroy()
//    {
//        var questSystem = QuestSystem.Instance;
//        if (questSystem)
//        {
//            questSystem.onQuestRegistered -= AddQuestToActiveListView;
//            questSystem.onQuestCompleted -= RemoveQuestFromActiveListView;
//            questSystem.onQuestCompleted -= AddQuestToCompletedListView;
//            questSystem.onQuestCompleted -= HideDetailIfQuestCanceled;
//            questSystem.onQuestCanceled -= HideDetailIfQuestCanceled;
//            questSystem.onQuestCanceled -= RemoveQuestFromActiveListView;
//        }
//    }

//    private void OnEnable()
//    {
//        if (questDetailView.Target != null)
//            questDetailView.Show(questDetailView.Target);
//    }

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Escape))
//            gameObject.SetActive(false);
//    }

//    private void ShowDetail(bool isOn, Quest quest)
//    {
//        if (isOn)
//            questDetailView.Show(quest);
//    }

//    private void HideDetail(bool isOn)
//    {
//        questDetailView.Hide();
//    }

//    private void AddQuestToActiveListView(Quest quest)
//        => questListViewController.AddQuestToActiveListView(quest, isOn => ShowDetail(isOn, quest));

//    private void AddQuestToCompletedListView(Quest quest)
//        => questListViewController.AddQuestToCompletedListView(quest, isOn => ShowDetail(isOn, quest));

//    private void HideDetailIfQuestCanceled(Quest quest)
//    {
//        if (questDetailView.Target == quest)
//            questDetailView.Hide();
//    }

//    private void RemoveQuestFromActiveListView(Quest quest)
//    {
//        questListViewController.RemoveQuestFromActiveListView(quest);
//        if (questDetailView.Target == quest)
//            questDetailView.Hide();
//    }
//}
