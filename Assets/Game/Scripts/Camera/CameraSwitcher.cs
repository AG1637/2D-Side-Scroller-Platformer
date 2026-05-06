using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    public GameObject previewCamera;
    public int previewDuration;
    public Button skipButton;

    private Coroutine previewCoroutine;

    private void Start()
    {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        PlayerMovement.instance.canMove = false;

        if (skipButton != null)
        {
            skipButton.gameObject.SetActive(true);
            skipButton.onClick.AddListener(SkipPreview);
        }

        previewCoroutine = StartCoroutine(Preview());
    }

    IEnumerator Preview()
    {
        yield return new WaitForSeconds(previewDuration);
        EndPreview();
    }

    public void SkipPreview()
    {
        if (previewCoroutine != null)
        {
            StopCoroutine(previewCoroutine);
        }
        EndPreview();
    }

    private void EndPreview()
    {
        mainCamera.SetActive(true);
        previewCamera.SetActive(false);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        PlayerMovement.instance.canMove = true;
        GameManager.instance.StartTimer();

        if (skipButton != null)
        {
            skipButton.gameObject.SetActive(false);
        }
    }
}

