using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using System;
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
    public float winCountOut = 3;

	private LoadingLevelParameter parameterScript;
	public MusicPlayer musicPlayer;

	private bool isReadyToPlay = false;

    private string path1 = @".\Assets\Songs\ThinkDude.txt";
    private string path2 = @".\Assets\Songs\Tea Blaster.txt";
    private string path3 = @".\Assets\Songs\Poor-o.txt";
    private string filename;

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
	public Text PauseText;

	private Movement movementScript;
	public float currentAudioSourceTime = -5f;


	// Use this for initialization
    void Start()
    {
		movementScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<Movement> ();
        PauseText.enabled = false; 

		if (GameObject.FindGameObjectWithTag ("LoadLevelParameterTag") != null) {
            parameterScript = GameObject.FindGameObjectWithTag("LoadLevelParameterTag").GetComponent<LoadingLevelParameter>();
            MasterPickedSong = parameterScript.getLoadLevelParameter();
            /////////////////////////////////////////////////////
            if (parameterScript.getDifficulty() == false)
                setIsEasyMode(true);
            else
                setIsEasyMode(false);
            /////////////////////////////////////////////////////
            pickedScene = parameterScript.sbsetter;
		} else {
			Debug.LogWarning ("LoadLevelParameter object not found...");
            setIsEasyMode(true);
        }
        //open file
        //////////////////////////////////////////
        string filename = "";
        switch (MasterPickedSong)
        {
            case 1:
                Debug.Log("1");
                filename = path1;
                break;
            case 2:
                Debug.Log("2");
                filename = path2;
                break;
            case 3:
                Debug.Log("3");
                filename = path3;
                break;
            case 0:
                Debug.Log(parameterScript.getCustomPath());
                Debug.Log("0");
                filename = parameterScript.getCustomPath();
                break;
        }

        ImportFromFile(filename);
        SpawnMap();

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
        foreach(GameObject text in GameObject.FindGameObjectsWithTag("UIText"))
        {
            text.GetComponent<Text>().color = picked.GetComponent<ISceneItem>().fontColor;
        }
        GameObject.FindGameObjectWithTag("PlayerMesh").GetComponent<SkinnedMeshRenderer>().materials[1].color = picked.GetComponent<ISceneItem>().noteColor;
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
				movementScript.setIfCharacterCanMove(true);
				Debug.Log("CanMove is " + movementScript.getIfCharacterCanMove());
				currentAudioSourceTime = musicPlayer.GetAudioSource().time;
				musicPlayer.GetAudioSource().mute = true;
				Debug.Log("AUDIO PAUSED IN TIME: " + currentAudioSourceTime);
				PauseText.enabled = true;
			}
			else
			{
                if (elapsedTime < 5)
                {
                    Application.LoadLevel(1);
                }
                else
                {
                    movementScript.setIfCharacterCanMove(false);
                    Debug.Log("CanMove is " + movementScript.getIfCharacterCanMove());
					musicPlayer.GetAudioSource().mute = false;
					musicPlayer.GetAudioSource().time = currentAudioSourceTime;
                    musicPlayer.GetAudioSource().Play();
                    Debug.Log("AUDIO UNPAUSED IN TIME: " + currentAudioSourceTime);
                    PauseText.enabled = false;
                }
			}
		}

		if (startTime != 0) {
			if (!Pause) {
                float audioTime = musicPlayer.getPlayTime();
                if (audioTime > 0) {
                    elapsedTime = audioTime + countInTime;
                }
                else
                {
                    if (musicPlayer.audio.clip != null)
                        if (musicPlayer.audio.clip.length < (Time.time - startTime))
                        {
                            Debug.Log("WIN");
                            Application.LoadLevel(3);
                        }
                    elapsedTime = Time.time - startTime;
                    /*if (musicPlayer.audio.clip != null)
                        if (elapsedTime > (musicPlayer.audio.clip.length + countInTime))
                        {
                            if (winCountOut <= 0)
                            {
                                Application.LoadLevel(3);
                            }
                            else 
                            {
                                musicPlayer.audio.mute = true;
                                winCountOut -= Time.deltaTime;
                                Color old =  GameObject.FindGameObjectWithTag("WinFadeOut").GetComponent<Image>().color;
                                old.a = Mathf.Lerp(1, 0, winCountOut);
                                GameObject.FindGameObjectWithTag("WinFadeOut").GetComponent<Image>().color = old;
                            }
                        }*/
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
                row = UnityEngine.Random.Range(0, 3);

                if (row == lastRow)
                {
                    if (getIsEasyMode())
                    {
                        while (row == lastRow)
                            row = UnityEngine.Random.Range(0, 3);
                    }
                    else
                    {
                        int prekazka = UnityEngine.Random.Range(0, prekazkaPrefab.Length);
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

        try
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

            return (0.4 > (nextBeat - lastBeat)) ? 1f : (nextBeat - lastBeat);
        }
        catch (Exception e)
        {
            return 1;
        }
    }

    public void ImportFromFile(string filename)
    {
        List<float> tempList = new List<float>();
        try
        {
            //need to be fixed 
            StreamReader reader = new StreamReader(filename);
            string input = reader.ReadToEnd();
            if (input != string.Empty)
            {
                string[] parts = input.Split('\n');
                foreach (string part in parts)
                {
                    float tempFloat;
                    float.TryParse(part, out tempFloat);
                    tempList.Add(tempFloat);
                    //                    Debug.Log(tempFloat + " parsed from: " + part);
                }
                //                kolajniceScript.DebugWriteAllBeats();

                Beats = tempList;
                reader.Close();
            }
            else
            {
                Debug.Log("No lvl data loaded from the file...string is empty");
            }
        }
        catch (ArgumentNullException e)
        {
            Debug.Log("Null filename.\n" + e.ToString());
        }
        catch (NullReferenceException e)
        {
            Debug.Log("Null Reference Exception.\n" + e.ToString());
        }
        catch (FileNotFoundException e)
        {
            Debug.Log("No lvl data loaded from the file...file is missing\n" + e.ToString());
        }
    }

}
