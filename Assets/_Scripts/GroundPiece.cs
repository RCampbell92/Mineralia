using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPiece : MonoBehaviour
{
    public float holeDepth = 0.0f;
    public Game game;
    public Player player;
    public GameObject hole;
    public Vector2Int tilePos = new Vector2Int(-1, -1);
    public GameObject popupPrefab, physicalItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        hole = transform.Find("Hole").gameObject;
        tilePos = new Vector2Int((int)(transform.position.x / 0.64), (int)(transform.position.y / 0.64));
        physicalItemPrefab = Resources.Load<GameObject>("Prefabs/Physical Item Stack");
    }

    // Update is called once per frame
    void Update()
    {
        holeDepth = Utils.Round(holeDepth, 2);
    }

    public void OnMouseDown()
    {
        if(hole.activeSelf == false)
        {
            hole.SetActive(true);
        }
        Mine(null);
    }

    public void OnMouseEnter()
    {
        game.activeGroundPiece = this;
    }

    public void OnMouseExit()
    {
        game.activeGroundPiece = null;
    }

    public void Mine(Miner miner, float depth = 0.01f)
    {
        holeDepth += 0.01f;
        if (miner)
        {
            if (Random.Range(0, 100) < 10.0f)
            {
                player.iron++;
                //***MOVE GETITEM FUNCTIONS TO GAME LOAD TIME***
                SpawnMineral(ItemLibrary.GetItem("Iron Ore"), miner);
            }
            if (Random.Range(0, 100) < 1.0f)
            {
                //***MOVE GETITEM FUNCTIONS TO GAME LOAD TIME***
                SpawnMineral(ItemLibrary.GetItem("Gold Ore"), miner);
            }
            if(Random.Range(0, 100) < 0.1f)
            {
                //***MOVE GETITEM FUNCTIONS TO GAME LOAD TIME***
                SpawnMineral(ItemLibrary.GetItem("Uncut Opal"), miner);
            }
        }
    }

    public void SpawnMineral(Item mineral, Miner miner)
    {
        Material mat = Resources.Load<Material>("Materials/Items/" + mineral.name);
        //spawn popup
        GameObject.Instantiate(popupPrefab, transform.position, Quaternion.identity)
                    .GetComponent<Popup>().Show(mat, "+1");
        //spawn mineral item
        GameObject item = Instantiate(physicalItemPrefab, miner.transform.Find("Output TF").position, Quaternion.identity);
        //change sprite to correct image
        item.GetComponent<MeshRenderer>().material = mat;
        //set id of item
        item.GetComponent<PhysicalItemStack>().item = mineral;
        item.GetComponent<PhysicalItemStack>().itemID = mineral.id;
        //throw it away from origin
        item.GetComponent<PhysicalItemStack>().Throw();
    }
}
