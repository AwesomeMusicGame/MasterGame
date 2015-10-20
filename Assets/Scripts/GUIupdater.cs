using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIupdater : MonoBehaviour {

    private Kolajnice kolajnice;
    public Text timeIn; 

	// Use this for initialization
    void Start()
    {
        kolajnice = GameObject.FindGameObjectWithTag("KolajniceTag").GetComponent<Kolajnice>();
	}
	
	// Update is called once per frame
	void Update () {
        timeIn.text = FormatTime(Time.time - kolajnice.countInTime) + " / " + FormatTime(kolajnice.Beats[kolajnice.Beats.Count - 2]);
	}

    private string FormatTime(float time)
    {
        int multipiler = 1;
        if (time < 0) multipiler = -1;
        string result = ((multipiler < 0) ? "-" : "") + ((int)time / 60).ToString() + ":" + (time % 60 * multipiler).ToString("0.00");
        return result;
    }
}
