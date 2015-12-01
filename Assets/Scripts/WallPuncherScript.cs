using UnityEngine;
using System.Collections;

public class WallPuncherScript : MonoBehaviour {

    private ArrayList punchables = new ArrayList();
    private ArrayList slidables = new ArrayList();
    private Kolajnice kolajnice;
    private float sizeMod = 1.7f;

	// Use this for initialization
    void Start()
    {
        kolajnice = GameObject.FindGameObjectWithTag("KolajniceTag").GetComponent<Kolajnice>();
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<BoxCollider>().size = new Vector3(kolajnice.getCurrentBeatLength(kolajnice.elapsedTime) * kolajnice.stretchingFactor * sizeMod, 2, 1);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Prekazka1Tag")
        {
            punchables.Add(other.gameObject);
        }
        if (other.gameObject.tag == "Prekazka2Tag")
        {
            slidables.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Prekazka1Tag")
        {
            punchables.Remove(other.gameObject);
        }
        if (other.gameObject.tag == "Prekazka2Tag")
        {
            slidables.Remove(other.gameObject);
        }
    }

    public void Punch()
    {
        foreach (GameObject prekazka in punchables)
        {
            PrekazkaBase target = prekazka.GetComponent<PrekazkaBase>();
            if (target != null)
            {
                target.Kill();
            }
        }
    }

    public void Slide()
    {
        foreach (GameObject prekazka in slidables)
        {
            PrekazkaBase target = prekazka.GetComponent<PrekazkaBase>();
            if (target != null)
            {
                target.Kill();
            }
        }
    }
}
