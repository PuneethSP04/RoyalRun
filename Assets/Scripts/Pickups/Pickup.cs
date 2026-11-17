using System;
using UnityEngine;
using DG.Tweening;

public abstract class Pickup : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed =100f;

    [Header("Pickup Animation")]
    [SerializeField] private float floatDistance = 2f;
    [SerializeField] private float animationDuration = 0.5f;

    [Header("Audio")]
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] [Range(0,1)] private float soundVolume = 1f; 

    private const String PlayerString = "Player";
    private Collider col;
    private bool isCollected = false;

    void Awake()
    {
        col = GetComponentInChildren<Collider>();
    }

    void Update()
    {
        if(!isCollected)
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);  
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (isCollected || !other.CompareTag(PlayerString))  return;

        isCollected = true;
        if(col != null) col.enabled = false;

        // Play audio independently of this GameObject's lifecycle
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, soundVolume);
        }

        OnPickup();

        PlayPickupAnimation();
    }

    private void PlayPickupAnimation()
    {
        Sequence pickupSeq = DOTween.Sequence();

        // Move up smoothly
        pickupSeq.Join(transform.DOMoveY(transform.position.y + floatDistance, animationDuration).SetEase(Ease.OutQuad));

        // 2. Shrink down to zero simultaneously
        pickupSeq.Join(transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.InBack));

        // 3. Spin faster while rising.
        // FastBeyond360 forces DOTween to complete full revolutions (360°+)
        // instead of taking the shortest math path (which would treat 360° as 0° and do nothing).
        // .SetRelative() adds this rotation onto its current angle.
        pickupSeq.Join(transform.DORotate(new Vector3(0, 360f, 0), animationDuration, RotateMode.FastBeyond360).SetRelative().SetEase(Ease.Linear));

        pickupSeq.OnComplete(() => Destroy(gameObject));
    }

    private void Oestroy()
    {
        // Best practice: kill running tweens on this object if destroyed early
        transform.DOKill();        
    }

    protected abstract void OnPickup();
}
