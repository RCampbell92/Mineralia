using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //Testing file modification

    public GroundPiece activeGroundPiece;
    public Text txtDepth;

    [Header("Icons")]
    public static Material matIron;

    [Header("Lists")]
    public static List<Machine> machines;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 8); //between items
        Physics2D.IgnoreLayerCollision(10, 10); //between conveyors
        Physics2D.IgnoreLayerCollision(8, 9); //between items and machines
        Physics2D.IgnoreLayerCollision(10, 11); //between conveyors and ground pieces
        Physics2D.IgnoreLayerCollision(8, 11); //between items and ground pieces
        matIron = Resources.Load<Material>("Materials/IronOre");

        machines = new List<Machine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeGroundPiece)
        {
            txtDepth.text = "Depth: " + activeGroundPiece.holeDepth + "m";
        }
        else
        {
            txtDepth.text = "";
        }
    }
}
