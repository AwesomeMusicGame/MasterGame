using UnityEngine;
using System.Collections;

public class ScenePicker : MonoBehaviour {

	public GameObject[] SceneVariants;
	
	private Kolajnice kolajnice;

	// Use this for initialization
	void Start () {
		kolajnice = GameObject.FindGameObjectWithTag("KolajniceTag").GetComponent<Kolajnice>();

		int Pick = 0;//Random.Range(0, SceneVariants.Length);
        GameObject picked = Instantiate(SceneVariants[Pick], transform.position, Quaternion.identity) as GameObject;
        picked.transform.parent = this.transform;

		kolajnice.kockaPrefab
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
