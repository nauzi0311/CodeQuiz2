using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonthButton : MonoBehaviour
{
    GameObject SceneDirector;
    public int diffmonth;
    int year,month;
    // Start is called before the first frame update
    void Start()
    {
        SceneDirector = GameObject.Find("SceneDirector");
    }

    public void OnClick(){
        year = Int32.Parse(GameObject.Find("Year").GetComponent<TextMeshProUGUI>().text);
        string tmp = GameObject.Find("Month").GetComponent<TextMeshProUGUI>().text;
        month = Int32.Parse(tmp.Substring(0,tmp.Length-1));
        if((month + diffmonth) == 0){
            year -= 1;
            month = 12;
        }else if((month + diffmonth) == 13){
            year += 1;
            month = 1;
        }else{
            month += diffmonth;
        }
        SceneDirector.GetComponent<HomeScene>().ChangeClaneder(year, month);
    }
}
