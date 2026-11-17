using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] [Min(2f)] float increaseSpeedAmount = 3f;
    LevelGenerator levelGenerator;

    public void Init(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator;
    }
    protected override void OnPickup()
    {
        levelGenerator.ChangeChunkMoveSpeed(increaseSpeedAmount);
    }
}
