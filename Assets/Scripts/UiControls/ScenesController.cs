using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    public Animator animator;
    private int index;  // Scene to load.

    public void LoadScene(int sceneIndex)
    {
        index = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void LoadNextScene()
    {
        index = SceneManager.GetActiveScene().buildIndex + 1;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeCompleted()
    {
        SceneManager.LoadScene(index);
    }
}
