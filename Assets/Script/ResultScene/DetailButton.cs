using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetailButton : MonoBehaviour
{
    Transform parent;
    GameObject director;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        director = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        int id = Int32.Parse(parent.Find("ID").GetComponent<TextMeshProUGUI>().text);
        StartCoroutine(PostDetail(id));
    }

    IEnumerator PostDetail(int id){
        string json = "{\"device\":\"" + PlayerPrefs.GetString("UUID") + "\",\"id\":" + id + "}";
        yield return GameDirector.WebReqPost("detail", json);
        director.GetComponent<GameDirector>().MoveScene("ResultPage","DetailPage");
    }
}
