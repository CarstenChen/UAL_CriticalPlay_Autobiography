using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    public bool isP1Ready;
    public bool isP2Ready;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isP1Ready && isP2Ready)
        {
            StartCoroutine(SceneLoader.instance.LoadScene("DecoratedRoom", Color.white));

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player1MainBody")
        {
            isP1Ready = true;
        }
        if (other.tag == "Player2MainBody")
        {
            isP2Ready = true;
        }      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player1MainBody")
        {
            isP1Ready = false;
        }
        if (other.tag == "Player2MainBody")
        {
            isP2Ready = false;
        }
    }
}
