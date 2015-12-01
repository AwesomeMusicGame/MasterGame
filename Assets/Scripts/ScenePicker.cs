using UnityEngine;
using System.Collections;

public class ScenePicker : MonoBehaviour {
	
	private Kolajnice kolajnice;

	// Use this for initialization
	void Start () {
        //kolajnice = GameObject.FindGameObjectWithTag("KolajniceTag").GetComponent<Kolajnice>();

        //// pick scene, later load from scene loader
        //int Pick = 1;//Random.Range(0, SceneVariants.Length);
        //GameObject picked = Instantiate(SceneVariants[Pick], transform.position, Quaternion.identity) as GameObject;
        //picked.transform.parent = this.transform;

        ////set veci z presetu
        //kolajnice.kockaPrefab.GetComponent<Stretching>().SetMaterial(picked.GetComponent<ISceneItem>().podlahaMaterial);
        //RenderSettings.skybox = picked.GetComponent<ISceneItem>().skyboxMaterial;
        //kolajnice.prekazkaPrefab = new GameObject[2];
        //kolajnice.prekazkaPrefab[0] = picked.GetComponent<ISceneItem>().prekazkaPunch;
        //kolajnice.prekazkaPrefab[1] = picked.GetComponent<ISceneItem>().prekazkaSlide;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
