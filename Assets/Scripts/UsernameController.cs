using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UsernameController : MonoBehaviour
{
    public Text playerUsername;
    
    private string username;
    private string score;
    private string scoreboardsAPI = "//134.209.97.218:5051/scoreboards/13517088";
    private Func<string> GetPlayerScore;
    private int mainMenuSceneIdx;

    public void OnEnterClick() {
        username = playerUsername.text;
        score = GetPlayerScore();
        StartCoroutine(PostScore());

        SceneManager.LoadSceneAsync(mainMenuSceneIdx);
    }

    public void Setup(Func<string> getPlayerScore, int mainMenuSceneIdx) {
        this.GetPlayerScore = getPlayerScore;
        this.mainMenuSceneIdx = mainMenuSceneIdx;
    }
    
    private IEnumerator PostScore() {
        WWWForm formData = new WWWForm();
        formData.AddField("username", this.username);
        formData.AddField("score", this.score);

        using (UnityWebRequest www = UnityWebRequest.Post(scoreboardsAPI, formData)) {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
            } else {
                Debug.Log("Score Posting Complete");
            }
        }
    }


}
