using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchDamage : MonoBehaviour
{
    public PlayerController player;
    public float punchForce;
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
        //if(player.isPunching && GetComponent<GrabAndDrop>().heldObject == null)
        //{
        //    if (other.gameObject.GetComponent<Rigidbody>() != null && other.gameObject.tag != this.tag && ((other.gameObject.tag == "Item") || (other.gameObject.tag == (this.tag == "Player1" ? "Player2" : "Player1"))))
        //    {
        //        if (other.gameObject.GetComponent<Rigidbody>().mass >= 1)
        //        {
        //            Vector3 normal = (other.ClosestPoint(transform.position) - transform.position).normalized;
        //            other.GetComponent<Rigidbody>().AddForce(normal * punchForce);
        //        }

        //    }

        //}

    }
}
