using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanjutBljr : MonoBehaviour
{
    // Start is called before the first frame update
    public void Kembali()
    {
        SceneManager.LoadScene(1);
    }

    public void Selesai()
    {
        SceneManager.LoadScene(0);
    }
}
