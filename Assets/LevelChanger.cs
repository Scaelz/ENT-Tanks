using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] Animator animator;
    int loadSceneIndex;
    string sceneName;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if(animator == null)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //FadeToLevel(3);
        }
    }

    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DelayedFadeToLevel(int indexLevel, float delay)
    {
        StartCoroutine(DelayedSceneChange(indexLevel, delay));
    }

    public void DelayedFadeToNextLevel(float delay)
    {
        StartCoroutine(DelayedSceneChange(SceneManager.GetActiveScene().buildIndex + 1, delay));
    }

    IEnumerator DelayedSceneChange(int indexLevel, float delay)
    {
        yield return new WaitForSeconds(delay);
        FadeToLevel(indexLevel);
    }

    public void FadeToLevel(int indexLevel)
    {
        if (indexLevel > SceneManager.sceneCount) return;

        animator.SetTrigger("FadeOut");
        loadSceneIndex = indexLevel;
        sceneName = null;

    }public void FadeToLevel(string sceneNameLevel)
    {
        animator.SetTrigger("FadeOut");
        sceneName = sceneNameLevel;
    }

    public void OnFadeComplite()
    {
        if (sceneName != null) { SceneManager.LoadScene(sceneName); return; }
        SceneManager.LoadScene(loadSceneIndex);
    }
}
