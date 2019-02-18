using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SceneGenScript : MonoBehaviour {
    float waitTime;
    float waitTimeLength = 1;
    int countDownNum;
    bool startedCountDown = false;
    bool countDownFinished = false;
    bool showNewSprite = false;

    GameObject player1;
    GameObject player2;
    GameObject spawnedPlayer1;
    GameObject spawnedPlayer2;
    GameObject CountDownHolder;
    GameObject cd_CurTime_Sprite;
    GameObject cdThree;
    GameObject cdTwo;
    GameObject cdOne;
    GameObject cdStart;

    void initializeGeneration()
    {
        player1 = SetPlayerChar(1);
        player2 = SetPlayerChar(2);

        //Instantiate(player1, GetSpawnLocation(1).position, Quaternion.Euler(0, 0, 0));
        //Instantiate(player2, GetSpawnLocation(2).position, Quaternion.Euler(0, 180, 0));
        Debug.Log("Calling beginCountDown");
        beginCountDown();
    }

    GameObject SetPlayerChar(int playernum)
    {
        try
        {
            #region How to load Prefab Examples
            // Each of these methods seem to work to get the prefab
            // You need the areapath AS WELL AS a dot(.) prefab sufflix to the end of the name
            //player = (GameObject)AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefab/Player1 OOP.prefab");
            //player = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/prefab/Player1 OOP.prefab", typeof(GameObject));
            //player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefab/Player1 OOP.prefab");
            //player = (GameObject)AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefab/Player1 OOP.prefab");
            //player = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/prefab/Player1 OOP.prefab", typeof(GameObject));
            #endregion

            //used to set this and return, can do for debugging
            //player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefab/Player1 OOP.prefab");
            //Debug.Log(player);
            return AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefab/" + getPrefab(playernum) + ".prefab");
        }
        catch
        {
            Debug.Log("Crashed in SetPlayerChar.");
            return null;
        }
    }
    
    string getPrefab(int playernum)
    {
        int selectedCharacter;
        if (playernum == 1)
        {
            selectedCharacter = StaticInfoScript.p1CharacterChoice;
        }
        else
        {
            selectedCharacter = StaticInfoScript.p2CharacterChoice;
        }

//        switch (selectedCharacter)
        switch (1)
        {
            case 1:
                return "IceRider";
            case 2:
                return "Blaster";
            case 3:
                return "Swinger";
            case 4:
                return "Phaser";
            default:
                Debug.Log("Error in selectedCharacter from StaticInfoScript returning empty for player num " + playernum);
                return "";
        }
    }

    Transform GetSpawnLocation(int playerNum)
    {
        string SpwnPntName = "Spawnpoints_Player";
        string spawnPrefix = "Spawn";
        #region debug location Test Region
        // Relative pathing
        // this.gameObject.transform.Find(SpwnPntName + playerNum).Find(spawnPrefix + Random.Range(1, 2)).gameObject.transform;
        // Objective pathing
        // Debug.Log(GameObject.Find(SpwnPntName + playerNum));
        // Debug.Log(GameObject.Find(SpwnPntName + playerNum).transform);
        // Debug.Log(GameObject.Find(SpwnPntName + playerNum).transform.Find(spawnPrefix + Random.Range(1, 3)));
        // Debug.Log(GameObject.Find(SpwnPntName + playerNum).transform.Find(spawnPrefix + Random.Range(1, 3)).gameObject);
        // Debug.Log(GameObject.Find(SpwnPntName + playerNum).transform.Find(spawnPrefix + Random.Range(1, 3)).gameObject.transform);
        #endregion

        return GameObject.Find(SpwnPntName + playerNum).transform.Find(spawnPrefix + Random.Range(1, 3)).gameObject.transform;
    }

    void beginCountDown()
    {
        Debug.Log("In beginCountDown");

        CountDownHolder = GameObject.Find("Count_D_To_Start");
        cdThree = CountDownHolder.transform.Find("C_D_3").gameObject;
        cdTwo = CountDownHolder.transform.Find("C_D_2").gameObject;
        cdOne = CountDownHolder.transform.Find("C_D_1").gameObject;
        cdStart = CountDownHolder.transform.Find("C_D_Start").gameObject;
        Debug.Log(CountDownHolder);
        Debug.Log(cdThree);
        cdThree.SetActive(true);
    }

    void countDownHandler()
    {
        switch (countDownNum)
        {
            case 3:
                cd_CurTime_Sprite = cdThree;
                continueCountDown(cd_CurTime_Sprite);
                break;
            case 2:
                cd_CurTime_Sprite = cdTwo;
                continueCountDown(cd_CurTime_Sprite);
                break;
            case 1:
                cd_CurTime_Sprite = cdOne;
                continueCountDown(cd_CurTime_Sprite);
                break;
            case 0:
                cd_CurTime_Sprite = cdStart;
                continueCountDown(cd_CurTime_Sprite);
                break;
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Go through and:
    // Either create an array of names ex:{IceRider_p1, Blaster_p1, Swinger_p1, ect} for p1 and p2 
    // and have a check which enables and disables all of both at 3 and start respectivly
    // or figure somthing out with regex. 
    // Clean up this code.
    // When done remove the temp dev changes in getPrefab
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void continueCountDown(GameObject cd_CurTime_Sprite) 
    {
        if(waitTime <= 0)
        {
            Debug.Log(waitTime);
            waitTime = waitTimeLength;
            cd_CurTime_Sprite.SetActive(false);
            Debug.Log("Done with " + cd_CurTime_Sprite.name);
            Debug.Log(waitTime);
            countDownNum--;
            showNewSprite = true;
            if (cd_CurTime_Sprite.name == "C_D_3")
            {
                spawnedPlayer1 = Instantiate(player1, GetSpawnLocation(1).position, Quaternion.Euler(0, 0, 0));
                //Help Differentiate the characters and set their names to be called if needed later
                spawnedPlayer1.name = player1.name + "_p1";
                if(spawnedPlayer1.name == "IceRider_p1")
                {
                    spawnedPlayer1.GetComponent<IceCharacterMovement>().enabled = false;
                }
            }
            else if (cd_CurTime_Sprite.name == "C_D_2")
            {
                spawnedPlayer2 = Instantiate(player2, GetSpawnLocation(2).position, Quaternion.Euler(0, 180, 0));
                //Help Differentiate the characters and set their names to be called if needed later
                spawnedPlayer2.name = player2.name + "_p2";
            }
            else if(cd_CurTime_Sprite.name == "C_D_1")
            {
                
            }
            else if(cd_CurTime_Sprite.name == "C_D_Start")
            {
                Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                Debug.Log(spawnedPlayer1.name);
                if (spawnedPlayer1.name == "IceRider_p1")
                {
                    
                    spawnedPlayer1.GetComponent<IceCharacterMovement>().enabled = true;
                }
                countDownFinished = true;
            }
        }
        else
        {
            if(showNewSprite == true)
            {
                cd_CurTime_Sprite.SetActive(true);
                showNewSprite = false;
            }
            Debug.Log(waitTime);
            waitTime = updateWaitTimer();
        }
    }
    float updateWaitTimer()
    {
        return waitTime -= Time.deltaTime;
    }

    // Use this for initialization
    void Start () {
        countDownNum = 3;
        waitTime = waitTimeLength;
        Debug.Log("SceneStarted!!!! Scene Gen Script");
        initializeGeneration();
    }



    // Update is called once per frame
    void Update () {
        if(startedCountDown == false)
        {
            startedCountDown = true;
            beginCountDown();
        }
        if(countDownFinished == false)
        {
            countDownHandler();
        }
    }
}
