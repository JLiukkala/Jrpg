using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonHandler : MonoBehaviour {

    private bool isOver = false;
    private int seconds;

	void Start () {
        StartCoroutine(TimePassed());
    }
	
	void Update () {
        if (isOver == true)
        {
            SceneManager.LoadScene("BattleScene");
        }
    }

    IEnumerator TimePassed()
    {
        seconds = Random.Range(1, 10);
        if (seconds > 3)
        {
            yield return new WaitForSeconds(seconds);
            isOver = true;
        } else
        {
            StartCoroutine(TimePassed());
        }
    }
}
