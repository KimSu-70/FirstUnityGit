using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    Coroutine loadingRoutine;
    [SerializeField] Image loadingImage;
    [SerializeField] Slider loadingBar;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void ChangeScene(string sceneName)
    {
        // SceneManager.LoadScene(sceneName);  동기식
        if (loadingRoutine != null)
            return;
        loadingRoutine = StartCoroutine(LoadingRoutin(sceneName));
    }

    IEnumerator LoadingRoutin(string sceneName)
    {
        AsyncOperation oper = SceneManager.LoadSceneAsync(sceneName); // 비동기식

        oper.allowSceneActivation = false;
        loadingImage.gameObject.SetActive(true);

        while (oper.isDone == false)
        {
            if (oper.progress < 0.9f)
            {
                // 로딩 중....
                Debug.Log($"loading = {oper.progress}");
                loadingBar.value = oper.progress;
            }
            else
            {
                // 로딩 완료!
                Debug.Log("loading suceess");
                if (Input.anyKeyDown)
                {
                    oper.allowSceneActivation = true;
                    loadingImage.gameObject.SetActive(false);
                }
            }
            yield return null;
        }
    }
}
