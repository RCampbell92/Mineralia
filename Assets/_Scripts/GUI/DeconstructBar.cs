using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeconstructBar : MonoBehaviour
{
    GameObject centre;

    // Start is called before the first frame update
    void Start()
    {
        centre = transform.Find("Centre").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GrowBar()
    {
        if (centre.transform.localScale.x < 1.0f)
        {
            centre.transform.localScale = new Vector3(centre.transform.localScale.x + 0.05f, centre.transform.localScale.y, centre.transform.localScale.z);
        }
    }
}
