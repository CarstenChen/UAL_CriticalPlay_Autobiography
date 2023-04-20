using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMachine : Interactible
{
    public AudioSource audio;

    public override void Interact(PlayerController player)
    {
        base.Interact(player);
        audio.Play();
        LayDownEventManager.Instance.canShowLayDownArea = true;
    }
}
