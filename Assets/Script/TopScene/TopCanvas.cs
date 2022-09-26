using System.Diagnostics;
using System.Net.Mime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

using Debug = UnityEngine.Debug;

public class TopCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject director;
    GameObject TextCode;
    float position;
    void Start()
    {
        director = GameObject.Find("SceneDirector");
        TextCode = GameObject.Find("Code");
        position = -140;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(TextCode.GetComponent<Transform>().localPosition.x > position){
                director.GetComponent<TopScene>().MoveTitle();
            }else if(TextCode.GetComponent<Transform>().localPosition.x <= position){
                director.GetComponent<TopScene>().StartCoroutine("AleartTap");
                director.GetComponent<TopScene>().MoveScene();
            }else{
                //never read
            }
        }
        if(TextCode.transform.localPosition.x <= position){
            director.GetComponent<TopScene>().VisualizeTap();
        }
    }
}
