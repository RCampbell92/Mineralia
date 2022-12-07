using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public Game game;
    public float speed = 1.0f; //1m/s
    public float maxHeight = 1.0f;
    public float hoverTime = 0.5f;

    Vector3 startPos;
    float hoverTimer = 0.0f;
    GameObject image;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x + (GetComponent<RectTransform>().rect.size.x/2) * 0.06f, transform.position.y, -5);
        startPos = transform.position;
        //Show(Game.matIron, "+1");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < startPos.y + maxHeight)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            if (hoverTimer < hoverTime)
            {
                hoverTimer += Time.deltaTime;
            }
            else
            {
                GameObject.Destroy(gameObject);
            }
        }
    }

    public void Show(Material mat, string text)
    {
        image = transform.Find("Icon").gameObject;
        image.GetComponent<MeshRenderer>().material = mat;
        GetComponent<TMPro.TextMeshPro>().text = text;
    }
}
