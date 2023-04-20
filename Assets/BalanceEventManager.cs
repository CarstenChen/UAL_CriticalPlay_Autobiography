using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceEventManager : MonoBehaviour
{
    private static BalanceEventManager instance;
    public static BalanceEventManager Instance { get { return instance; } private set { } }
    public PlayerArea playerAreaP1;
    public PlayerArea playerAreaP2;
    public bool player1Win;
    public bool player2Win;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        playerAreaP1.gameObject.SetActive(true);
        playerAreaP2.gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player1Win || player2Win)
        {
            playerAreaP1.enabled = false;
            playerAreaP2.enabled = false;
        }
    }
}
