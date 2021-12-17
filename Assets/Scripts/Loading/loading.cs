using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loading : MonoBehaviour
{
    void Awake()
    {
    }

    public IEnumerator LoadingScene(string sceneToLoad)
    {  
        SceneManager.LoadScene(sceneToLoad);
        yield return null;
    }

    public void StartLoading(string sceneToLoad)
    {
        StartCoroutine(LoadingScene(sceneToLoad));
    }

    void Update()
    {
    }
}
