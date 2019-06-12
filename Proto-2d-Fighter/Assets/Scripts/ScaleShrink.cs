using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleShrink : MonoBehaviour {

    public KeyCode fGuageed;
    public SpriteRenderer fGuage;
    private GameObject curGuage;
    public float curCologG = 255;

    // Use this for initialization
    void Start () {
        curGuage = GameObject.Find("CurFuel");
        fGuage = curGuage.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update () {
        ShrinkDown();

    }

    private void ShrinkDown()
    {
        if (Input.GetKeyDown(fGuageed))
        {

            //This scales the fuel gauge down with every time it is clicked.
            fGuage.transform.localScale = new Vector3(fGuage.transform.localScale.x, fGuage.transform.localScale.y - .1f, fGuage.transform.localScale.z);
            curCologG -= 25;
            Debug.Log(curCologG);
            fGuage.color = new Color(1, curCologG / 255,0);
            Debug.Log(fGuage.color);
            Debug.Log(fGuage.color.r);
            Debug.Log(fGuage.color.g);
            Debug.Log(fGuage.color.b);

            //SpriteRenderer spTemp = new SpriteRenderer();
            //spTemp.color.r = 
            //spTemp.color.r.Equals(curCologG);
            ////fGuage
            ////spTemp.color.r = 255;
            ////spTemp.color.b = 0;
            ////spTemp.color.a = 255;


            //fGuage.GetComponent<SpriteRenderer>().color = spTemp.color;
            //fGuage.transform.localPosition = new Vector3(fGuage.transform.localPosition.x, fGuage.transform.localPosition.y - .15f, fGuage.transform.localPosition.z);


        }
    }

}
