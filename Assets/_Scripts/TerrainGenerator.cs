using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject groundPiece;
    public GameObject level;

    [Header("Map Settings")]
    public int width = 10;
    public int height = 10;
    public float perlinSize = 0.5f;

    [Header("Textures")]
    public Material matDesert;
    public Material matGrass;

    private GroundPiece[][] tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new GroundPiece[width][];

        for (int i = 0; i < width; i++)
        {
            tiles[i] = new GroundPiece[height];
            for (int j = 0; j < height; j++)
            {
                GameObject gp = Instantiate(groundPiece, new Vector3(i + 0.5f, j + 0.5f, -5.0f), transform.rotation, level.transform);
                if (Mathf.PerlinNoise((float)i*perlinSize, (float)j*perlinSize) < 0.5f)
                {
                    gp.GetComponent<MeshRenderer>().material = matGrass;
                }

                tiles[i][j] = gp.GetComponent<GroundPiece>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GroundPiece GetTile(int x, int y)
    {
        return tiles[x][y];
    }
}
