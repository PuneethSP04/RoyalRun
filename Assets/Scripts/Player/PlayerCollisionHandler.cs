using System;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] float collisionCooldown = 1f;
    [SerializeField] float reduceSpeedAmount = -2f;
    [SerializeField] AudioClip hitAudioClip;
    private Animator playerAnim;
    private AudioSource playerAudioSrc;
    LevelGenerator levelGenerator;
    private static readonly int HitTrigger = Animator.StringToHash("Hit");
    float cooldownTimer = 0f;
    void Awake()
    {
        playerAnim = GetComponentInChildren<Animator>();
        playerAudioSrc = GetComponent<AudioSource>();
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (cooldownTimer <= collisionCooldown) return;
        playerAnim.SetTrigger(HitTrigger);
        playerAudioSrc.PlayOneShot(hitAudioClip);
        levelGenerator.ChangeChunkMoveSpeed(reduceSpeedAmount);
        cooldownTimer = 0f;
    }
}
