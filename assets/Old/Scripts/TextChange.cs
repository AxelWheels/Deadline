using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextChange : MonoBehaviour {

    [SerializeField]
    private Text newText = null;
    private Color textColour = new Color(0.0f, 0.0f, 0.0f, 1.0f);
    private bool trigger = false;
    public SpeechBox speechText;

	// Use this for initialization
	void Start () {
        newText.text = speechText.GetText(); // places new text from Speech Box script
	}
	
	// Update is called once per frame
	void Update () {
        trigger = speechText.eventTrigger;
        newText.text = speechText.GetText();

        if (trigger)
        {
            textColour.a = 1.0f; // makes text visible with the text box
        }
        else
        {
            textColour.a = 0.0f; // makes text transparent with the text box
        }
        newText.color = textColour;
	}
}