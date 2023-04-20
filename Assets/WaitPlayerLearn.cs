using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitPlayerLearn : MonoBehaviour
{
    public enum PlayerBehaviour
    {
        Lie,Grab,WalkAndJump
    }
    public PlayerController playerP1;
    public PlayerController playerP2;
    public bool performedP1;
    public bool performedP2;
    public PlayerBehaviour behaviour;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (behaviour)
        {
            case PlayerBehaviour.Lie:
                if (playerP1.isLaying) performedP1 = true;
                if (playerP2.isLaying) performedP2 = true;
                break;
            case PlayerBehaviour.Grab:
                if (playerP1.handLeft.itemOnLeft|| playerP1.handRight.itemOnRight) performedP1 = true;
                if (playerP2.handLeft.itemOnLeft || playerP2.handRight.itemOnRight) performedP2 = true;
                if (GameManager.Instance.arrangementFinished) { performedP1 = true; performedP2 = true; }
                break;
            case PlayerBehaviour.WalkAndJump:
                if (playerP1.move.magnitude > 0.1f && !playerP1.isGrounded) performedP1 = true;
                if (playerP2.move.magnitude > 0.1f && !playerP2.isGrounded) performedP2 = true;
                break;
        }

        if (performedP1 && performedP2)
        {
            this.gameObject.SetActive(false);
        }
    }
}
