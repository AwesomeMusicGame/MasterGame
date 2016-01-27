using UnityEngine;
using System.Collections;

public class Stretching : MonoBehaviour {

    public float lenght = 2;

    private Material mat;
        
    // Use this for initialization
	void Start () {
        this.transform.localScale = new Vector3(lenght, 0.1f, 1f);

        transform.GetChild(0).GetComponent<Renderer>().material.mainTextureScale = new Vector2(lenght / 2, 1);
	}

    public void SetMaterial(Material m)
    {
        //if (GetComponentInChildren<Renderer>() == null)
        //    return;

        mat = m;
        transform.GetChild(0).GetComponent<Renderer>().material = mat;
    }
	
	// Update is called once per frame
	void Update () {
	}
}