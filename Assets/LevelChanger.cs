using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] Animator animator;
    int loadSceneIndex;
    string sceneName;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    FadeToNextLevel();
        //}
    }

    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DelayedFadeToLevel(int indexLevel, float delay)
    {
        StartCoroutine(DelayedSceneChange(indexLevel, delay));
    }

    IEnumerator DelayedSceneChange(int indexLevel, float delay)
    {
        yield return new WaitForSeconds(delay);
        FadeToLevel(indexLevel);
    }

    public void FadeToLevel(int indexLevel)
    {
        if (indexLevel > SceneManager.sceneCount) return;
        loadSceneIndex = indexLevel;
        sceneName = null;
        animator.SetTrigger("FadeOut");

    }public void FadeToLevel(string sceneNameLevel)
    {
        sceneName = sceneNameLevel;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplite()
    {
        if (sceneName != null) { SceneManager.LoadScene(sceneName); return; }
        SceneManager.LoadScene(loadSceneIndex);
    }
}
