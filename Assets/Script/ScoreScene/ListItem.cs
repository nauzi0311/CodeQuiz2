using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListItem : MonoBehaviour
{
    GameObject director;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("SceneDirector");
    }

    public void OnClick(){
        Transform ID = gameObject.transform.Find("Id");
        int _id = Int32.Parse(ID.gameObject.GetComponent<TextMeshProUGUI>().text);
        StartCoroutine(director.GetComponent<ScoreScene>().MoveDetail(_id));
    }
}
