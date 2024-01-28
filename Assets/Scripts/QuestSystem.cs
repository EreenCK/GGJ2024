using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class Quest
{
    public string questID;
    public string title;
    public string description;
    public bool isCompleted;
}

public class QuestSystem : MonoBehaviour
{
    public TMP_Text questLogText;
    public List<Quest> quests = new List<Quest>(); // Public list for Unity Inspector
    private List<Quest> activeQuests = new List<Quest>(); // Private list for internal management

    void Start()
    {
        // Initially, update the quest log to display only accepted quests
        UpdateQuestLog();
    }

    void UpdateQuestLog()
    {
        // Update the quest log UI with current accepted quests
        string logText = "Quest Log:\n";

        foreach (var quest in activeQuests)
        {
            logText += $"{quest.title} - Status: {(quest.isCompleted ? "Completed" : "Incomplete")}\n";
        }

        questLogText.text = logText;
    }

    public void AcceptQuest(string questID)
    {
        // Find the quest by ID in the public list
        Quest quest = quests.Find(q => q.questID == questID);

        if (quest != null && !quest.isCompleted)
        {
            // Move the quest from the public list to the private list
            quests.Remove(quest);
            activeQuests.Add(quest);

            quest.isCompleted = false; // Reset completion status if needed
            UpdateQuestLog();
        }
    }

    public void CompleteQuest(string questID)
    {
        // Find the quest by ID in the private list
        Quest quest = activeQuests.Find(q => q.questID == questID);

        if (quest != null && !quest.isCompleted)
        {
            quest.isCompleted = true;

            // Provide additional rewards if needed

            UpdateQuestLog();
        }
    }
}