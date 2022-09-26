using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStageScene : MonoBehaviour
{
    GameObject director;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveScene(string ToPage){
        director.GetComponent<GameDirector>().MoveScene("SelectStagePage",ToPage);
    }
}
