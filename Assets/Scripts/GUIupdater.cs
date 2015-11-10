using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIupdater : MonoBehaviour {

    private Kolajnice kolajnice;
    public Text timeIn; 

	public Color startColor;
	public Color endColor;

	private float t = 0; //lerp variable

    void Start()
    {
        kolajnice = GameObject.FindGameObjectWithTag("KolajniceTag").GetComponent<Kolajnice>();
		timeIn.color = startColor;
	}

	void Update () {

		//changing stuff bc 1 min. till the end
		if (((kolajnice.elapsedTime - kolajnice.countInTime)) > ((kolajnice.Beats [kolajnice.Beats.Count - 2] - 60))) {
			//...tbd
		}

		//changing color in time
		timeIn.color = Color.Lerp(startColor, endColor, t);
		if (t < 1){ 
			t += Time.deltaTime/(kolajnice.Beats [kolajnice.Beats.Count - 2]);
		}

		//print (((Time.time - kolajnice.countInTime)));
		//print ((kolajnice.Beats [kolajnice.Beats.Count - 2]));
        timeIn.text = FormatTime(kolajnice.elapsedTime - kolajnice.countInTime) + " / " + FormatTime(kolajnice.Beats[kolajnice.Beats.Count - 2]);
	}

    private string FormatTime(float time)
    {
        int multipiler = 1;
        if (time < 0) multipiler = -1;
        string result = ((multipiler < 0) ? "-" : "") + ((int)time / 60).ToString() + ":" + (time % 60 * multipiler).ToString("0.00");
        return result;
    }
}
