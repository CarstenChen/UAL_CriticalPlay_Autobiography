using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } private set { } }

    public PlayerController playerP1;
    public PlayerController playerP2;

    public ArrangementManager arrangementManager;
    public MusicMachine musicMachine;
    public BalanceEventManager balanceEventManager;

    public bool dialogueArriveHomeStart;
    public bool dialogueArriveHomeFinished;
    public bool arrangementStart;
    public bool arrangementFinished;
    public bool layDownEventStart;
    public bool layDownEventFinished;
    public bool hasChangeToFullRoom;
    public bool balanceEventStart;
    public bool balanceEventFinished;

    public GameObject grabUI;

    public bool isInDialogue;
    private void Awake()
    {
        if (instance == null)
            instance = this;

        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        if (SceneLoader.isLoadingScene)
            return;

        if (!dialogueArriveHomeStart)
        {
            //First Dialogue
            dialogueArriveHomeStart = true;
            if (!isInDialogue)
            {
                DialogueManager.Instance.DisplayLine(0, 0);
                StartCoroutine(WaitDialogue(0));
            }

        }

        if (dialogueArriveHomeFinished && !arrangementStart)
        {
            arrangementStart = true;
            arrangementManager.enabled = true;
            grabUI.SetActive(true);
        }

        if (arrangementFinished && !layDownEventStart)
        {
            if (!isInDialogue)
            {
                DialogueManager.Instance.DisplayLine(2, 0);
                StartCoroutine(WaitDialogue(2));
            }

            musicMachine.enabled = true;
        }

        if (playerP1.inLayDownEvent && playerP2.inLayDownEvent && !layDownEventFinished)
        {
            if (!isInDialogue)
            {
                DialogueManager.Instance.DisplayLine(1, 0);
                StartCoroutine(WaitDialogue(1));
            }

        }

        if (layDownEventFinished && !hasChangeToFullRoom)
        {
            StartCoroutine(SceneLoader.instance.LoadScene("FullRoom", Color.white));
            hasChangeToFullRoom = true;
            StartCoroutine(StartBalanceEvent());
        }
    }

    IEnumerator WaitDialogue(int index)
    {
        isInDialogue = true;
        yield return new WaitUntil(() => DialogueManager.isPlayingLines == false);

        switch (index)
        {
            case 0:
                dialogueArriveHomeFinished = true;
                break;
            case 1:
                layDownEventFinished = true;
                break;
            case 2:
                layDownEventStart = true;
                break;
            case 3:
                balanceEventStart = true;
                balanceEventManager.enabled = true;
                break;
        }

        isInDialogue = false;

    }
    IEnumerator StartBalanceEvent()
    {
        yield return new WaitUntil(() => SceneLoader.isLoadingScene == false);
        

        if (!isInDialogue)
        {
            DialogueManager.Instance.DisplayLine(3, 0);
            StartCoroutine(WaitDialogue(3));
        }       
    }
}
