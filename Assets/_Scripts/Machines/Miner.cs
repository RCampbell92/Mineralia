using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Machine
{
    public int mineTime = 60;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void Work()
    {
        if (age % mineTime == 0)
        {
            groundPiece.Mine(this);
        }
    }

    public void DropOre(Item item)
    {

    }
}
