using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private AudioClip levelCompleteSound;
    private void OnTriggerEnter(Collider other)
    {
        SoundManager.instance.PlaySound(levelCompleteSound);
        Invoke("LoadLevel", 2f);
    }

    public void LoadLevel()
    {
        SceneManager2.instance.LoadNextLevel();
    }
}
