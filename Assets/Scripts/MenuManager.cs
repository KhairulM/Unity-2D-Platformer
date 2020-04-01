using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MenuManager : MonoBehaviour
{
    public int mainSceneIdx;
    public GameObject scoreboardPanel;
    public Font scoreFont;
    public Color scoreColor;
    public int scoreSize;

    private ScoreList scores;
    private string scoreboardsAPI = "//134.209.97.218:5051/scoreboards/13517088";

    public void Start() {
        StartCoroutine(GetScore());
    }

    public void OnPlayButtonClick() {
        Debug.Log("PLAY");
        SceneManager.LoadScene(mainSceneIdx);
    }

    public void OnQuitButtonClick() {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void OnSoundToggle(bool state) {
        Debug.Log("TOGGLE SOUND");

        if (PlayerPrefs.GetInt("sounds", 0) == 0) {
            PlayerPrefs.SetInt("sounds", 1);
        } else {
            PlayerPrefs.SetInt("sounds", 0);
        }
        

        Debug.Log(PlayerPrefs.GetInt("sounds"));
        
    }

    private IEnumerator GetScore() {
        using (UnityWebRequest www = UnityWebRequest.Get(scoreboardsAPI)) {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
            } else {
                Debug.Log("SUCCESS GETTING SCORES : " + www.downloadHandler.text);
                string received = www.downloadHandler.text;
                int count = 0;

                scores = JsonUtility.FromJson<ScoreList>("{\"scores\":" + received + "}");


                for (int i = 0; i < scores.scores.Length && count < 5; i++) {
                    GameObject scoreboardEl = new GameObject("score " + (i+1).ToString());
                    scoreboardEl.transform.SetParent(scoreboardPanel.transform);

                    Text scoreText = scoreboardEl.AddComponent<Text>();
                    string username = scores.scores[i].username;
                    string score = scores.scores[i].score;

                    // sanity check for null usernames or scores
                    if (username == "" || score == "") {
                        continue;
                    }
                    count++;

                    scoreText.text = username + " " + score;
                    scoreText.alignment = TextAnchor.UpperCenter;
                    scoreText.font = scoreFont;
                    scoreText.color = scoreColor;
                    scoreText.fontSize = scoreSize;

                    // attach text gameobject to scoreboard panel
                    RectTransform rect = scoreboardEl.GetComponent<RectTransform>();
                    rect.anchorMax = new Vector2(1, 1);
                    rect.anchorMin = new Vector2(0, 0);
                    // set right and top
                    rect.offsetMax = new Vector2(0, -((count-1)*16));
                    rect.offsetMin = new Vector2(0, 0);
                }
            }
        } 
    }
}

[Serializable]
public class Score{
    public string _id;
    public string nim;
    public string username;
    public string score;
}

[Serializable]
public class ScoreList{
    public Score[] scores;
}
