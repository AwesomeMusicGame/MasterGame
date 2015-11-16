using UnityEngine;
using System.Collections;

public class WallPuncherScript : MonoBehaviour {

    private ArrayList punchables = new ArrayList();
    private Kolajnice kolajnice;

	// Use this for initialization
    void Start()
    {
        kolajnice = GameObject.FindGameObjectWithTag("KolajniceTag").GetComponent<Kolajnice>();
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<BoxCollider>().size = new Vector3(kolajnice.getCurrentBeatLength(kolajnice.elapsedTime) * kolajnice.stretchingFactor, 2, 1);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Prekazka1Tag")
        {
            punchables.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Prekazka1Tag")
        {
            punchables.Remove(other.gameObject);
        }
    }

    public void Punch()
    {
        foreach (GameObject prekazka in punchables)
        {
            PrekazkaScript target = prekazka.GetComponent<PrekazkaScript>();
            if (target != null)
            {
                target.GetRekt();
            }
        }
    }
}
