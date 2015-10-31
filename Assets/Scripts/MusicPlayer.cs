using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour {

    private Kolajnice kolajnice;
    private GameObject character;

    public AudioClip song1;
    public AudioClip song2;
    public AudioClip song3;

    public int pickedSong = 3;

    private AudioSource audio;

    // Use this for initialization
    void Start() {
        kolajnice = GameObject.FindGameObjectWithTag("KolajniceTag").GetComponent<Kolajnice>();
        character = GameObject.FindGameObjectWithTag("Player");
        audio = GetComponent<AudioSource>();
        //pickedSong = kolajnice.MasterPickedSong;
        switch (pickedSong)
        {
            case 1:
                audio.clip = song1;
                break;
            case 2:
                audio.clip = song2;
                break;
            case 3:
                audio.clip = song3;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!audio.isPlaying && Time.time >= kolajnice.countInTime)
        {
            audio.Play();
        }
        if (character.transform.localPosition.y < 0)
        {
            audio.pitch = Mathf.Lerp(1, 0, character.transform.localPosition.y / -8);
            if (character.transform.localPosition.y < -8)
            {
                kolajnice.GameOver = true;
            }
        }
        else
        {
            audio.pitch = 1;
        }
        if (kolajnice.GameOver)
        {
            audio.Stop();
        }
	}
}
