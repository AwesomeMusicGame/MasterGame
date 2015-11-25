using UnityEngine;
using System.Collections;

public abstract class PrekazkaBase : MonoBehaviour
{

    private Kolajnice kolajnice;
    public abstract void Kill(); // alias get rekt

    // Use this for initialization
    void Start()
    {
        kolajnice = GameObject.FindGameObjectWithTag("KolajniceTag").GetComponent<Kolajnice>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            kolajnice.GameOver = true;
            other.gameObject.GetComponentInChildren<PlayerAnimation>().StartDeadAnimation();
        }
    }
}
