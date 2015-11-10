using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    private void Update()
    {
    }

    public void SetLenght(float beatLength)
    {
        GetComponent<Animator>().SetFloat("BeatLenght", beatLength);
    }

    public void StartJumpAnimation()
    {
        GetComponent<Animator>().SetTrigger("JumpTrigger");
    }

    public void StartPunchAnimation()
    {
        GetComponent<Animator>().SetTrigger("PunchTrigger");
    }

    public void StartSlideAnimation()
    {
        GetComponent<Animator>().SetTrigger("SlideTrigger");
    }

    public void StartDeadAnimation()
    {
        GetComponent<Animator>().SetTrigger("DeadTrigger");
    }
}
