using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance { get; private set; }
    private AudioSource musicSource;
    [SerializeField] private AudioClip track1;
    [SerializeField] private AudioClip track2;

    private bool playingTrack1 = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        musicSource = GetComponent<AudioSource>();
        musicSource.loop = false;
    }

    private void Start()
    {
        PlayNextTrack();
    }

    private void Update()
    {
        if (!musicSource.isPlaying && musicSource.clip != null)
        {
            PlayNextTrack();
        }
    }

    private void PlayNextTrack()
    {
        if (playingTrack1)
        {
            musicSource.clip = track1;
            playingTrack1 = false;
        }
        else
        {
            musicSource.clip = track2;
            playingTrack1 = true;
        }

        musicSource.Play();
    }
}
