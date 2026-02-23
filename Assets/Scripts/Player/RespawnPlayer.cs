using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    public void GameOver()
    {
        uiManager.GameOver();
    }
}