using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class ClipManager : MonoBehaviour
{

    public string nextSceneName;
    private bool hasFinished;
    private VideoPlayer vp;
    
    // Start is called before the first frame update
    void Start()
    {
        vp = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(vp.clip.length - vp.time < 0.1f)
        {
            if (!hasFinished && nextSceneName != null)
            {

                StartCoroutine(SceneLoader.instance.LoadScene(nextSceneName, Color.white));
                hasFinished = true;
            }
        }
    }
}
