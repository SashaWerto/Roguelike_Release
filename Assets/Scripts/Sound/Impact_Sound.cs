using UnityEngine;
public class Impact_Sound : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioClip[] impactClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.relativeVelocity.magnitude > 3)
        {
            audioSource.pitch = Random.Range(0.9f, 1.2f);
            audioSource.PlayOneShot(impactClip[Random.Range(0, impactClip.Length)]);
        }
    }
}
