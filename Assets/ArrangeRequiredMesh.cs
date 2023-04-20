using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeRequiredMesh : MonoBehaviour
{
    public int arrangeID;
    public bool isGrabbed;
    public bool isGrabbedByP1;
    public bool isGrabbedByP2;
    public bool isGrabbedByP1Left;
    public bool isGrabbedByP2Left;
    public bool isGrabbedByP1Right;
    public bool isGrabbedByP2Right;
    public ArrangedArea area;
    public bool isInRequiredArea;
    public bool alreadyPlaced;
    public Material outlineMtl;


    private Material[] baseMtls;
    // Start is called before the first frame update
    private void OnEnable()
    {
        baseMtls = GetComponent<MeshRenderer>().materials;
        AddOutlineShader();
    }
   

    // Update is called once per frame
    void Update()
    {
        DealWithShader();
        DealWithArea();

        isGrabbedByP1 = isGrabbedByP1Left || isGrabbedByP1Right;
        isGrabbedByP2 = isGrabbedByP2Left || isGrabbedByP2Right;

        isGrabbed = isGrabbedByP1 || isGrabbedByP2;
    }

    void DealWithShader()
    {
        if(!isGrabbed && !alreadyPlaced)
        {
            AddOutlineShader();
        }
        else
        {
            GetComponent<MeshRenderer>().materials = baseMtls;
        }
    }

    void DealWithArea()
    {
        if (isGrabbed && !alreadyPlaced)
        {
            area.tipEffect.SetActive(true);
            area.SetAreaColor(isGrabbedByP1, isGrabbedByP2);
        }
        else
        {
            area.tipEffect.SetActive(false);
        }
    }

    void AddOutlineShader()
    {
        Material[] newMaterials = new Material[baseMtls.Length + 1];
        baseMtls.CopyTo(newMaterials, 0);
        newMaterials[newMaterials.Length - 1] = outlineMtl;

        GetComponent<MeshRenderer>().materials = newMaterials;
    }
}
