using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DBcurScoreCom: MonoBehaviour
{
    public void addScore(int score)
    {
        Debug.Log("sending entry");
        StartCoroutine(postRequest("https://us-central1-htcvruis2018.cloudfunctions.net/cur_userentry", score));
    }

    IEnumerator postRequest(string url, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", GlobalVariables.name);
        form.AddField("score", GlobalVariables.score);

        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }
}
