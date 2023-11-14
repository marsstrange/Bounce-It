using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_GroundGenerator : MonoBehaviour
{
    public Camera mainCamera;
    public Transform startPoint; //Point from where ground tiles will start
    public SC_PlatformTile tilePrefab;
    public float movingSpeed = 12;
    public int tilesToPreSpawn = 15; //How many tiles should be pre-spawned
    public int tilesWithoutObstacles = 3; //How many tiles at the beginning should not have obstacles, good for warm-up
    
    public Camera CameraP1;
    public Camera CameraP2;
    public Camera CameraP3;
    public Camera CameraP4;
    public RenderTexture backTexture; 

    List<SC_PlatformTile> spawnedTiles = new List<SC_PlatformTile>();
    int nextTileToActivate = -1;
    [HideInInspector]
    public bool gameOverPlayer1 = false;
    [HideInInspector]
    public bool gameOverPlayer2 = false;
    [HideInInspector]
    public bool gameOverPlayer3 = false;
    [HideInInspector]
    public bool gameOverPlayer4 = false;
    [HideInInspector]
    static bool gameStarted = false;
    float score = 0;
    float scorePlayer1 = 0;
    float scorePlayer2 = 0;
    float scorePlayer3 = 0;
    float scorePlayer4 = 0;

    public static SC_GroundGenerator instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        Vector3 spawnPosition = startPoint.position;
        int tilesWithNoObstaclesTmp = tilesWithoutObstacles;
        for (int i = 0; i < tilesToPreSpawn; i++)
        {
            spawnPosition -= tilePrefab.startPoint.localPosition;
            SC_PlatformTile spawnedTile = Instantiate(tilePrefab, spawnPosition,Quaternion.identity) as SC_PlatformTile;
            if(tilesWithNoObstaclesTmp > 0)
            {
                spawnedTile.DeactivateAllObstacles();
                tilesWithNoObstaclesTmp--;
            }
            else
            {
                spawnedTile.ActivateRandomObstacle();
            }
            
            spawnPosition = spawnedTile.endPoint.position;
            spawnedTile.transform.SetParent(transform);
            spawnedTiles.Add(spawnedTile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object upward in world space x unit/second.
        //Increase speed the higher score we get
        if (!(gameOverPlayer1 && gameOverPlayer2 && gameOverPlayer3 && gameOverPlayer4)  && gameStarted)
        {
            transform.Translate(spawnedTiles[0].transform.forward * Time.deltaTime * (movingSpeed + (score/500)), Space.World);
            /*scorePlayer1 += Time.deltaTime * movingSpeed;
            scorePlayer2 += Time.deltaTime * movingSpeed;
            scorePlayer3 += Time.deltaTime * movingSpeed;
            scorePlayer4 += Time.deltaTime * movingSpeed;*/
        }
        
        if (!gameOverPlayer1)
            scorePlayer1 += Time.deltaTime * movingSpeed;
        if (!gameOverPlayer2)
            scorePlayer2 += Time.deltaTime * movingSpeed;
        if (!gameOverPlayer3)
            scorePlayer3 += Time.deltaTime * movingSpeed;
        if (!gameOverPlayer4)
            scorePlayer4 += Time.deltaTime * movingSpeed;
        
        if (mainCamera.WorldToViewportPoint(spawnedTiles[0].endPoint.position).z < 0)
        {
            //Move the tile to the front if it's behind the Camera
            SC_PlatformTile tileTmp = spawnedTiles[0];
            spawnedTiles.RemoveAt(0);
            tileTmp.transform.position = spawnedTiles[spawnedTiles.Count - 1].endPoint.position - tileTmp.startPoint.localPosition;
            tileTmp.ActivateRandomObstacle();
            spawnedTiles.Add(tileTmp);
        }
        
        if (gameOverPlayer1)
        {
            CameraP1.targetTexture = backTexture;
        }
                
        if (gameOverPlayer2)
        {
            CameraP2.targetTexture = backTexture;
        }
                
        if (gameOverPlayer3)
        {
            CameraP3.targetTexture = backTexture;
        }
                
        if (gameOverPlayer4)
        {
            CameraP4.targetTexture = backTexture;
        }

        if (gameOverPlayer1 && gameOverPlayer2 && gameOverPlayer3 && gameOverPlayer4 || !gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (gameOverPlayer1 && gameOverPlayer2 && gameOverPlayer3 && gameOverPlayer4)
                {
                    //Restart current scene
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }
                else
                {
                    //Start the game
                    gameStarted = true;
                }
            }
        }
    }

    void OnGUI()
    {
        /*if (gameOverPlayer1)
        {
            GUI.color = Color.blue;
            GUI.Label(new Rect(40, 40, 200, 200), "Game Over\nYour score is: " + ((int)scorePlayer1) + "\nPress 'Space' to restart");
        }
        else if (gameOverPlayer2)
        {
            GUI.color = Color.green;
            GUI.Label(new Rect(Screen.width / 2 + 40, 40, 200, 200), "Game Over\nYour score is: " + ((int)scorePlayer2) + "\nPress 'Space' to restart");
        }
        else if (gameOverPlayer3)
        {
            GUI.color = Color.red;
            GUI.Label(new Rect(40, Screen.height / 2 + 40, 200, 200), "Game Over\nYour score is: " + ((int)scorePlayer3) + "\nPress 'Space' to restart");
        }
        else if (gameOverPlayer4)
        {
            GUI.color = Color.yellow;
            GUI.Label(new Rect(Screen.width / 2 + 40, Screen.height / 2 + 40, 200, 200), "Game Over\nYour score is: " + ((int)scorePlayer4) + "\nPress 'Space' to restart");
        }
        else
        {*/
            if (!gameStarted)
            {
                GUI.color = Color.black;
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), "Press 'Space' to start");
            }

            if (gameOverPlayer1 && gameOverPlayer2 && gameOverPlayer3 && gameOverPlayer4)
            {
                GUI.color = Color.black;
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), "Press 'Space' to start");
            }
       // }
    }
}