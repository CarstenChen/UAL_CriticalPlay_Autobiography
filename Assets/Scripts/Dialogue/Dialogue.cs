using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public int plotID;
    public int index;
    public int nextIndex;
    public string text;
    public int playerID;
    public AudioClip audio;
    public bool blockInput;
}
