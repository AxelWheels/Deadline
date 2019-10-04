using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeechBox : MonoBehaviour {

    Color shapeColour = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    public Image theImage;
    string newText;
    public bool eventTrigger = false;
    private int waitTime;
    //public PlayerScript player;

	// Use this for initialization
	void Start () {
        waitTime = 0;
        theImage.color = shapeColour;
        //theImage.material.color = shapeColour;
    }

    public void SetText(string intext)
    {
        newText = intext;
    }

    public string GetText()
    {
        return newText;
    }

	// Update is called once per frame
	void Update () {
	    if (eventTrigger)
        {
            waitTime++;
            shapeColour.a = 1.0f;
        }
        else
        {
            shapeColour.a = 0.0f;
        }
        theImage.color = shapeColour;
        //theImage.material.color = shapeColour;

        if(waitTime >= 100)
        {
            eventTrigger = false;
        }
    }
}
