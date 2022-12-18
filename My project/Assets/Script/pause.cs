using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{

    [SerializeField] public GameObject PausePanel;
    // Update is called once per frame
    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
