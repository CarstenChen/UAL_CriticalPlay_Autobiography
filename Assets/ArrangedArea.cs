using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangedArea : MonoBehaviour
{
    public GameObject tipEffect;
    public GameObject inAreaEffect;
    public int areaID;

    public Material areaColorP1;
    public Material areaColorP2;
    public Material areaColorP1P2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "ArrangedItem")
        {
            
            ArrangeRequiredMesh m = other.GetComponent<ArrangeRequiredMesh>();
            if (m.alreadyPlaced) return;

            if (m.arrangeID == areaID)
            {
                if (m.isGrabbed)
                {
                    m.isInRequiredArea = true;
                    inAreaEffect.SetActive(true);
                }
                else
                {
                    tipEffect.SetActive(false);
                    inAreaEffect.SetActive(false);
                    m.isInRequiredArea = true;
                    m.alreadyPlaced = true;
                    ArrangementManager.Instance.currentArrangeNum++;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ArrangedItem")
        {
            ArrangeRequiredMesh m = other.GetComponent<ArrangeRequiredMesh>();
            if (m.alreadyPlaced) return;

            if (m.arrangeID == areaID)
            {
                tipEffect.SetActive(false);
                inAreaEffect.SetActive(false);
            }
        }
    }

    public void SetAreaColor(bool isGrabbedByP1, bool isGrabbedByP2)
    {
        if (isGrabbedByP1 & isGrabbedByP2)
        {
            tipEffect.GetComponent<MeshRenderer>().material = areaColorP1P2;
        }
        else if (isGrabbedByP1 && !isGrabbedByP2)
        {
            tipEffect.GetComponent<MeshRenderer>().material = areaColorP1;
        }
        else if (!isGrabbedByP1 && isGrabbedByP2)
        {
            tipEffect.GetComponent<MeshRenderer>().material = areaColorP2;
        }
    }
}
