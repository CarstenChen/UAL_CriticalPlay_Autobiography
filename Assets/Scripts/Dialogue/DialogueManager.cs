using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    public static DialogueManager Instance { get { return instance; } private set { } }

    public GameObject[] lineUIs;

    //public Color LerpColorA;
    //public Color LerpColorB;

    [SerializeField] public static bool isPlayingLines;
    //[SerializeField] public static bool isReadingStartPlot;

    public TextMeshProUGUI[] textMeshPros;
    //protected Animator textAnimator;
    protected AudioSource audioSource;
    protected PlotManager plotManager;
    protected Dialogue[] allDialogues;
    protected Dialogue currentLine;

    public bool blockInput;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        isPlayingLines = false;
        //isReadingStartPlot = false;

        audioSource = GetComponent<AudioSource>();
        //textMeshPros[0] = lineUIs[0].GetComponentInChildren<TextMeshProUGUI>();
        //textMeshPros[1] = lineUIs[1].GetComponentInChildren<TextMeshProUGUI>();
        //textAnimator = lineUI.GetComponent<Animator>();

        plotManager = Resources.Load<PlotManager>("DataAssets/Dialogues");
        allDialogues = plotManager.dialogues;
    }

    public void DisplayLine(int plotID, int index)
    {
        if (isPlayingLines && index == 0) return;

        if (index == 0)
        {
            StopAllCoroutines();
            isPlayingLines = true;

            
            //if (plotID == 0)
            //{
            //    isReadingStartPlot = true;
            //}
            //else
            //{
            //    isReadingStartPlot = false;
            //}
        }

        for (int i = 0; i < allDialogues.Length; i++)
        {
            if (allDialogues[i].plotID == plotID && allDialogues[i].index == index)
            {
                currentLine = allDialogues[i];

                textMeshPros[currentLine.playerID].text = currentLine.text;

                if (currentLine.blockInput) blockInput = true;

                StartCoroutine(SetLineUI(true, 0f, currentLine.playerID));
                StartCoroutine(WaitSoundEndToNextLine(currentLine));
            }
        }
    }
    IEnumerator WaitSoundEndToNextLine(Dialogue dialogue)
    {

        audioSource.clip = dialogue.audio;
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);

        if (dialogue.nextIndex != -1)
        {
            DisplayLine(dialogue.plotID, dialogue.nextIndex);
        }
        else
        {
            //if (textAnimator != null)
            //{
            //    textAnimator.SetBool("FadeIn", false);
            //    textAnimator.SetBool("FadeOut", true);
            //}
            StartCoroutine(SetLineUI(false, 1f,dialogue.playerID));
        }
    }

    IEnumerator SetLineUI(bool active, float delay, int playerID)
    {
        yield return new WaitForSeconds(delay);
        if (lineUIs[playerID] != null)
        {
            lineUIs[playerID].SetActive(active);
            lineUIs[1-playerID].SetActive(false);
        }
            

        if (!active)
        {
            isPlayingLines = false;
            blockInput = false;
        }
    }
}