using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour {

    // How long it takes for the next letter to show up.
    public float delay = 0.1f;
    // The entire text.
    [SerializeField]
    private string fullText;
    // The letter at the current position.
    private string currentText = "";

	void Start () {
        StartCoroutine(ShowText());
	}
	
    IEnumerator ShowText()
    {
        // Prints out the text letter by letter.
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay); 
        }
    }
}
