using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    public GameObject previewCamera;

    private void Start()
    {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        PlayerMovement.instance.canMove = false;
        StartCoroutine(Preview());
    }

    IEnumerator Preview()
    {
        yield return new WaitForSeconds(11);
        mainCamera.SetActive(true);
        previewCamera.SetActive(false);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        PlayerMovement.instance.canMove = true;
    }
}
