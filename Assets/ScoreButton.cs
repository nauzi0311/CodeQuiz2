using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreButton : MonoBehaviour
{
    GameObject director;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("SceneDirector");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        director.GetComponent<HomeScene>().MoveScene("ScorePage");
    }
}
