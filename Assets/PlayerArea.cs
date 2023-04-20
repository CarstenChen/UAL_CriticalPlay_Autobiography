using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArea : MonoBehaviour
{
    public PlayerController playerP1;
    public PlayerController playerP2;

    public int playerID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerID == 0)
        {
            if (other.tag == "Player2MainBody")
            {
                BalanceEventManager.Instance.player1Win = true;
            }
        }
        else if (playerID == 1)
        {
            if (other.tag == "Player1MainBody")
            {
                BalanceEventManager.Instance.player2Win = true;
            }
        }
    }
}
