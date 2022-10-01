using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    GameObject Text;
    // Start is called before the first frame update
    void Start()
    {
        Text = transform.Find("Count").gameObject;
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CountDown(){
        for(int i = 0; i < 60;i++){
            yield return new WaitForSeconds(1.0f);
            Text.GetComponent<TextMeshProUGUI>().text = (Int32.Parse(Text.GetComponent<TextMeshProUGUI>().text) - 1).ToString();
        }
    }

    public int GetCount(){
        return Int32.Parse(Text.GetComponent<TextMeshProUGUI>().text);
    }
}
