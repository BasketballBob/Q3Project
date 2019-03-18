using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class cloudManager : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////
   
    ///////////////////////////////////////////////////////////////
    
    //DO NOT USE THIS (HAS BEEN TAKEN OUT IN FAVOR OF JIM'S SYSTEM)

    ///////////////////////////////////////////////////////////////
    
    ///////////////////////////////////////////////////////////////

    //URL Variables 
    const string baseURL = "http://vgdapi.basmati.org/";
    const string groupID = "PMQ3T5";
    const string groupPW = "PMQ3T5";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator fred ()
    {
        UnityWebRequest wr = UnityWebRequest.Get(baseURL);
        yield return wr.SendWebRequest();
            
        //Check For Error
        if(wr.isNetworkError || wr.isHttpError)
        {
            Debug.Log("WEB REQUEST TEST ERROR");
            Debug.Log(wr.error);
        }
        //Return Positively If Successful
        else
        {
            //Show Results
            string testText = wr.downloadHandler.text;
            
        }
    }
}
