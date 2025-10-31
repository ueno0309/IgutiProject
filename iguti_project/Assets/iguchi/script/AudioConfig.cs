using UnityEngine;

public class AudioConfig : MonoBehaviour
{

    [SerializeField] AudioSource seAudioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            seAudioSource.Play();
        }
    }
}
