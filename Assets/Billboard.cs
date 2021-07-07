using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public GameObject worldObject;
    public Camera cam;
    //public Transform cam;
    private void Start()
    {

    }
    void Update()
    {
     
    }
    private void FixedUpdate()
    {

        transform.position = cam.WorldToScreenPoint(worldObject.transform.position);
    }

    //void LateUpdate()
    //{
    //    transform.LookAt(transform.position + cam.forward);
    //}
}
