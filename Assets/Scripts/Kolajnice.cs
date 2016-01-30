using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Kolajnice : MonoBehaviour {

    public GameObject[] SceneVariants;
    public GameObject TextPrefab;
    public GameObject kockaPrefab;
    public GameObject[] prekazkaPrefab;
    public float sideDistance = 5;
    public float countInTime = 5;
    public float stretchingFactor = 5;
    public int MasterPickedSong = 1;
    public bool GameOver = false;
    public float elapsedTime = 0;
    private float startTime = 0;
    public float gameOverWait = 0;
    public int pickedScene = 2;
    private bool isEasy = true;
	public GameObject debug;

	private LoadingLevelParameter parameterScript;
	public MusicPlayer musicPlayer;

	private bool isReadyToPlay = false;

    private List<float> _beats = new List<float>();
    public List<float> Beats
    {
        get
        {
            return _beats;
        }
        set
        {
            _beats = value;
        }
    }
    public float SongTime
    {
        get
        {
            return elapsedTime - countInTime;
        }
    }

	public bool Pause = false;
	public float currentAudioSourceTime = -5f;
	public Text PauseText;

	private Movement movementScript;

	// Use this for initialization
    void Start()
    {
		movementScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<Movement> ();
        PauseText.enabled = false; 

		if (GameObject.FindGameObjectWithTag ("LoadLevelParameterTag") == null) {
			Debug.LogWarning ("LoadLevelParameter object not found...");
			setIsEasyMode(true);
		} else {
			parameterScript = GameObject.FindGameObjectWithTag ("LoadLevelParameterTag").GetComponent<LoadingLevelParameter> ();
			MasterPickedSong = parameterScript.getLoadLevelParameter();
			/////////////////////////////////////////////////////
			if(parameterScript.getDifficulty() == false)
				setIsEasyMode(true);
			else
				setIsEasyMode(false);
			/////////////////////////////////////////////////////
            pickedScene = parameterScript.getTypeOfBg();
		}
		Debug.Log ("EASY MODE IS >>> " + getIsEasyMode()); //nefunguje z nejakeho dovodu.... 
		musicPlayer = GameObject.FindGameObjectWithTag ("MusicPlayer").GetComponent<MusicPlayer> ();
		musicPlayer.pickedSong = MasterPickedSong;

        this.transform.position = new Vector3(stretchingFactor * countInTime, 0, -sideDistance);
		elapsedTime = 0;

        
//#if UNITY_EDITOR
//        startTime = -0.01f;
//#else
        startTime = Time.time;
//#endif
    }

	public void setIsEasyMode (bool i)
	{
		isEasy = i;
	}

	public bool getIsEasyMode()
	{
		return isEasy;
	}

    private void SetVisualPreset()
    {
        GameObject picked = Instantiate(SceneVariants[pickedScene], transform.position, Quaternion.identity) as GameObject;

        //set veci z presetu
        RenderSettings.skybox = picked.GetComponent<ISceneItem>().skyboxMaterial;
        prekazkaPrefab = new GameObject[2];
        prekazkaPrefab[0] = picked.GetComponent<ISceneItem>().prekazkaPunch;
        prekazkaPrefab[1] = picked.GetComponent<ISceneItem>().prekazkaSlide;
        kockaPrefab.GetComponent<Stretching>().SetMaterial(picked.GetComponent<ISceneItem>().podlahaMaterial);
        (GameObject.FindGameObjectWithTag("UIText") as GameObject).GetComponent<Text>().color = picked.GetComponent<ISceneItem>().fontColor;
        //(GameObject.FindGameObjectWithTag("PlayerMesh") as GameObject).GetComponent<notaSetter>().SetColor(picked.GetComponent<ISceneItem>().noteColor);
        TextPrefab.GetComponent<TextMesh>().color = picked.GetComponent<ISceneItem>().countInFontColor;
    }

    // Update is called once per frame
    void Update () {

		if (Input.GetButtonDown ("Cancel")) {
			Application.LoadLevel(2);
		}

		if (Input.GetButtonDown ("Pause")) {
			Pause = !Pause;
			if(Pause)
			{
				movementScript.setIfCharacterCanMove(false);
				Debug.Log("CanMove is " + movementScript.getIfCharacterCanMove());
				currentAudioSourceTime = musicPlayer.GetAudioSource().time;
				musicPlayer.GetAudioSource().mute = true;
				Debug.Log("AUDIO PAUSED IN TIME: " + currentAudioSourceTime);
				PauseText.enabled = true;
			}
			else
			{
				movementScript.setIfCharacterCanMove(true);
				Debug.Log("CanMove is " + movementScript.getIfCharacterCanMove());
				musicPlayer.GetAudioSource().mute = false;
				musicPlayer.GetAudioSource().time = currentAudioSourceTime;
				musicPlayer.GetAudioSource().Play();
				Debug.Log ("AUDIO UNPAUSED");
				PauseText.enabled = false;
			}
		}


		if (startTime != 0) {
			if (!Pause) {
				float audioTime = musicPlayer.getPlayTime ();
				if (audioTime > 0) {
					elapsedTime = audioTime + countInTime;
                }
                else
                {
                    if (musicPlayer.audio.clip.length+3 < (Time.time - startTime))
                    {
                        Debug.Log("WIN");
						Application.LoadLevel(3);
                    }
					elapsedTime = Time.time - startTime;
				}
			}

			if (!GameOver) {
				this.transform.position = new Vector3 (-(elapsedTime - countInTime) * stretchingFactor, 0, -sideDistance);
			} else {
				if (gameOverWait > 0)
					gameOverWait -= Time.deltaTime;
				else
					Application.LoadLevel (2);
			}
			
		} 
	}

    public void SpawnMap()
    {
        SetVisualPreset();

        //some inits
        int lastRow = 1;
        int row = lastRow;
        float beatLenght = 0;
        float lastBeat = 0;
        float modifiedBeat;

        // generate startinging line 
        GameObject tempStart = (GameObject)Instantiate(kockaPrefab, new Vector3(lastBeat, 0, sideDistance * row), Quaternion.identity);
        tempStart.transform.parent = this.transform;
        tempStart.GetComponent<Stretching>().lenght = - countInTime * stretchingFactor;
        bool FirstLineDone = false;

        //generate count in numbers
        for (int i = 0; i < countInTime; i++)
        {
            GameObject tempText = (GameObject) Instantiate(TextPrefab, new Vector3(-i * stretchingFactor, 0, sideDistance), Quaternion.AngleAxis(90, transform.up));
            tempText.transform.parent = this.transform;
            tempText.GetComponent<TextMesh>().text = i.ToString();
            Color orig = tempText.GetComponent<TextMesh>().color;
            tempText.GetComponent<TextMesh>().color = new Color(orig.r, orig.g, orig.b, (1 / countInTime) * i);
        }

        foreach (float currentBeat in Beats)
        {

            modifiedBeat = currentBeat * stretchingFactor;

            beatLenght = modifiedBeat - lastBeat;
            if (beatLenght <= 0)
            {
                beatLenght = 100; // level end, one long ass lane
            }

            if (FirstLineDone)
            {
                row = Random.Range(0, 3);

                if (row == lastRow)
                {
                    if (getIsEasyMode())
                    {
                        while (row == lastRow)
                            row = Random.Range(0, 3);
                    }
                    else
                    {
                        int prekazka = Random.Range(0, prekazkaPrefab.Length);
                        GameObject tempPrekazka = (GameObject)Instantiate(prekazkaPrefab[prekazka], new Vector3(lastBeat + beatLenght / 4, 0, sideDistance * row), Quaternion.identity);
                        tempPrekazka.transform.parent = this.transform;
                    }
                }

                lastRow = row;
            }
            else
            {
                FirstLineDone = true;
            }

            GameObject temp = (GameObject)Instantiate(kockaPrefab, new Vector3(lastBeat, 0, sideDistance * row), Quaternion.identity);
            temp.transform.parent = this.transform;
            temp.GetComponent<Stretching>().lenght = beatLenght;

            ////Draw debug line
            //GameObject line = (GameObject)Instantiate(debug, new Vector3(lastBeat, 0, sideDistance * row), Quaternion.identity);
            //line.transform.parent = this.transform;

            lastBeat = modifiedBeat;
        }
    }

    public float getCurrentBeatLength(float time)
    {

        int nextBeatIndex = Beats.FindIndex(x => x > time);
        float nextBeat, lastBeat;

        if (Beats.Count > (nextBeatIndex - 1))
        {
            nextBeat = Beats[nextBeatIndex + 1];
            lastBeat = Beats[nextBeatIndex];
        }
        else
        {
            nextBeat = Beats[nextBeatIndex];
            lastBeat = Beats[nextBeatIndex - 1];
        }

        return (nextBeat - lastBeat);
    }

    public float getClosestBeatTime()
    {

        int nextBeatIndex = Beats.FindIndex(x => x >= SongTime);
        int lastBeatIndex = Beats.FindIndex(x => x <= SongTime);
        int resultIndex = (Mathf.Abs(SongTime - nextBeatIndex) > Mathf.Abs(SongTime - lastBeatIndex)) ? lastBeatIndex : nextBeatIndex;
        if (resultIndex < 0 || resultIndex >= Beats.Count)
            resultIndex = 0;
        return Beats[resultIndex];
    }

	/*//choosing lvl from main menu
	public void ChooseLevel(int level)
	{
		return level;
	}*/

}
