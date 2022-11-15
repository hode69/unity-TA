using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextPrevObjAnim : MonoBehaviour
{
    public GameObject[] OBJ;
    int indexOBJ;

    void Start()
    {
        indexOBJ = 0;        
    }

    void Update()
    {
        if (indexOBJ >= 10)
            indexOBJ = 10;

        if (indexOBJ < 0)
            indexOBJ = 0;

        if (indexOBJ == 0)
        {
            OBJ[0].gameObject.SetActive(true);
        }
                 
    }

    public void OBJNext()
    {
        indexOBJ += 1;

        for(int i=0;i<OBJ.Length; i++)
        {
            OBJ[i].gameObject.SetActive(false);
            OBJ[indexOBJ].gameObject.SetActive(true);
        }

        Debug.Log(indexOBJ);
    }

    public void OBJPrevious()
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
