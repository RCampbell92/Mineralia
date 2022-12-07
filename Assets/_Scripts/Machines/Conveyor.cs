using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : Machine
{
    List<PhysicalItemStack> items;
    string direction = "right";
    float dx = 0.0f, dy = 0.0f;
    Chest chestIn, chestOut;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        UpdateDirection();
    }

    void Update()
    {
        base.Update();
    }

    protected override void RotateCW()
    {
        base.RotateCW();
        UpdateDirection();
    }

    protected override void RotateACW()
    {
        base.RotateACW();
        UpdateDirection();
    }

    protected override void OnPlace()
    {
        base.OnPlace();

        GetChests();
    }

    public void SetRotation(float rot)
    {
        base.SetRotation(rot);
        UpdateDirection();
    }

    void UpdateDirection()
    {
        if(transform.rotation.eulerAngles.z == 0.0f)
        {
            direction = "right";
            dx = 2.0f;
            dy = 0.0f;
        }
        if (transform.rotation.eulerAngles.z == 90.0f)
        {
            direction = "up";
            dx = 0.0f;
            dy = 2.0f;
        }
        if (transform.rotation.eulerAngles.z == 180.0f)
        {
            direction = "left";
            dx = -2.0f;
            dy = 0.0f;
        }
        if (transform.rotation.eulerAngles.z == 270.0f)
        {
            direction = "down";
            dx = 0.0f;
            dy = -2.0f;
        }
        else
        {
            
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //stop item before moving it
        if (collider.gameObject.layer == 8)
        {
            collider.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (!placing)
        {
            Transform tf = collider.transform;
            if (tf)
            {
                //Set y val to middle
                if (direction.Equals("right"))
                {
                    tf.position = new Vector3(tf.position.x, transform.position.y, -11.0f);

                    //put item into chest if at the end and if possible
                    if (tf.position.x > GetComponent<BoxCollider2D>().bounds.max.x)
                    {
                        if (chestOut)
                        {
                            chestOut.AddItem(tf.GetComponent<PhysicalItemStack>().itemID, tf.GetComponent<PhysicalItemStack>().numItems);
                            Destroy(tf.gameObject);
                        }
                    }
                }
                if (direction.Equals("up"))
                {
                    tf.position = new Vector3(transform.position.x, tf.position.y, -11.0f);

                    //put item into chest if at the end and if possible
                    if (tf.position.y > GetComponent<BoxCollider2D>().bounds.max.y)
                    {
                        if (chestOut)
                        {
                            chestOut.AddItem(tf.GetComponent<PhysicalItemStack>().itemID, tf.GetComponent<PhysicalItemStack>().numItems);
                            Destroy(tf.gameObject);
                        }
                    }
                }
                if (direction.Equals("left"))
                {
                    tf.position = new Vector3(tf.position.x, transform.position.y, -11.0f);
                }
                if (direction.Equals("down"))
                {
                    tf.position = new Vector3(transform.position.x, tf.position.y, -11.0f);
                }

                //Move item along
                if (tf.GetComponent<PhysicalItemStack>())
                {
                    tf.GetComponent<PhysicalItemStack>().SetConveyorVelocity(dx, dy);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 8)
        {
            collider.GetComponent<PhysicalItemStack>().SetConveyorVelocity(0.0f, 0.0f);
        }
    }

    public void GetChests()
    {
        if (direction.Equals("right"))
        {
            Collider[] cols = Physics.OverlapSphere(new Vector3(transform.position.x - 1.0f, transform.position.y, -10.0f), 0.1f);
            foreach (Collider col in cols)
            {
                if (col.name.Contains("Chest"))
                {
                    chestIn = col.GetComponent<Chest>();
                }
            }
            cols = Physics.OverlapSphere(new Vector3(transform.position.x + 1.0f, transform.position.y, -10.0f), 0.1f);
            foreach (Collider col in cols)
            {
                if (col.name.Contains("Chest"))
                {
                    chestOut = col.GetComponent<Chest>();
                }
            }
        }

        if (direction.Equals("up"))
        {
            Collider[] cols = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - 1.0f, -10.0f), 0.1f);
            foreach (Collider col in cols)
            {
                if (col.name.Contains("Chest"))
                {
                    chestIn = col.GetComponent<Chest>();
                }
            }
            cols = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y + 1.0f, -10.0f), 0.1f);
            foreach (Collider col in cols)
            {
                if (col.name.Contains("Chest"))
                {
                    chestOut = col.GetComponent<Chest>();
                }
            }
        }
    }
}
