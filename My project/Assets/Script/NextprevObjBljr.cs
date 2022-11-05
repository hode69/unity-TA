using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextprevObjBljr : MonoBehaviour
{
    public GameObject[] OBJ;
    int indexOBJ;

    void Start()
    {
        indexOBJ = 0;        
    }

    void Update()
    {
        if (indexOBJ >= 7)
            indexOBJ = 7;

        if (indexOBJ < 0)
            indexOBJ = 0;

        if (indexOBJ == 0)
        {
            OBJ[0].gameObject.SetActive(true);
        }
                 
    }

    public void Next()
    {
        indexOBJ += 1;

        for(int i=0;i<OBJ.Length; i++)
        {
            OBJ[i].gameObject.SetActive(false);
            OBJ[indexOBJ].gameObject.SetActive(true);
        }

        Debug.Log(indexOBJ);
    }

    public void Previous()
    {
        indexOBJ -= 1;

        for(int i=0;i<OBJ.Length;i++)
        {
            OBJ[i].gameObject.SetActive(false);
            OBJ[indexOBJ].gameObject.SetActive(true);
        }

        Debug.Log(indexOBJ);
    }
}
