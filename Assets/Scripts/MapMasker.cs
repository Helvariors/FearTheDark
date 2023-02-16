using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMasker : MonoBehaviour
{
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.AddComponent<SpriteMask>();
            child.gameObject.GetComponent<SpriteMask>().sprite = sprite;
        }
    }
}