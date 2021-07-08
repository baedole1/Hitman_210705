using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeClothing : MonoBehaviour
{
    public GameObject nowCloth;
    void Start()
    {
        nowCloth = GameObject.Find("Body");
    }

    // Update is called once per frame
    void Update()
    {
        switch (Input.inputString)
        {
            case "Q": case "q":
                KeyDown_Q();
                break;

            case "E": case "e":
                KeyDown_E();
                break;

            case "R": case "r":
                KeyDown_F();
                break;
        }

    }
    private void KeyDown_Q()
    {
        SetChildActiveFalse();
        nowCloth.transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("Q");
    }
    private void KeyDown_E()
    {
        SetChildActiveFalse();
        nowCloth.transform.GetChild(1).gameObject.SetActive(true);
        Debug.Log("E");
    }
    private void KeyDown_F()
    {
        SetChildActiveFalse();
        nowCloth.transform.GetChild(2).gameObject.SetActive(true);
        Debug.Log("R");
    }

    void SetChildActiveFalse()
    {
        for (int i = 0; i < nowCloth.transform.childCount; i++)
        {
            nowCloth.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}
