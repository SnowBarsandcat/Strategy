using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image LoadingBarFill;
    public Text LoadingBarText;

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }
    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operetion = SceneManager.LoadSceneAsync(sceneId);
        operetion.allowSceneActivation = false;
        LoadingScreen.SetActive(true);
        while(!operetion.isDone)
        {
            float progressValue = Mathf.Clamp01(operetion.progress / 0.9f);
            LoadingBarFill.fillAmount = progressValue;
            LoadingBarText.text = (progressValue * 100).ToString("F0") + "%";
            if(operetion.progress >= 0.9f)
            {
                LoadingBarText.text = "Press SpaceBar to Continue";
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    operetion.allowSceneActivation = true;
                }
                        
            }
            yield return null;
        }
    }
}
