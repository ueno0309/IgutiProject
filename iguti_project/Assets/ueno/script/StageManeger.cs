using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Stage4 Flags")]
    public bool canCreateGoal = false;
    public bool hasExitDoor = false;

    private void Awake()
    {
        Instance = this;
    }

    public void ResetFlags()
    {
        canCreateGoal = false;
        hasExitDoor = false;
    }
}
