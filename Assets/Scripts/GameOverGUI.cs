using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverGUI : MonoBehaviour {

    private Kolajnice kolajnice;
    public Text Splash; 

	// Use this for initialization
    void Start()
    {
        Splash.enabled = false;
        kolajnice = GameObject.FindObjectOfType<Kolajnice>().GetComponent<Kolajnice>();
	}
	
	// Update is called once per frame
	void Update () {
        if (kolajnice.GameOver)
        {
            Splash.enabled = true;
        }
	}
}
