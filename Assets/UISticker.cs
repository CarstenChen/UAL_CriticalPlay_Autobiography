using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISticker : MonoBehaviour
{
    public GameObject stickToObject;
    public Vector3 offset;
    private RectTransform uiTransform;
    // Start is called before the first frame update
    void Start()
    {
        uiTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(stickToObject.transform.position) + offset;
        uiTransform.position = screenPosition;
    }
}
