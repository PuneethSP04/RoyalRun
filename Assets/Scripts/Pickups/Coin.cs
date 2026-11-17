using TMPro;
using UnityEngine;

public class Coin : Pickup
{
    [SerializeField] int rewardAmount = 100;
    ScoreManager scoreManager;
    
    public void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;
    }

    protected override void OnPickup()
    {
        //Debug.Log("Points added 100");
        scoreManager.AddCoins(rewardAmount);
        
    }
}
