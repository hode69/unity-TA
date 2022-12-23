using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GyroScope : MonoBehaviour
{
    public GameObject GyroError, mulai;

    // Start is called before the first frame update
    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            mulai.SetActive(true);
        }
         else
        {
            GyroError.SetActive(true);
            Debug.Log("Gyro is not supported on your Device");
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (SystemInfo.supportsGyroscope && Time.timeScale==1)
        {
            transform.localRotation = GyroToUnity (Input.gyro.attitude);

            //Debug.Log("Gyro Data: " + Input.gyro.attitude);
        }
    }

    private Quaternion GyroToUnity (Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
