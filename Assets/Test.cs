using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
            {
                Debug.Log("dasdsad");
                if (hitInfo.transform.GetComponent<MineManager>() == null)
                {
                    Debug.Log("hit");
                }
            }
        }
    }
}
