using UnityEngine;
using System.Collections;

public class Kolajnice : MonoBehaviour {

    public GameObject kockaPrefab;
    public GameObject prekazkaPrefab;
    public float sideDistance = 5;

    private ArrayList beats;

	// Use this for initialization
	void Start () {

        float beatlenght = 0.5f;
        int lastrow = -1;
        for (float i = 0; i < 300; i += beatlenght)
        {
            float positionOffset = (kockaPrefab.transform.localScale.z / 2);
            int row = Random.Range(0, 3);
            if (lastrow == row)
            {
                GameObject tempPrekazka = (GameObject)Instantiate(prekazkaPrefab, new Vector3(i, 0, sideDistance * row + positionOffset), Quaternion.identity);
                tempPrekazka.transform.parent = this.transform;
            }
            lastrow = row;
            //beats.Add(i);
            GameObject temp = (GameObject) Instantiate(kockaPrefab, new Vector3(i, 0, sideDistance * row + positionOffset), Quaternion.identity);
            temp.GetComponent<Stretching>().lenght = beatlenght;
            temp.transform.parent = this.transform;
        }


	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(0, 0, -sideDistance);
	}
}
