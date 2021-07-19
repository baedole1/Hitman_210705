using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GoFoward : MonoBehaviour
{
    public bool isSceneStart;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FirstScene();
    }


    void FirstScene()
    {
        if (isSceneStart)
        {

            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }
}
