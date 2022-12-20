using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowManager : MonoBehaviour
{
    public Vector3 positionAwal;
    public Transform ambil; 
    public Transform kembalikan;
    
    void Start()
    {
        positionAwal = this.gameObject.transform.position;
    }

    public void selectObject()
    {
        this.transform.parent = ambil.transform;
        this.transform.position = new Vector3(0, 0, 0);
    }

    public void cancelObject()
    {
        this.transform.parent = kembalikan.transform;
        this.transform.position = positionAwal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
