using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeartChange : MonoBehaviour {

    public Sprite[] Hearts;
    public Image HeartUI; 
    private PlayerScript player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
        HeartUI.sprite = Hearts[player.health];
	}
}
