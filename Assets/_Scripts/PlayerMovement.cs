using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        float dx = 0;
        float dy = 0;

        if (Input.GetKey(KeyCode.D))
        {
            dx += Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dx -= Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            dy += Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dy -= Time.deltaTime * speed;
        }

        transform.Translate(dx, dy, 0);
    }
}
