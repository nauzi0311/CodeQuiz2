using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    GameObject Text,director;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("SceneDirector");
        Text = transform.Find("Count").gameObject;
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown(){
        for(int i = 0; i < 60;i++){
            yield return new WaitForSeconds(1.0f);
            Text.GetComponent<TextMeshProUGUI>().text = (Int32.Parse(Text.GetComponent<TextMeshProUGUI>().text) - 1).ToString();
        }
        director.GetComponent<QuizScene>().MoveScene(5);
    }

    public int GetCount(){
        return 60 - Int32.Parse(Text.GetComponent<TextMeshProUGUI>().text);
    }
}
