using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour {

    private Kolajnice kolajnice;

    //[SerializeField]float timePassed = 0;
    AudioSource audio;

    // Use this for initialization
    void Start() {
        kolajnice = GameObject.FindGameObjectWithTag("KolajniceTag").GetComponent<Kolajnice>();
        audio = GetComponent<AudioSource>();
        //audio.Play();
    }
	
	// Update is called once per frame
	void Update () {
       // timePassed = Time.timeSinceLevelLoad;
        if (!audio.isPlaying && Time.time >= kolajnice.countInTime)
        {
            audio.Play();
        }
	}
}
