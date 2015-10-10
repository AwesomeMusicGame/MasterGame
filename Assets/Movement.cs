using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    private float jumpDistance;
    private bool canMove = true;
    private Vector3[] positions = new Vector3[3];
    private int currentPosition = 1;
    private float height = 0.8f;

	// Use this for initialization
	void Start () {
        jumpDistance = GameObject.FindGameObjectWithTag("KolajniceTag").GetComponent<Kolajnice>().sideDistance;
        positions[0] = new Vector3(0, height, -jumpDistance);
        positions[1] = new Vector3(0, height, 0);
        positions[2] = new Vector3(0, height, jumpDistance);
        this.transform.position = positions[currentPosition];
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetAxis("Horizontal") == 0) && !canMove) canMove = true;
        if ((Input.GetAxis("Horizontal") > 0) && canMove)
        {
            currentPosition--;
            if (currentPosition < 0) currentPosition = 2;
            this.transform.position = positions[currentPosition];
            canMove = false;
        }
        if ((Input.GetAxis("Horizontal") < 0) && canMove)
        {
            currentPosition++;
            if (currentPosition > 2) currentPosition = 0;
            this.transform.position = positions[currentPosition];
            canMove = false;
        }
	}
}
