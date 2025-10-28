using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Image loadingBarFill;
    //public float progressValue;
    private void Start()
    {
        loadingScreen.SetActive(false);
    }
    public void LoadScene(string scene)
    {
        Debug.Log("Load Scene Called");
        //StartCoroutine(progressLoad(scene));
    }
    IEnumerator progressLoad(string scene)
    {
        yield return new WaitForSeconds(Random.Range(2, 5));
        Debug.Log("Couroutine Started");
        AsyncOperation load = SceneManager.LoadSceneAsync(scene);
        Debug.Log("Scene Loading");
        loadingScreen.SetActive(true);
        Debug.Log("Loading Screen Active");
        while (!load.isDone)
        {
            float progressValue = Mathf.Clamp01(load.progress / 0.9f);
            //float progressValue = Mathf.Clamp01(0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }
        Debug.Log("Load Complete");
    }
}
