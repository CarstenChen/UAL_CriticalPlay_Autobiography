using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustGravity : MonoBehaviour
{
    public PlayerController player;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(Physics.gravity);
        if (rb.velocity.y < 0 && !player.isGrounded)
        {
            Physics.gravity = new Vector3(0, -25f, 0);
        }
        else
        {
            Physics.gravity = new Vector3(0, -10f, 0);
        }
    }
}
