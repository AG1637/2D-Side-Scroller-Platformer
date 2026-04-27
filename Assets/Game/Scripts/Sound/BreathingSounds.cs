using UnityEngine;

public class BreathingSounds : MonoBehaviour
{
    [SerializeField] private AudioClip breathingClip;
    private AudioSource breathingSource;

    private void Awake()
    {
        breathingSource = gameObject.AddComponent<AudioSource>();
        breathingSource.loop = true;
    }
    private void Start()
    {
        if (breathingClip != null)
        {
            breathingSource.clip = breathingClip;
            breathingSource.Play();
        }
    }
}
