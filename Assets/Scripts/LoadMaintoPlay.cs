using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMaintoPlay : MonoBehaviour
{
    public float delayTime = 2f;
    public bool isStart = false;
    public bool isEnd = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            StartCoroutine(LoadMainToPlay());
        }

        if (isEnd)
        {
            StartCoroutine(LoadEnd());
        }

    }

	public void IsStartOn()
	{
		isStart = true;
	}

    public void IsEndOn()
    {
        isEnd = true;
    }

    IEnumerator LoadMainToPlay()
	{
        yield return new WaitForSeconds(delayTime);
        LoadingSceneController.LoadScene("Play");
    }

    IEnumerator LoadEnd()
    {
        yield return new WaitForSeconds(delayTime);
        Application.Quit();
    }
}
