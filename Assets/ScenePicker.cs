using UnityEngine;
using System.Collections;

public class ScenePicker : MonoBehaviour {

    public GameObject[] SceneVariants;

	// Use this for initialization
	void Start () {
        int Pick = Random.Range(0, SceneVariants.Length);
        GameObject temp = Instantiate(SceneVariants[Pick], transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
