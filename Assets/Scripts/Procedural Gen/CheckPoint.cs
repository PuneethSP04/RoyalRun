using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] float increaseTimeAmount = 5f;

    GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.IncreaseTimer(increaseTimeAmount);
        }
    }
}
