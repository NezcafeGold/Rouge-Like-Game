using System;
using LitJson;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestHandler : MonoBehaviour
{
    private QuestData currentQuest;
    private Quest quest;
    private GameObject questGO;
    private GameObject titleGO;
    private GameObject[] answersGO;

    private void Awake()
    {
        Messenger.AddListener<QuestData>(GameEvent.HANDLE_QUEST, HandleQuest);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener<QuestData>(GameEvent.HANDLE_QUEST, HandleQuest);
    }

    private void Start()
    {
        Transform quest = transform.Find("Quest");
        questGO = quest.Find("QuestForm").Find("QuestText").gameObject;
        titleGO = quest.Find("QuestForm").Find("Answer").gameObject;

        Transform answersT = quest.Find("AnswersForm");
        answersGO = new GameObject[3];
        int i = 0;
        foreach (Transform answ in answersT)
        {
            answersGO[i] = answ.gameObject;
            answ.gameObject.SetActive(false);
            i++;
        }

        gameObject.SetActive(false);
    }

    private void HandleQuest(QuestData quest)
    {
        currentQuest = quest;
        LoadJsonQuest();
        LoadQuestForm();
        gameObject.SetActive(true);
    }

    private void LoadJsonQuest()
    {
        string path = currentQuest.PathToJSON;
        string[] pathA = path.Split('\\');
        TextAsset jsonTextFile =
            Resources.Load<TextAsset>(pathA[0] + "\\" + LocalizationManager.CurrentLanguage + "\\" + pathA[1]);
        quest = JsonMapper.ToObject<Quest>(jsonTextFile.text);
        Debug.Log("Quest Loaded!");
    }

    private void LoadQuestForm()
    {
        if (quest.currentQuestPart == null)
        {
            foreach (QuestPart qp in quest.questParts)
            {
                if (qp.type == "Quest")
                {
                    LoadQuestForm(qp);
                    break;
                }
            }
        }
    }

    private void LoadQuestForm(QuestPart qp)
    {
        InactiveAllAnswers();
        quest.currentQuestPart = qp;
        titleGO.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = qp.title;
        questGO.GetComponent<TextMeshProUGUI>().text = qp.text;
        if (qp.next == null && qp.choices != null)
        {
            for (int i = 0; i < qp.choices.Length; i++)
            {
                answersGO[i].SetActive(true);
                answersGO[i].transform.Find("Text").GetComponent<TextMeshProUGUI>().text =
                    FindQuestPartById(qp.choices[i]).title;
            }
        }
        else
        {
            answersGO[0].SetActive(true);
            answersGO[0].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = qp.next;
        }

        if (qp.choices == null)
        {
            answersGO[0].SetActive(true);
            answersGO[0].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Выход";
        }
    }

    private void InactiveAllAnswers()
    {
        for (int i = 0; i < 3; i++)
        {
            answersGO[i].SetActive(false);
        }
    }

    private QuestPart FindQuestPartById(string id)
    {
        for (int i = 0; i < quest.questParts.Length; i++)
        {
            if (quest.questParts[i].id == id)
            {
                return quest.questParts[i];
            }
        }

        return null;
    }

    public void LoadAnswerByIdArray(int id)
    {
        if (quest.currentQuestPart.choices == null)
        {
            CloseQuest();
            return;
        }

        string idQP = quest.currentQuestPart.choices[id];
        QuestPart questPart = FindQuestPartById(idQP);
        if (questPart.type == "Check")
        {
            LoadCheckAnswer(questPart);
        }
        else
        {
            InactiveAllAnswers();
            LoadQuestForm(questPart);
        }
    }

    private void CloseQuest()
    {
        gameObject.SetActive(false);
    }

    private void LoadCheckAnswer(QuestPart questPart)
    {
        string idBranch = "";
        if (questPart.variable.StartsWith("random"))
        {
            string[] qpsplt = questPart.variable.Split('_');
            int value = Int16.Parse(qpsplt[1]);
            int randomV = Random.Range(0, 100);
            if (randomV >= value)
            {
                idBranch = questPart.branches[0];
            }
            else
            {
                idBranch = questPart.branches[1];
            }

            LoadQuestForm(FindQuestPartById(idBranch));
        }
    }

    public class Quest
    {
        public QuestPart[] questParts;
        public QuestPart currentQuestPart;
    }

    public class QuestPart
    {
        public string type;
        public string id;
        public string title;
        public string text;
        public string[] choices;
        public string variable; //var_int // если больше инт - true branches[0]
        public string[] branches;
        public string next;
        public string[] reward;
    }
}