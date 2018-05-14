using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DBfinalScoreCom: MonoBehaviour
{

    void Start()
    {
        Debug.Log("sending entry");
        StartCoroutine(cur_postRequest("https://us-central1-htcvruis2018.cloudfunctions.net/userentry"));
    }

    IEnumerator cur_postRequest(string url)
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
