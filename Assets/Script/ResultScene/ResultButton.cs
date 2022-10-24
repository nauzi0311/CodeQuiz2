using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultButton : MonoBehaviour
{
    [SerializeField] string Topage;
    GameObject director;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("SceneDirector");
    }

    public void OnClick(){
        director.GetComponent<ResultScene>().MoveScene(Topage);
        if(Topage == "ScorePage" && ScoreScene._data == null){
            StartCoroutine(ScoreScene.GetScoreData());
        }
        if(Topage == "RankingPage"){
            StartCoroutine(Ranking.RankingPost());
        }
    }
}
