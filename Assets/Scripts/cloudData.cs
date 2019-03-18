using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudData
{
    //Static Variables
    public const string baseURL = "http://vgdapi.basmati.org/";
    public const string groupID = "PMQ3T5";
    public const string groupPW = "PMQ3T5";

    //Database Variables
    public int ROW;
    public float x1, y1, z1;
    public float x2, y2, z2;
    public int i1, i2, i3;
    public float f1, f2, f3;
    public string s1, s2, s3;

    //"Housekeeping Items"
    public bool RequiresUpdate;
    public bool pullPending;
    public bool pushPending;
    public bool pullComplete;
    public bool pushComplete;

    public cloudData(int ROW, float x1, float y1, float z1, float x2, float y2, float z2, int i1, int i2, int i3, float f1, float f2, float f3, string s1, string s2, string s3)
    {
        this.ROW = ROW;
        this.x1 = x1;
        this.y1 = y1;
        this.z1 = z1;
        this.x2 = x2;
        this.y2 = y2;
        this.z2 = z2;
        this.i1 = i1;
        this.i2 = i2;
        this.i3 = i3;
        this.f1 = f1;
        this.f2 = f2;
        this.f3 = f3;
        this.s1 = s1;
        this.s2 = s2;
        this.s3 = s3;
    }

    public cloudData()
    {
        pullPending = false;
        pushPending = false;
        pullComplete = false;
        pushComplete = false;
    }

    public void populateFromString(string data)//HAVE JIM LOOK////////////////////////////////////////////////////////////////
    {
        //Create Data Array From String
        string[] dataArray = data.Split(',');

        //Assign Data Based On Pulled String 
        x1 = float.Parse(dataArray[1]);
        y1 = float.Parse(dataArray[2]);

        x2 = float.Parse(dataArray[4]);
        y2 = float.Parse(dataArray[5]);
        z2 = float.Parse(dataArray[6]);

        i1 = int.Parse(dataArray[7]);
        i2 = int.Parse(dataArray[8]);
        i3 = int.Parse(dataArray[9]);

        f1 = float.Parse(dataArray[10]);
        f2 = float.Parse(dataArray[11]);
        f3 = float.Parse(dataArray[12]);

        s1 = dataArray[13];
        s2 = dataArray[14];
        s3 = dataArray[15];
    }

    //NOTE: PUT ALL PULLING AND SETTING FUNCTIONS 
    //(FOR LOCAL DATA) HERE
    public string preparePushData()
    {
        //Purpose Of Data Values
        //s3 - Grid Array (100 values)


        //Package Grid Data
        string gridData = "";
        for(int x  = 0;x < gameManager.gridWidth;x++)
        {
            for(int y = 0;y < gameManager.gridHeight;y++)
            {
                gridData += gameManager.gridArray[x, y].ToString()+".";
            }
        }


        //Prepare 
        //Put Together Final String (To Upload)
        string data = "";
        data += "&x1=" + x1 + "&y1" + y1 + "&z1" + z1;
        data += "&x2=" + x2 + "&y2" + y2 + "&z2" + z2;
        data += "&i1=" + i1 + "&i2" + i2 + "&i3" + i3;
        data += "&f1=" + f1 + "&f2=" + f2 + "&f3=" + f3;
        data += "&s1=" + s1 + "&s2=" + s2 + "&s3=" + gridData;

        return data;
    }

}
