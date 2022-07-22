using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuestListViewController : MonoBehaviour
{
    [SerializeField]
    private ToggleGroup tabGroup;
    [SerializeField]
    private QuestListView activeQuestListView;
    [SerializeField]
    private QuestListView completedQuestListView;

    public IEnumerable<Toggle> Tabs => tabGroup.ActiveToggles();

    public void AddQuestToActiveListView(Quest quest, UnityAction<bool> onClicked)
        => activeQuestListView.AddElement(quest, onClicked);

    public void RemoveQuestFromActiveListView(Quest quest)
        => activeQuestListView.RemoveElement(quest);

    public void AddQuestToCompletedListView(Quest quest, UnityAction<bool> onClicked)
        => completedQuestListView.AddElement(quest, onClicked);
}
