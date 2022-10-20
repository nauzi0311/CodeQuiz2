using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingBackButton : MonoBehaviour
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

    public void OnClick(){
        director.GetComponent<GameDirector>().MoveScene("RankingPage","HomePage");
    }
}
