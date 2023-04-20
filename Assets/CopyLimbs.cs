using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyLimbs : MonoBehaviour
{
    public bool mirror;
    [SerializeField] private Transform targetLimb;
    private ConfigurableJoint joint;

    private Quaternion targetInitialRotation;

    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        targetInitialRotation = targetLimb.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        joint.targetRotation = CopyLimbRoatation();
    }
    private Quaternion CopyLimbRoatation()
    {
        if(!mirror)
        return Quaternion.Inverse(targetLimb.localRotation) * targetInitialRotation;
        else
            return targetLimb.localRotation * targetInitialRotation;
    }
}
