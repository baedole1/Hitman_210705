using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCam : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //todo : 카메라가 회전했을때 카메라 각도에 맞춰서 카메라 타겟인 큐브도 회전
        //if (angle > 0)
        //{
        //    transform.RotateAround(target.transform.position, Vector3.down, angle);
        //}

        //if (angle < 0)
        //{
        //    transform.RotateAround(target.transform.position, Vector3.up, angle);
        //}

    }
}
