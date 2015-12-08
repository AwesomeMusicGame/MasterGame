using UnityEngine;
using System.Collections;

public class notaSetter : MonoBehaviour {

    public Material cerna;

    public void SetColor(Color c)
    {
        cerna.color = c;
    }
}
