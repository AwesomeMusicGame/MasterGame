using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GetComponent<Animator>().SetBool("jump", true);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GetComponent<Animator>().SetBool("jump", false);
        }


        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GetComponent<Animator>().SetBool("bend", true);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            GetComponent<Animator>().SetBool("bend", false);
        }
    }
}
