using UnityEngine;
using System.Collections;

public class Stretching : MonoBehaviour {

    public float lenght = 2;
        
    // Use this for initialization
	void Start () {
		this.transform.localScale = new Vector3(lenght, 0.1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
	}
}