using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextprevTxtBljr : MonoBehaviour
{
    public GameObject[] TXT;
    int indexTXT;

    void Start()
    {
        indexTXT = 0;        
    }

    void Update()
    {
        if (indexTXT >= 7)
            indexTXT = 7;

        if (indexTXT < 0)
            indexTXT = 0;

        if (indexTXT == 0)
        {
            TXT[0].gameObject.SetActive(true);
        }
                 
    }

    public void Next()
    {
        indexTXT += 1;

        for(int i=0;i<TXT.Length; i++)
        {
            TXT[i].gameObject.SetActive(false);
            TXT[indexTXT].gameObject.SetActive(true);
        }

        Debug.Log(indexTXT);
    }

    public void Previous()
    {
        indexTXT -= 1;

        for(int i=0;i<TXT.Length;i++)
        {
            TXT[i].gameObject.SetActive(false);
            TXT[indexTXT].gameObject.SetActive(true);
        }

        Debug.Log(indexTXT);
    }
}
