using UnityEngine;
using System.Collections;
using System.Threading;

public class Movement : MonoBehaviour {

    //stuff for movement management
    private float jumpDistance;
    private int currentPosition = 0;
    private bool canMove = true;
    private float animLength;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float startTime = 0;
    private float animSpeedMultipiler = 6;
    private float animationRotation = 20;
    private bool teleport = false;

    private Kolajnice kolajnice;

	// Use this for initialization
	void Start () {
        kolajnice = GameObject.FindGameObjectWithTag("KolajniceTag").GetComponent<Kolajnice>();
        jumpDistance = kolajnice.sideDistance;
        targetPosition = transform.localPosition;
        startPosition = targetPosition;
	}
	
	// Update is called once per frame
	void Update () {

        DoInput();

        DoAnimations();
	}

    private void DoInput()
    {
        // Im getting no input (buttons were released), allow the input to do stuff again
        if ((Input.GetAxis("Horizontal") == 0) && !canMove)
        {
            canMove = true;
        }
        //input right
        if ((Input.GetAxis("Horizontal") > 0) && canMove)
        {
            //calculate positions
            currentPosition--;
            targetPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - jumpDistance);
            startPosition = transform.localPosition;
            if (currentPosition < -1)
            {
                currentPosition = 1;
                teleport = true;
            }
            //calculate time stuff
            animLength = kolajnice.getCurrentBeatLength(Time.time) / animSpeedMultipiler;
            startTime = Time.time;
            //rotate in the right direction
            transform.Rotate(transform.right, -animationRotation, Space.Self);
            //disable input
            canMove = false;
            GetComponent<Rigidbody>().useGravity = false;
        }
        //input left
        if ((Input.GetAxis("Horizontal") < 0) && canMove)
        {
            //calculate positions
            currentPosition++;
            targetPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + jumpDistance);
            startPosition = transform.localPosition;
            if (currentPosition > 1)
            {
                currentPosition = -1;
                teleport = true;
            }
            //calculate time stuff
            animLength = kolajnice.getCurrentBeatLength(Time.time) / animSpeedMultipiler;
            startTime = Time.time;
            //rotate in the right direction
            transform.Rotate(transform.right, animationRotation, Space.Self);
            //disable input
            canMove = false;
            GetComponent<Rigidbody>().useGravity = false;
        }
        //held fly key
        if (Input.GetButton("Fly"))
        {
            this.transform.position = new Vector3(this.transform.position.x, 2,this.transform.position.z);
        }
    }
    private void DoAnimations()
    {
        //if not at the position I should be, do animation, used for movement
        if (transform.localPosition.z != targetPosition.z)
        {
            //lerp between where i should be and where I am by time
            float animLerp = (Time.time - startTime) / animLength;

            //if jumping through edge of the screen teleport
            if (teleport && animLerp >= 0.5)
            {
                startPosition = new Vector3(startPosition.x, startPosition.y, 2 * jumpDistance * currentPosition);
                targetPosition = new Vector3(startPosition.x, startPosition.y, jumpDistance * currentPosition);
                teleport = false;
            }

            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, animLerp);
        }
        //if I am where i should be but I'm still rotated set me straight, used after movement ends
        else if (transform.rotation != Quaternion.identity)
        {
            //rotate me back
            transform.rotation = Quaternion.identity;
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
