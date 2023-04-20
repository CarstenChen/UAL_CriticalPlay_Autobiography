using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangementManager : MonoBehaviour
{
    private static ArrangementManager instance;
    public static ArrangementManager Instance { get { return instance; } private set { } }

    public ArrangeRequiredMesh[] arrangeMeshs;
    private int totalNum;
    public int currentArrangeNum;


    private void Awake()
    {
        if (instance == null)
            instance = this;

        arrangeMeshs = GetComponentsInChildren<ArrangeRequiredMesh>();
        totalNum = arrangeMeshs.Length;


        for (int i = 0; i < arrangeMeshs.Length; i++)
        {
            arrangeMeshs[i].enabled = false;
        }

        this.enabled = false;
    }

    private void OnEnable()
    {
        for (int i = 0; i < arrangeMeshs.Length; i++)
        {
            arrangeMeshs[i].enabled = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentArrangeNum == totalNum)
        {
           GameManager.Instance.arrangementFinished = true;
        }
    }
}
