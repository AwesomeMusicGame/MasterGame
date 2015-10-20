using UnityEngine;
using System.Collections;

public class PrekazkaDeal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider other) {
		Debug.Log("LLL"); 
		if (other.gameObject.tag == "Prekazka1Tag" 
		    || other.gameObject.tag == "Prekazka2Tag") {
			if ((Input.GetAxis("Vertical") > 0))// && !canMove)
			{
				
				Destroy(other.gameObject);
				//canMove = false;
			}
			
		}
	}
}
