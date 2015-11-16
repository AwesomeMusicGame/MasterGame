using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverGUI : MonoBehaviour {

    private Kolajnice kolajnice;
    public Text Splash;
    public float wait = 5;

	// Use this for initialization
    void Start()
    {
        Splash.enabled = false;
        kolajnice = GameObject.FindObjectOfType<Kolajnice>().GetComponent<Kolajnice>();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (kolajnice.GameOver && !Splash.enabled)
        {
            wait -= Time.deltaTime;
            //Debug.Log("Waiting " + kolajnice.elapsedTime);
        }
        if (wait <= 0)
        {
            Splash.enabled = true;
            //Debug.Log("Waiting " + kolajnice.elapsedTime);
        } 
	}
}
