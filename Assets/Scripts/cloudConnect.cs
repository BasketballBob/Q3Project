using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class cloudConnect : MonoBehaviour
{
    //Cloud Connect Variables
    public cloudData webData;
    public int ROW_ID;
    public string ReturnString;

    //Initialization
    public void Start()
    {
        webData = new cloudData();
        //webData.ROW = ROW_ID;
        /*CloudData.BaseUrl = "http://vgdapi.basmati.org";
        CloudData.GROUPID = "DUMMY";
        CloudData.GROUPPW = "derf";
        CloudData.PlayerID = 1;*/

        TestConnection(); //Should Return "OK"
    }

    public void TestConnection()
    {
        StartCoroutine(CDTestConn());
    }

    public void PullRow(int row)
    {
        StartCoroutine(CDGetRow(row));
    }
    
    public void PushRow(int row)
    {
        StartCoroutine(CDPushRow(row));
    }

    IEnumerator CDTestConn()
    {
        UnityWebRequest www = UnityWebRequest.Get(cloudData.baseURL);
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log("CONNECTION TEST ERROR");
        }
        else
        {
            //Show Results As Text
            string getText = www.downloadHandler.text;
            ReturnString = getText;

            //Push Results Into Debug
            Debug.Log(getText);
        }
    }

    IEnumerator CDGetRow(int r)
    {
        string addedURL = "/getrow.php?groupid=" + cloudData.groupID + "&row" + r.ToString();

        UnityWebRequest www = UnityWebRequest.Get(cloudData.baseURL + addedURL);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("CONNECTION GET ROW ERROR");
        }
        else
        {
            //Show Results As Text
            webData.pullComplete = false;
            string getText = www.downloadHandler.text;
            ReturnString = getText;
            webData.populateFromString(getText);
            webData.pullComplete = true;
            webData.pullPending = false;

            //"Now Perform Desired Action..."

        }
    }

    IEnumerator CDPushRow(int r)
    {
        string pushData = webData.preparePushData();
        string addedURL = "/modrow.php?groupid=" + cloudData.groupID 
        + "&grouppw=" + cloudData.groupPW + "&row=" + r.ToString() + pushData;
        UnityWebRequest www = UnityWebRequest.Get(cloudData.baseURL + addedURL);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("CONNECTION PUSH ROW ERROR");
        }
        else
        {
            webData.pushComplete = false;
            string getText = www.downloadHandler.text;
            ReturnString = getText;
            webData.pushComplete = true;
        }
    }
}
