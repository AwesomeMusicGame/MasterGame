using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    private float jumpDistance;

	// Use this for initialization
	void Start () {
        jumpDistance = GameObject.FindGameObjectWithTag("KolajniceTag").GetComponent<Kolajnice>().sideDistance;
	}
	
	// Update is called once per frame
	void Update () {
        //Input.GetButtonDown("Horizontal")
	}
}
