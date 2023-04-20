using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayDownArea : Interactible
{
    public override void Interact(PlayerController player)
    {
        base.Interact(player);
        player.inLayDownEvent = true;

        if (playerP1.inLayDownEvent && playerP2.inLayDownEvent)
        {
            StartCoroutine(playerP1.ResetPlayerLayDown());
            StartCoroutine(playerP2.ResetPlayerLayDown());
            destroyAfterCollected.SetActive(false);
        }
    }


}
