﻿using UnityEngine;
using System.Collections;

public class ClassicVisual : MonoBehaviour, ISceneItem
{
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
            return Color.black;
        }
    }
    public Color noteColor
    {
        get
        {
            return Color.black;
        }
    }
    public Color countInFontColor
    {
        get
        {
            return Color.black;
        }
    }
    //ISceneItem

    // C#
    // Instantiates a prefab in a circle

    public GameObject prefab;
    public int numberOfObjects = 20;
    public float radius = 5f;
    private GameObject[] cubes;

    void Start()
    {
        ParticleSystem jolo = this.GetComponent<ParticleSystem>(); 
        for (int i = 0; i < numberOfObjects; i++)
        {
            if ((i < numberOfObjects / 2 || i > numberOfObjects / 2) &&
               (i < (numberOfObjects / 2) - 1 || i > (numberOfObjects / 2) + 1) &&
               (i < (numberOfObjects / 2) - 2 || i > (numberOfObjects / 2) + 2) &&
               (i < (numberOfObjects / 2) - 3 || i > (numberOfObjects / 2) + 3))
            {
                float angle = i * Mathf.PI * 1 / numberOfObjects;
                Vector3 pos = new Vector3(Mathf.Sin(angle) + 0.2f, 0, Mathf.Cos(angle) * Mathf.PI / 8) * radius;
                GameObject temp = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
                temp.transform.parent = this.transform;
            }
        }
        cubes = GameObject.FindGameObjectsWithTag("Cubes");
    }

    void Update()
    {
        float[] spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);
        for (int i = 0; i < numberOfObjects - 7; i++)
        {
            Vector3 previousScale = cubes[i].transform.localScale;
            previousScale.y = spectrum[i] * 5;
            cubes[i].transform.localScale = previousScale;
        }
    }
}
