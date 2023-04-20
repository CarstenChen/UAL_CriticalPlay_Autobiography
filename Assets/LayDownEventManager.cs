using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayDownEventManager : MonoBehaviour
{
    private static LayDownEventManager instance;
    public static LayDownEventManager Instance { get { return instance; } private set { } }
    public bool canShowLayDownArea;
    public GameObject layDownGuideUI;
    public GameObject layDownArea;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (canShowLayDownArea)
        {
            layDownArea.SetActive(true);
            layDownGuideUI.SetActive(true);
            canShowLayDownArea = false;
        }
    }
}
