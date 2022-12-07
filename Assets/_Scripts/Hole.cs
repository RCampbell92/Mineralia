using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hole : MonoBehaviour
{
    public float depth = 0.0f;
    public Game game;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        depth = Utils.Round(depth, 2);
    }

    public void OnMouseDown()
    {
        depth += 0.01f;
        if(Random.Range(0, 100) < 10)
        {
            player.iron++;
        }
    }
}
