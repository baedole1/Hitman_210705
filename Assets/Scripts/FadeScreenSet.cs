using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreenSet : MonoBehaviour
{
	public GameObject fadeScreen;
	public void FadingOutMain()
	{
		fadeScreen.GetComponent<Animation>().Play("Main_Fade");
	}

	public void FadingInPlay()
	{
		fadeScreen.GetComponent<Animation>().Play("Fade");
	}


}
