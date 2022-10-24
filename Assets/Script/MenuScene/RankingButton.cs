using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingButton : MonoBehaviour
{
    GameObject director;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("SceneDirector");
    }
    public void OnClick(){
        StartCoroutine(MoveScore());
    }

    IEnumerator MoveScore(){
        string json = "{\"device\":\"" + PlayerPrefs.GetString("UUID") + "\"}";
        Debug.Log(json);
        yield return GameDirector.WebReqPost("rank",json);
        Debug.Log(GameDirector.GetResponse());
        director.GetComponent<HomeScene>().MoveScene("RankingPage");
    }
}
