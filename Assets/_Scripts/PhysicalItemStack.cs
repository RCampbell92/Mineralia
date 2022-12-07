using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalItemStack : ItemStack
{
    public float throwSpeed = 2.0f;
    public Vector3 conveyorVelocity = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        UpdateItem();
    }

    // Update is called once per frame
    void Update()
    {
        //move along conveyor
        transform.position += conveyorVelocity * Time.deltaTime;
    }

    public void Throw()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-throwSpeed, throwSpeed), Random.Range(-throwSpeed, throwSpeed)) * Time.deltaTime;
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(2.0f, 2.0f));
    }

    public void SetConveyorVelocity(float x, float y)
    {
        conveyorVelocity = new Vector3(x, y, 0);
    }
}
