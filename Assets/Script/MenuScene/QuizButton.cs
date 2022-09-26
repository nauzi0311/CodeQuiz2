using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizButton : MonoBehaviour
{
    GameObject director;
    void Start(){
        director = GameObject.Find("SceneDirector");
    }
    public void OnClick(){
        director.GetComponent<HomeScene>().MoveScene("SelectStagePage");
    }
}
