using UnityEngine;

public class PictogramPickup1 : MonoBehaviour
{
    private PictogramBase1 pictogram;

    private void Awake()
    {
        pictogram = GetComponent<PictogramBase1>();
    }

    private void OnMouseDown()
    {
        pictogram.OnPickup();
    }
}
