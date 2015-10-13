using UnityEngine;
using System.Collections;

public class Stretching : MonoBehaviour {

    public float lenght = 2;
        
    // Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.localScale = new Vector3(lenght, 0.1f, 1f);
	}
}