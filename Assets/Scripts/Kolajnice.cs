using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Kolajnice : MonoBehaviour {

    public GameObject debugBeat;
    public GameObject kockaPrefab;
    public GameObject prekazkaPrefab;
    public float sideDistance = 5;
    public float countInTime = 5;
    public float stretchingFactor = 5;

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
        this.transform.position = new Vector3(-(Time.time - countInTime) * stretchingFactor, 0, -sideDistance);
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
        tempStart.GetComponent<Stretching>().lenght = -100;
        bool FirstLineDone = false;

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
                do
                {
                    row = Random.Range(0, 3);
                } while (row == lastRow);
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
        float nextBeat = Beats[nextBeatIndex+1];
        float lastBeat = Beats[nextBeatIndex];

        Debug.Log(nextBeat + " - " + lastBeat);

        return (nextBeat - lastBeat);
    }

    // Debug help functions
    public void DebugWriteAllBeats()
    {
        foreach (float Beat in _beats)
        {
            Debug.Log(Beat + "\n");
        }
    }
}
