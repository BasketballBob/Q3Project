using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    //Global Variables
    public static float roomWidth = 14;
    public static float roomHeight = 10;

    //Grid Variables
    public const int gridWidth = 10;
    public const int gridHeight = 10;
    public static int[,] gridArray = new int[gridWidth, gridHeight];
    public static Vector2 gridPos = new Vector2(10, 10);
    public static float gridDimensions = 10; //Dimensions For Grid Squares
    public static float gridBevel = 10;
    public static int[,] gameGrid = new int[10, 10];
    public static Color defaultSquare = new Color(.8f, .8f, .8f);
    public static Color team1Square = new Color(.8f, .8f, .8f);
    public static Color team2Square = new Color(.8f, .8f, .8f);
    public const int colorCount = 3;

    //Rectangle Drawing Variables
    public static Color prevColor;
    static GUIStyle rectStyle = new GUIStyle();
    static Texture2D rectTexture = new Texture2D(1, 1);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Draw Game GUI
    private void OnGUI()
    {
        drawRect(new Rect(10, 10, 300, 300), new Color(1, 0, 0));
    }

    public static void drawRect(Rect inputRect, Color rectColor)
    {
        //Initialize Textures If Necessary
        if(rectColor != prevColor)
        {
            rectTexture.SetPixel(0, 0, rectColor);
            rectTexture.Apply();
            rectStyle.normal.background = rectTexture;

            prevColor = rectColor;
        }

        //Actually Draw Rectangle
        GUI.Box(inputRect, GUIContent.none, rectStyle);
    }

    public static void goToRoom(int x, int y)
    {
        //Set Player Previous Grid Position
        player.prevGridPosX = player.gridPosX;
        player.prevGridPosY = player.gridPosY;
        
        //Set Player Grid Position
        player.gridPosX = x;
        player.gridPosY = y;

        //Set Player Room Position
        float spawnOffSet = 0f;
        if(player.gridPosY < player.prevGridPosY) //UP 
        {
            player.spawnPos = new Vector2(0, -(roomHeight/2-spawnOffSet));
        }
        else if (player.gridPosY > player.prevGridPosY) //DOWN
        {
            player.spawnPos = new Vector2(0, (roomHeight/2-spawnOffSet));
        }
        else if(player.gridPosX > player.prevGridPosX) //RIGHT
        {
            player.spawnPos = new Vector2(-(roomWidth/2-spawnOffSet), 0);
        }
        else if (player.gridPosX < player.prevGridPosX) //RIGHT
        {
            player.spawnPos = new Vector2((roomWidth/2-spawnOffSet), 0);
        }
        else player.spawnPos = new Vector2(0, 0);

        //Restart Room 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //Fill Room With Obstacles
    }

    public static void drawGrid() //(Use In OnGUI)
    {
        //Draw All Colored Squares (Draw One Set Of Colors At A Time)
        for (int i = 0; i < colorCount-1;)
        {
            //Determine Draw Color 
            Color drawColor = new Color(0f, 0f, 0f);
            if (i == 0) drawColor = defaultSquare;
            else if (i == 1) drawColor = team1Square;
            else if (i == 2) drawColor = team2Square;

            //Check All Grid Vals
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    //Determine Draw Color
                    //Color drawColor = new Color(.5f, .5f, .5f);

                    //Only Draw Square  
                    if()

                        //Set Draw Opacity
                        if (x == player.gridPosX && y == player.gridPosY)
                        {
                            drawColor = new Color(drawColor.r, drawColor.g, drawColor.b, 1f);
                        }
                        else drawColor = new Color(drawColor.r, drawColor.g, drawColor.b, .25f);

                        //Draw Rectangle
                        drawRect(new Rect(gridPos.x + gridBevel * x + gridDimensions * (x), gridPos.y + gridBevel * y + gridDimensions * (y), gridDimensions, gridDimensions), drawColor);
                }
            }
        }
    }
}
