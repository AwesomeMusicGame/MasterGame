using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Kolajnice : MonoBehaviour {

    public GameObject kockaPrefab;
    public GameObject prekazkaPrefab;
    public float sideDistance = 5;
    public bool spawnMapTemp = false;

    private List<float> beats = new List<float>();

	// Use this for initialization
	void Start () {
               
	}

    public List<float> GetBeats()
    {
        return beats;
    }

    public void SetBeats(List<float> tempList)
    {
        beats = tempList;
    }

    public void WriteAllBeats()
    {
        foreach (float Beat in beats)
        {
            Debug.Log(Beat + "\n");
        }
    }

    // Update is called once per frame
    void Update () {
        if (spawnMapTemp == true)
        {
            SpawnMap();
            spawnMapTemp = false;
        }
        this.transform.position = new Vector3(0, 0, -sideDistance);
	}

    void SpawnMap()
    {
        float beatlenght = 0.5f;
        int lastrow = -1;
        // for (float i = 0; i < 300; i += beatlenght)
        for (int i = 0; i < beats.Count; i++)
        {
            float positionOffset = (kockaPrefab.transform.localScale.z/2);
            int row = Random.Range(0, 3);
            if (lastrow == row)
            {
                 GameObject tempPrekazka = (GameObject)Instantiate(prekazkaPrefab, new Vector3(i, 0, sideDistance * row + positionOffset), Quaternion.identity);
                 tempPrekazka.transform.parent = this.transform;
            }
            lastrow = row;
            //beats.Add(i);
            GameObject temp = (GameObject)Instantiate(kockaPrefab, new Vector3(i, 0, sideDistance * row + positionOffset), Quaternion.identity);
            temp.GetComponent<Stretching>().lenght = beatlenght;
            temp.transform.parent = this.transform;
        }
    }
}
