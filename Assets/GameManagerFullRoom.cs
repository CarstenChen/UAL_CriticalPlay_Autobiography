using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerFullRoom : MonoBehaviour
{
    private static GameManagerFullRoom instance;
    public static GameManagerFullRoom Instance { get { return instance; } private set { } }

    public BalanceEventManager balanceEventManager;

    public bool balanceEventStart;
    public bool balanceEventFinished;

    public bool isInDialogue;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }


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

        if (!balanceEventStart) 
        {
            if (!isInDialogue)
            {
                DialogueManager.Instance.DisplayLine(3, 0);
                StartCoroutine(WaitDialogue(3));
            }
        }
    }

    IEnumerator WaitDialogue(int index)
    {
        isInDialogue = true;
        yield return new WaitUntil(() => DialogueManager.isPlayingLines == false);

        switch (index)
        {
            case 0:
                //dialogueArriveHomeFinished = true;
                break;
            case 1:
                //layDownEventFinished = true;
                break;
            case 2:
                //layDownEventStart = true;
                break;
            case 3:
                balanceEventStart = true;
                balanceEventManager.enabled = true;
                break;
        }

        isInDialogue = false;

    }
}
