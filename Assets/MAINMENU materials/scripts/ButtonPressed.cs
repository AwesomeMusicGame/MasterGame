using UnityEngine;
using System.Collections;

public class ButtonPressed : MonoBehaviour {

    AudioSource highlightedButtonAudio;
    AudioSource pressedButtonAudio;
	Animator anim;

	public AudioSource[] myAudios;

    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
		highlightedButtonAudio = audios[0];
	    pressedButtonAudio = audios[1];
		myAudios = audios;
    }


	public void PlayClickButtonSound()
	{
		pressedButtonAudio.PlayOneShot (pressedButtonAudio.clip);
	}

	// Update is called once per frame
	void Update () {


	    /*if (Input.GetKeyDown (KeyCode.Mouse0)) {
			highlightedButtonAudio.Play();
		}

		if (Input.GetKeyDown (KeyCode.Mouse1)) {
			pressedButtonAudio.Play();
		}*/
	}
}
