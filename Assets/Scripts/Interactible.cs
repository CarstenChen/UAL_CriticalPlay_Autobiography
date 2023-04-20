using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactible : MonoBehaviour
{
    [Header("Interactibe Settings")]
    public GameObject destroyAfterCollected;
    public Material outlineMtl;
    public PlayerController playerP1;
    public PlayerController playerP2;
    public GameObject buttonUIP1;
    public GameObject buttonUIP2;
    public bool distinguishPlayerInteract;
    public bool blockLayDown;

    protected bool hasShownUIP1;
    protected bool hasShownUIP2;

    protected bool canInteractP1;
    protected bool canInteractP2;
    protected bool alreadyInteracted;


    private Material[] baseMtls;

    protected virtual void OnEnable()
    {
        canInteractP1 = true;
        canInteractP2 = true;

        if (GetComponent<MeshRenderer>())
        {
            baseMtls = GetComponent<MeshRenderer>().materials;
        }

        AddOutlineShader();
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        buttonUIP1.SetActive(false);
        buttonUIP2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Interact(PlayerController player)
    {
        if (GetComponent<MeshRenderer>())
            GetComponent<MeshRenderer>().materials = baseMtls;

        if((!distinguishPlayerInteract && (!canInteractP1||!canInteractP2))|| (!canInteractP1 && !canInteractP2))
        {
            playerP1.inImportantTrigger = false;
            playerP2.inImportantTrigger = false;
            alreadyInteracted = true;
        }

    }

    void AddOutlineShader()
    {
        if (outlineMtl)
        {
            Material[] newMaterials = new Material[baseMtls.Length + 1];
            baseMtls.CopyTo(newMaterials, 0);
            newMaterials[newMaterials.Length - 1] = outlineMtl;

            GetComponent<MeshRenderer>().materials = newMaterials;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!this.enabled) return;
        if (other.tag == "Player1MainBody" && !hasShownUIP1 && !alreadyInteracted) 
        {
            buttonUIP1.SetActive(true);
            hasShownUIP1 = true;
            playerP1.inImportantTrigger = true;
            if (blockLayDown) playerP1.blockLayDown = true;
        }
        if (other.tag == "Player2MainBody" && !hasShownUIP2 && !alreadyInteracted)
        {

            buttonUIP2.SetActive(true);
            hasShownUIP2 = true;
            playerP2.inImportantTrigger = true;
            if (blockLayDown) playerP2.blockLayDown = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!this.enabled) return;
        if (other.tag == "Player1MainBody")
        {
            buttonUIP1.SetActive(false);
            hasShownUIP1 = false;
            playerP1.inImportantTrigger = false;
            playerP1.blockLayDown = false;
        }
        if (other.tag == "Player2MainBody")
        {
            buttonUIP2.SetActive(false);
            hasShownUIP2 = false;
            playerP2.inImportantTrigger = false;
            playerP2.blockLayDown = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!this.enabled) return;
        if (other.tag == "Player1MainBody" && canInteractP1 && !alreadyInteracted)
        {

            if (playerP1.GetInteractState())
            {
                Debug.Log("interact");
                canInteractP1 = false;
                Interact(playerP1);

                hasShownUIP1 = false;
                playerP1.resetInteract = true;
                buttonUIP1.SetActive(false);
                hasShownUIP1 = false;

                if (!distinguishPlayerInteract)
                {
                    hasShownUIP2 = false;

                    buttonUIP2.SetActive(false);
                    hasShownUIP2 = false;
                    playerP2.resetInteract = true;
                }
            }
        }
        else if (other.tag == "Player2MainBody" && canInteractP2 && !alreadyInteracted)
        {
            if (playerP2.GetInteractState())
            {
                Debug.Log("interact");

                canInteractP2 = false;
                Interact(playerP2);

                hasShownUIP2 = false;
                playerP2.resetInteract = true;
                buttonUIP2.SetActive(false);
                hasShownUIP2 = false;

                if (!distinguishPlayerInteract)
                {
                    hasShownUIP1 = false;

                    playerP1.resetInteract = true;
                    buttonUIP1.SetActive(false);
                    hasShownUIP1 = false;
                }
            }
        }
    }
}

