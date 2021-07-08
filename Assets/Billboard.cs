using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public GameObject worldObject;
    public Camera cam;
    public float offsetX;
    public float offsetY;

    //public Transform cam;
    private void Start()
    {

    }
    void Update()
    {
     
    }
    private void FixedUpdate()
    {
        //Vector3 offsetPosition = new Vector3(worldObject.transform.position.x+offsetX, worldObject.transform.position.y+offsetY, worldObject.transform.position.z);
        //transform.position = cam.WorldToScreenPoint(offsetPosition);
        
        transform.position = cam.WorldToScreenPoint(worldObject.transform.position);
        Vector3 offsetPosition = transform.position;

        transform.position = new Vector3(offsetPosition.x + offsetX, offsetPosition.y + offsetY);
    }

    //void LateUpdate()
    //{
    //    transform.LookAt(transform.position + cam.forward);
    //}
}
