using System.Diagnostics;
using System.Net.Mime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(director == null) {Debug.LogError("No GameObject found");}
        else {Debug.Log("GameObject is found: " + director.name);}
        TextCode = GameObject.Find("Code");
        position = TextCode.GetComponent<TextAnimation>().stop_x;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(TextCode.GetComponent<Transform>().localPosition.x >= position){
                director.GetComponent<TopScene>().MoveTitle();
            }else if(TextCode.GetComponent<Transform>().localPosition.x < position){
                director.GetComponent<TopScene>().StartCoroutine("AleartTap");
                Debug.Log("Clicked ");
            }else{
                //never read
            }
        }
        if(TextCode.transform.localPosition.x <= position){
            director.GetComponent<TopScene>().VisualizeTap();
        }
    }
}
