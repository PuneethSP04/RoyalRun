using UnityEngine;
using Unity.Cinemachine;

public class Rock : MonoBehaviour
{
    [SerializeField] float shakeModifier = 8f;
    [SerializeField] float collisionCoolsdownTimer = 1f;
    [SerializeField] ParticleSystem collsionParticleSystem;
    [SerializeField] AudioSource audioSource;
    CinemachineImpulseSource cinemachineImpulseSource;

    float collisionTimer = 0f;

    void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        collisionTimer +=Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        if (collisionTimer < collisionCoolsdownTimer) return;
        FireImpulse();
        CollisionFX(other);
        collisionTimer = 0f;
    }

    void FireImpulse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f / distance) * shakeModifier;
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);
        cinemachineImpulseSource.GenerateImpulse();
    }

    void CollisionFX(Collision other)
    {
        ContactPoint contactPoint = other.contacts[0];
        collsionParticleSystem.transform.position = contactPoint.point;
        collsionParticleSystem.Play();
        audioSource.Play();
    }

}
