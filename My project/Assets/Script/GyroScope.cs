using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroScope : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
         else
        {
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
