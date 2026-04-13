using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    [SerializeField] private float standTime = 2f;  
    [SerializeField] private float reappearTime = 2f;   
    [SerializeField] private float fadeDuration = 0.5f; 

    private BoxCollider platformCollider;
    private Renderer platformRenderer;
    private Rigidbody rb;
    private Material material;
    private float timePlayerStood = 0f;
    private float disappearTimer = 0f;
    private float fadeTimer = 0f;
    private bool hasPlatformDisappeared = false;
    private bool isPlayerOnPlatform = false;
    private bool isFading = false;
    private bool isFadingOut = false;

    private void Start()
    {
        platformCollider = GetComponent<BoxCollider>();
        platformRenderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        material = platformRenderer.material;

        platformCollider.isTrigger = false;
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            isPlayerOnPlatform = true;
            timePlayerStood = 0f;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            if (!isPlayerOnPlatform)
            {
                isPlayerOnPlatform = true;
                timePlayerStood = 0f;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            isPlayerOnPlatform = false;
            timePlayerStood = 0f;
        }
    }

    private void Update()
    {
        if (isFading)
        {
            Fade();
        }

        if (isPlayerOnPlatform && !hasPlatformDisappeared && !isFading)
        {
            timePlayerStood += Time.deltaTime;
            if (timePlayerStood >= standTime)
            {
                FadeOut();
            }
        }

        if (hasPlatformDisappeared && !isFading)
        {
            disappearTimer -= Time.deltaTime;

            if (disappearTimer <= 0f)
            {
                FadeIn();
            }
        }
    }

    private void Fade()
    {
        fadeTimer -= Time.deltaTime;

        if (isFadingOut)
        {
            float alpha = fadeTimer / fadeDuration;
            SetAlpha(alpha);

            if (fadeTimer <= 0f)
            {
                isFading = false;
                platformCollider.enabled = false;
                SetAlpha(0f);
                hasPlatformDisappeared = true;
                disappearTimer = reappearTime;
                isPlayerOnPlatform = false;
            }
        }
        else
        {
            float alpha = 1f - (fadeTimer / fadeDuration);
            SetAlpha(alpha);

            if (fadeTimer <= 0f)
            {
                isFading = false;
                platformCollider.enabled = true;
                SetAlpha(1f);
                hasPlatformDisappeared = false;
                timePlayerStood = 0f;
            }
        }
    }

    private void SetAlpha(float alpha)
    {
        Color color = material.color;
        color.a = Mathf.Clamp01(alpha);
        material.color = color;
    }
    
    private void FadeOut()
    {
        isFading = true;
        isFadingOut = true;
        fadeTimer = fadeDuration;
    }

    private void FadeIn()
    {
        isFading = true;
        isFadingOut = false;
        fadeTimer = fadeDuration;
    }
}