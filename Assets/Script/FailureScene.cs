using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailureScene : MonoBehaviour
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

    public void MoveScene(){
        if(QuizScene._qcount == 7){
            director.GetComponent<GameDirector>().MoveScene("FailurePage","ResultPage");
        }else{
            director.GetComponent<GameDirector>().MoveScene("FailurePage","QuizPage");
        }
    }
}
