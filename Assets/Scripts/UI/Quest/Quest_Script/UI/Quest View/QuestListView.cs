using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class QuestListView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI elementTextPrefab;

    private Dictionary<Quest, GameObject> elementsByQuest = new Dictionary<Quest, GameObject>();
    private ToggleGroup toggleGroup;

    private void Awake()
    {
        toggleGroup = GetComponent<ToggleGroup>();
    }

    public void AddElement(Quest quest, UnityAction<bool> onClicked)
    {
        var element = Instantiate(elementTextPrefab, transform);
        element.text = quest.DisplayName;

        var toggle = element.GetComponent<Toggle>();
        toggle.group = toggleGroup;
        toggle.onValueChanged.AddListener(onClicked);

        elementsByQuest.Add(quest, element.gameObject);
    }

    public void RemoveElement(Quest quest)
    {
        Destroy(elementsByQuest[quest]);
        elementsByQuest.Remove(quest);
    }
}
