using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Kolajnice : MonoBehaviour {

    public GameObject TextPrefab;
    public GameObject kockaPrefab;
    public GameObject[] prekazkaPrefab = new GameObject[2];
    public float sideDistance = 5;
    public float countInTime = 5;
    public float stretchingFactor = 5;
    public int MasterPickedSong = 1;
    public bool GameOver = false;

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


	// Use this for initialization
    void Start() {
        this.transform.position = new Vector3(stretchingFactor * countInTime, 0, -sideDistance);
	}

    // Update is called once per frame
    void Update () {
        if (!GameOver)
        {
            this.transform.position = new Vector3(-(Time.time - countInTime) * stretchingFactor, 0, -sideDistance);
        }
	}

    public void SpawnMap()
    {
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
        }

        foreach (float currentBeat in Beats)
        {
            //DEBUG LINES
            //Instantiate(debugBeat, new Vector3(currentBeat, 0, 0), debugBeat.transform.localRotation);

            modifiedBeat = currentBeat * stretchingFactor;

            beatLenght = modifiedBeat - lastBeat;
            if (beatLenght <= 0)
            {
                beatLenght = 100; // level end, one long ass lane
            }

            if (FirstLineDone)
            {
                //do
                //{
                //    row = Random.Range(0, 3);
                //} while (row == lastRow);

                row = Random.Range(0, 3);
                if (row == lastRow)
                {
                    int prekazkaType = Random.Range(0, 2);

                    GameObject prek = (GameObject)Instantiate(prekazkaPrefab[prekazkaType], new Vector3(lastBeat, 0, sideDistance * row), Quaternion.identity);
					prek.transform.parent = this.transform;
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
            
            lastBeat = modifiedBeat;
        }
    }

    public float getCurrentBeatLength(float time) {

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

	/*//choosing lvl from main menu
	public void ChooseLevel(int level)
	{
		return level;
	}*/

    // Debug help functions
    public void DebugWriteAllBeats()
    {
        foreach (float Beat in _beats)
        {
            Debug.Log(Beat + "\n");
        }
    }
}
