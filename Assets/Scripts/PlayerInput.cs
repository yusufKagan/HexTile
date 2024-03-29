using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    //public UnityEvent<Vector3> PointerClick;
    public UnityEvent<Vector3> PointerClick;
    // Update is called once per frame
    void Update()
    {
        DetectMouseClick();
    }
    void DetectMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("in");
            Vector3 mousePos=Input.mousePosition;
           PointerClick?.Invoke(mousePos);
        }
    }
}
