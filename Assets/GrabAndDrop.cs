using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndDrop : MonoBehaviour
{
    public Rigidbody handRb;
    public bool isHolding = false;
    public GameObject heldObjectLeft;
    public GameObject heldObjectRight;

    public float grabStrength = 100f;
    public ConfigurableJoint grabJointLeft;
    public ConfigurableJoint grabJointRight;
    public float throwItemMultiplier = 2f;
    public float throwPlayerMultiplier = 20f;

    public bool isLeftArm = false;

    public Animator targetAnimator;
    public PlayerController player;

    public bool itemOnLeft;
    public bool itemOnRight;

    public bool leftHandKeyDetected;
    public bool rightHandKeyDetected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLeftArm)
        {
            if (!leftHandKeyDetected)
            {
                targetAnimator.SetBool("IsLeftHandUp", false);
                targetAnimator.SetBool("Holding", false);

                Drop(true);
                isHolding = false;
                itemOnLeft = false;
            }
            else
            {
                if (rightHandKeyDetected)
                {
                    targetAnimator.SetBool("IsLeftHandUp", true);
                    targetAnimator.SetBool("Holding", true);
                }
                else
                {
                    targetAnimator.SetBool("IsLeftHandUp", true);
                }
            }
        }
        else
        {
            if (!rightHandKeyDetected)
            {
                targetAnimator.SetBool("IsRightHandUp", false);
                targetAnimator.SetBool("Holding", false);

                Drop(false);
                isHolding = false;
                itemOnRight = false;
            }
            else
            {
                if (leftHandKeyDetected)
                {
                    targetAnimator.SetBool("IsRightHandUp", true);
                    targetAnimator.SetBool("Holding", true);
                }
                else
                {
                    targetAnimator.SetBool("IsRightHandUp", true);
                }
            }
        }

        if(isLeftArm)
        targetAnimator.SetBool("LeftHolding", isHolding||itemOnLeft);
        else
            targetAnimator.SetBool("RightHolding", isHolding || itemOnRight);
    }

    public void Drop(bool isLeftArm)
    {
        if (isLeftArm && heldObjectLeft)
        {
            if (heldObjectLeft.tag == "ArrangedItem")
            {
                ArrangeRequiredMesh m = heldObjectLeft.GetComponent<ArrangeRequiredMesh>();

                if (this.tag == "Player1") m.isGrabbedByP1Left = false;
                if (this.tag == "Player2") m.isGrabbedByP2Left = false;
            }

            Destroy(grabJointLeft);

            if((heldObjectLeft.tag=="Player1"|| heldObjectLeft.tag == "Player2")&&heldObjectLeft!=heldObjectRight)
            {
                heldObjectLeft.GetComponent<Rigidbody>().AddForce(heldObjectLeft.GetComponent<Rigidbody>().velocity * throwPlayerMultiplier);
                //heldObjectLeft.GetComponent<Rigidbody>().velocity = heldObjectLeft.GetComponent<Rigidbody>().velocity * throwPlayerMultiplier;
            }
            else
            {
                heldObjectLeft.GetComponent<Rigidbody>().velocity = heldObjectLeft.GetComponent<Rigidbody>().velocity * throwItemMultiplier;
            }

            heldObjectLeft = null;
        }
        else if (!isLeftArm && heldObjectRight)
        {
            if (heldObjectRight.tag == "ArrangedItem")
            {
                ArrangeRequiredMesh m = heldObjectRight.GetComponent<ArrangeRequiredMesh>();

                if (this.tag == "Player1") m.isGrabbedByP1Right = false;
                if (this.tag == "Player2") m.isGrabbedByP2Right = false;
            }

            Destroy(grabJointRight);

            if ((heldObjectRight.tag == "Player1" || heldObjectRight.tag == "Player2") && heldObjectLeft != heldObjectRight)
            {
                heldObjectRight.GetComponent<Rigidbody>().AddForce(heldObjectRight.GetComponent<Rigidbody>().velocity * throwPlayerMultiplier);
                //heldObjectRight.GetComponent<Rigidbody>().velocity = heldObjectRight.GetComponent<Rigidbody>().velocity * throwPlayerMultiplier;
            }
            else
            {
                heldObjectRight.GetComponent<Rigidbody>().velocity = heldObjectRight.GetComponent<Rigidbody>().velocity * throwItemMultiplier;
            }

            heldObjectRight = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isHolding)
        {
            return;
        }

        if (isLeftArm)
        {
            if (leftHandKeyDetected && other.gameObject.GetComponent<Rigidbody>() != null && other.gameObject.tag != this.tag && ((other.gameObject.tag == "Item") || (other.gameObject.tag == "ArrangedItem")||(other.gameObject.tag == (this.tag == "Player1" ? "Player2" : "Player1"))||(other.gameObject.tag == (this.tag == "Player1" ? "Player2MainBody" : "Player1MainBody"))))
            {
                targetAnimator.SetBool("IsLeftHandUp", true);
                heldObjectLeft = other.gameObject;

                Rigidbody heldObjectRb = other.GetComponent<Rigidbody>();

                isHolding = true;
                itemOnLeft = true;

                grabJointLeft = other.gameObject.AddComponent(typeof(ConfigurableJoint)) as ConfigurableJoint;

                grabJointLeft.connectedBody = handRb;

                //Set Joint
                JointDrive jointDrive = grabJointLeft.angularXDrive;
                jointDrive.positionSpring = grabStrength;

                grabJointLeft.xMotion = ConfigurableJointMotion.Locked;
                grabJointLeft.yMotion = ConfigurableJointMotion.Locked;
                grabJointLeft.zMotion = ConfigurableJointMotion.Locked;

                grabJointLeft.angularXMotion = ConfigurableJointMotion.Locked;
                grabJointLeft.angularYMotion = ConfigurableJointMotion.Locked;
                grabJointLeft.angularZMotion = ConfigurableJointMotion.Locked;

                grabJointLeft.anchor = new Vector3(0, 0, 0);

                grabJointLeft.breakForce = 8000f;

                grabJointLeft.angularXDrive = jointDrive;
                grabJointLeft.angularYZDrive = jointDrive;

                grabJointLeft.connectedMassScale = 0.5f;

                //grabJointLeft.autoConfigureConnectedAnchor = false;
            }

            if (heldObjectLeft && heldObjectLeft.tag == "ArrangedItem")
            {
                ArrangeRequiredMesh m = other.GetComponent<ArrangeRequiredMesh>();
                m.isGrabbed = true;
                if (this.tag == "Player1") m.isGrabbedByP1Left = true;
                if (this.tag == "Player2") m.isGrabbedByP2Left = true;
            }
        }
        else
        {
            if (rightHandKeyDetected && other.gameObject.GetComponent<Rigidbody>() != null && other.gameObject.tag != this.tag && ((other.gameObject.tag == "Item") || (other.gameObject.tag == "ArrangedItem") || (other.gameObject.tag == (this.tag == "Player1" ? "Player2" : "Player1")) || (other.gameObject.tag == (this.tag == "Player1" ? "Player2MainBody" : "Player1MainBody"))))
            {
                targetAnimator.SetBool("IsRightHandUp", true);
                heldObjectRight = other.gameObject;

                Rigidbody heldObjectRb = other.GetComponent<Rigidbody>();

                isHolding = true;
                itemOnRight = true;

                grabJointRight = other.gameObject.AddComponent(typeof(ConfigurableJoint)) as ConfigurableJoint;

                grabJointRight.connectedBody = handRb;

                //Set Joint
                JointDrive jointDrive = grabJointRight.angularXDrive;
                jointDrive.positionSpring = grabStrength;

                grabJointRight.xMotion = ConfigurableJointMotion.Locked;
                grabJointRight.yMotion = ConfigurableJointMotion.Locked;
                grabJointRight.zMotion = ConfigurableJointMotion.Locked;

                grabJointRight.angularXMotion = ConfigurableJointMotion.Locked;
                grabJointRight.angularYMotion = ConfigurableJointMotion.Locked;
                grabJointRight.angularZMotion = ConfigurableJointMotion.Locked;

                grabJointRight.anchor = new Vector3(0, 0, 0);

                grabJointRight.breakForce = 8000f;

                grabJointRight.angularXDrive = jointDrive;
                grabJointRight.angularYZDrive = jointDrive;

                grabJointRight.connectedMassScale = 0.5f;

                //grabJointRight.autoConfigureConnectedAnchor = false;
            }

            if (heldObjectRight && heldObjectRight.tag == "ArrangedItem")
            {
                ArrangeRequiredMesh m = heldObjectRight.GetComponent<ArrangeRequiredMesh>();
                m.isGrabbed = true;
                if (this.tag == "Player1") m.isGrabbedByP1Right = true;
                if (this.tag == "Player2") m.isGrabbedByP2Right = true;
            }
        }
    }
}
