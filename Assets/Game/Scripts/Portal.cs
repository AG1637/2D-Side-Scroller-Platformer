using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("portal hit");
        SceneManager2.instance.LoadNextLevel();
    }
}
