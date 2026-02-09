using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    public void startBtn()
    {
        SceneManager.LoadScene("Stage2");
    }
}
