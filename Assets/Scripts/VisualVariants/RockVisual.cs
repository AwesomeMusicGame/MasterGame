using UnityEngine;
using System.Collections;

public class RockVisual : MonoBehaviour, ISceneItem {

    //ISceneItem
    public Material podlaha;
    public Material skybox;
    public GameObject punch_prekazka;
    public GameObject slide_prekazka;
    public Material podlahaMaterial
    {
        get
        {
            return podlaha;
        }
    }
    public Material skyboxMaterial
    {
        get
        {
            return skybox;
        }
    }
    public GameObject prekazkaPunch
    {
        get
        {
            return punch_prekazka;
        }
    }
    public GameObject prekazkaSlide
    {
        get
        {
            return slide_prekazka;
        }
    }
    public Color fontColor
    {
        get
        {
            return Color.red;
        }
    }
    public Color noteColor
    {
        get
        {
            return Color.red;
        }
    }
    public Color countInFontColor
    {
        get
        {
            return Color.red;
        }
    }
    //ISceneItem

    private int sampleRate = 1024;
    private GameObject[] objects; 

    public GameObject obj;
    public int NumberObjects = 64; //should be a power of 2 i think
    public float radius = 1;
    public float maxLenght = 1;

	// Use this for initialization
	void Start () {
        objects = new GameObject[NumberObjects];
        for (int i = 0; i < NumberObjects; i++)
        {
            float angle = i * 360 / NumberObjects;
            Vector3 position = new Vector3(transform.position.x, Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)) * radius;
            objects[i] = Instantiate(obj, position, Quaternion.AngleAxis(90, transform.forward)) as GameObject;
            objects[i].transform.parent = transform;
        }
	}
	
	// Update is called once per frame
    void Update()
    {
        float[] spectrum = AudioListener.GetSpectrumData(sampleRate, 0, FFTWindow.Hamming);

        int bandSize = sampleRate / 2 / NumberObjects;
        for (int i = 0; i < NumberObjects / 2; i++)
        {
            float avg = spectrum[(bandSize / 2) + bandSize * i];
            objects[i].transform.localScale = new Vector3(0.3f, avg * maxLenght, 0.3f);
            objects[NumberObjects - i - 1].transform.localScale = new Vector3(0.5f, avg * maxLenght, 0.5f);
        }
	}
}
