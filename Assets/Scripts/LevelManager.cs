using UnityEngine;

using System;
using System.Collections;
using System.IO;

using SimpleJSON;

public class LevelManager : MonoBehaviour {

    public string levelName;

    public float Y_TOP;

    JSONNode levelJSON;
    int id;
    int numEnemy;

    float curTime;

    // Use this for initialization
    void Start () {
        curTime = 0.0f;
        id = 0;

        LoadLevelJsonFile(levelName + ".json");
	}
	
	// Update is called once per frame
	void Update () {
        curTime += Time.deltaTime;

        //while(id < numEnemy && levelJSON[id]["time"].AsFloat >= curTime)
        if (id < numEnemy && levelJSON[id]["time"].AsFloat <= curTime)      // prevent stuck frame
        {
            //generate this 
            GenerateEnemy(levelJSON[id].AsObject);

            id++;
        }
    }


    public string ReadTextFile(string filename)
    {
        string dataAsString = "";

        try
        {
            // open text file
            StreamReader textReader = File.OpenText(Application.dataPath + "/Levels/" + filename);

            // read contents
            dataAsString = textReader.ReadToEnd();

            // close file
            textReader.Close();

        }
        catch (Exception e)
        {
            //			display/set e.Message error message here if you wish  ...
        }

        // return contents
        return dataAsString;
    }

    public void LoadLevelJsonFile(string json_file)
    {
        levelJSON = JSON.Parse(ReadTextFile(json_file));

        // first level is array
        numEnemy = levelJSON.Count;
    }

    public void GenerateEnemy(JSONClass enemyJson)
    {
        GameObject newEnemy = Instantiate(Resources.Load("Enemy/" + enemyJson["class"]), new Vector3(enemyJson["x"].AsFloat, Y_TOP), Quaternion.identity) as GameObject;

        //enemy move path? optional?
        EnemyMovePath emp = newEnemy.GetComponent<EnemyMovePath>();
        if(emp)
        {
            emp.pathName = enemyJson["path"];
            emp.pathTime = enemyJson["pathTime"].AsFloat;
        }
        
        // TODO: ? generate path? probably not
        // put all paths in the scene

    }
}
