using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public Camera cam;
    public Player player;
    public TerrainGenerator tGen;
    public Vector2Int tilePos = new Vector2Int(-1, -1);
    public bool placing = false;
    public Color normalColour = Color.white;
    public Color placingColour = new Color(0.4f, 1.0f, 0.4f, 0.5f);

    //adjacent conveyors
    public Conveyor convN, convE, convS, convW;

    protected Vector2Int defaultTilePos = new Vector2Int(-1, -1);
    protected GroundPiece groundPiece;
    protected int age = 0;
    protected Hotbar hotbar;
    protected GameObject deconstructBar;

    // Start is called before the first frame update
    protected void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("Player").GetComponent<Player>();
        tGen = GameObject.Find("TerrainGen").GetComponent<TerrainGenerator>();
        hotbar = GameObject.Find("Hotbar").GetComponent<Hotbar>();
        deconstructBar = GameObject.Find("Deconstruct Bar");
        if (deconstructBar)
        {
            deconstructBar.SetActive(false);
        }
    }

    // Update is called once per frame
    protected void Update()
    {

        if (placing)
        {
            //change colour to placing colour
            if (GetComponent<MeshRenderer>() != null)
            {
                GetComponent<MeshRenderer>().material.color = placingColour;
            }

            //get mouse pos
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;

            //set pos to world pos of mouse
            transform.position = cam.ScreenToWorldPoint(new Vector3(x, y, 7));

            //fix for values around 0
            float offsetX = 0.5f;
            float offsetY = 0.5f;
            if (transform.position.x < 0)
            {
                offsetX = -0.5f;
            }
            if (transform.position.y < 0)
            {
                offsetY = -0.5f;
            }
            transform.position = new Vector3((int)(transform.position.x) + offsetX, (int)(transform.position.y) + offsetY, -10);

            //Q and E to rotate
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RotateACW();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                RotateCW();
            }

        }
        else if (tilePos == defaultTilePos) //if tilePos hasn't changed yet
        {
            //change colour to normal colour
            if (GetComponent<MeshRenderer>() != null)
            {
                GetComponent<MeshRenderer>().material.color = normalColour;
            }

            tilePos = new Vector2Int((int)(transform.position.x), (int)(transform.position.y));
            groundPiece = tGen.GetTile(tilePos.x, tilePos.y);

            OnPlace();
        }
        else
        {
            Work();
        }

        age++;
    }

    protected void OnMouseDown()
    {
        deconstructBar.SetActive(true);
    }

    protected void OnMouseUp()
    {
        deconstructBar.SetActive(false);
    }

    protected void OnMouseDrag()
    {
        
    }

    protected virtual void RotateCW()
    {
        //print(transform.rotation
        transform.Rotate(0, 0, -90.0f);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, (int)(transform.rotation.eulerAngles.z + 0.1f));
        hotbar.machineRotation = transform.eulerAngles.z;
    }

    protected virtual void RotateACW()
    {
        transform.Rotate(0, 0, 90.0f);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, (int)(transform.rotation.eulerAngles.z + 0.1f));
        hotbar.machineRotation = transform.eulerAngles.z;
    }

    protected virtual void OnPlace()
    {

    }

    public void SetRotation(float rot)
    {
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    public virtual void Work()
    {

    }

    public void GetConveyors()
    {
        Collider[] colsN = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y + 1.0f, -10.0f), 0.1f);
        foreach(Collider c in colsN)
        {
            print("Collider found");
            convN = c.GetComponent<Conveyor>();
            if (convN)
            {
                //set conveyor if found
                print("ConvN");
            }
        }
    }
}
