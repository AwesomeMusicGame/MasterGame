using UnityEngine;
using System.Collections;

public class Vizualizer : MonoBehaviour {

	// C#
	// Instantiates a prefab in a circle
	
	public GameObject prefab;
	public int numberOfObjects = 20;
	public float radius = 5f;
	public GameObject[] cubes;
	
	void Start() {
		for (int i = 0; i < numberOfObjects; i++) {
			if((i < numberOfObjects /2 || i > numberOfObjects/2) && 
			   (i < (numberOfObjects /2)-1 || i > (numberOfObjects/2)+1) &&
			   (i < (numberOfObjects /2)-2 || i > (numberOfObjects/2)+2) &&
			   (i < (numberOfObjects /2)-3 || i > (numberOfObjects/2)+3))
			{
				float angle = i * Mathf.PI * 1 / numberOfObjects;
				Vector3 pos = new Vector3(Mathf.Sin(angle)+0.2f, 0, Mathf.Cos(angle)*Mathf.PI/8) * radius;
				Instantiate(prefab, pos, Quaternion.identity);
			}
		}
		cubes = GameObject.FindGameObjectsWithTag("Cubes");
	}

	void Update()
	{
		float[] spectrum = AudioListener.GetSpectrumData (1024, 0, FFTWindow.Hamming);
		for (int i = 0; i < numberOfObjects-7; i++) {
			Vector3 previousScale = cubes [i].transform.localScale;
			previousScale.y = spectrum [i] * 5;
			cubes [i].transform.localScale = previousScale;
		}
	}
}
