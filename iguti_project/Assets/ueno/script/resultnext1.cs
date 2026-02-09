using UnityEngine;
using UnityEngine.SceneManagement;

public class resultnext1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

public void SwitchScene()
    {
        SceneManager.LoadScene("1-3", LoadSceneMode.Single);
    }
    
}
