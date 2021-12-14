using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loading : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public IEnumerator LoadingScene(string sceneToLoad)
    {  
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!loadingOperation.isDone)
        {
            yield return null;
        }
    }

    public void StartLoading(string sceneToLoad)
    {
        StartCoroutine(LoadingScene(sceneToLoad));
    }

    void Update()
    {
    }
}
