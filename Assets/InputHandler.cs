using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using static UnityEngine.InputSystem.InputAction;
public class InputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerController player;

    private bool blockInput;


    // Start is called before the first frame update
    private void Awake()
    {
        ReconnectInput();
    }
    public void ReconnectInput()
    {
        playerInput = GetComponent<PlayerInput>();
        var players = FindObjectsOfType<PlayerController>();
        var index = playerInput.playerIndex;
        player = players.FirstOrDefault(m => m.playerID == index);
    }


    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.Instance)
        {
            blockInput = DialogueManager.Instance.blockInput;
        }
    }

    public void OnMove(CallbackContext ctx)
    {
        if (blockInput) return;

        player.move = ctx.ReadValue<Vector2>();
    }

    public void Jump(CallbackContext ctx)
    {
        if (blockInput) return;

        if (ctx.performed)
        player.Jump();

        //DialogueManager.Instance.DisplayLine(0, 0);
    }

    public void OnRaiseLeftHand(CallbackContext ctx)
    {
        if (blockInput) return;

        player.handLeft.leftHandKeyDetected = ctx.ReadValueAsButton();
    }
    public void OnRaiseRightHand(CallbackContext ctx)
    {
        if (blockInput) return;

        player.handRight.rightHandKeyDetected = ctx.ReadValueAsButton();
    }

    public void OnPunch(CallbackContext ctx)
    {
        if (blockInput) return;

        player.isPunching = ctx.ReadValueAsButton();
    }

    public void OnLayDown(CallbackContext ctx)
    {
        if (blockInput) return;

        if (ctx.performed)
        {
            if (player.inImportantTrigger)
            {
                player.Interact();
                if(!player.blockLayDown && !player.isLaying) player.DealWithLayDown();
                else if(player.isLaying && player.blockLayDown) player.DealWithLayDown();
            }
            else
            {
                player.DealWithLayDown();
            }
        }
    }

    public void Interact(CallbackContext ctx) 
    {
        if (blockInput) return;

        if (ctx.performed)
        {
            player.Interact();
        }
    }
}
