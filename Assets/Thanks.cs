using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Thanks : MonoBehaviour
{
    public bool hasChangeToThanks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerFullRoom.Instance.balanceEventFinished &&!hasChangeToThanks)
        {
            GetComponent<TextMeshProUGUI>().text = "THANKS";
            hasChangeToThanks = true;
        }
    }
}
