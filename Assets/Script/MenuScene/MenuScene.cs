using System.Diagnostics;
using System.Net.Mime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Debug = UnityEngine.Debug;

public class MenuScene : MonoBehaviour
{
    private GameObject DayBox;
    private GameObject TextYear;
    private GameObject TextMonth;
    private GameObject TextLevel;
    private GameObject SliderLevel;
    private GameObject LevelScore;
    private Outline beforebutton,today;
    private int[] Monthlist = new int[]{0,31,28,31,30,31,30,31,31,30,31,30,31};
    // Start is called before the first frame update
    void Start()
    {
        DayBox = GameObject.Find("Day");
        TextYear = GameObject.Find("Year");
        TextMonth = GameObject.Find("Month");
        beforebutton = null;
        InitialCalender();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitialCalender(){
        DateTime today = DateTime.Now;
        int year = today.Year;
        int month = today.Month;
        TextYear.GetComponent<TextMeshProUGUI>().text = year.ToString();
        TextMonth.GetComponent<TextMeshProUGUI>().text = month.ToString() + "月";
        int day = today.Day;
        float alpha_value = .4f;
        DayOfWeek dow = new DateTime(year, month, 1).DayOfWeek;
        GameObject DL1 = DayBox.transform.Find("DayLine1").gameObject;
        GameObject[] DL1arr = new GameObject[DL1.transform.childCount];
        for(int i = 0; i < DL1arr.Length; i++){
            DL1arr[i] = DL1.transform.Find((i+1).ToString()).gameObject;
        }
        int firstcount = 0;
        switch(dow){
            case DayOfWeek.Sunday:
                firstcount = 0;
                break;
            case DayOfWeek.Monday:
                firstcount = 1;
                break;
            case DayOfWeek.Tuesday:
                firstcount = 2;
                break;
            case DayOfWeek.Wednesday:
                firstcount = 3;
                break;
            case DayOfWeek.Thursday:
                firstcount = 4;
                break;
            case DayOfWeek.Friday:
                firstcount = 5;
                break;
            case DayOfWeek.Saturday:
                firstcount = 6;
                break;
        }
        // Set Clander
        for(int i = 1; i < 7; i++){
            string dayline = "DayLine" + i.ToString();
            GameObject DL = DayBox.transform.Find(dayline).gameObject;
            GameObject[] DLarr = new GameObject[DL.transform.childCount];
            for(int k = 0; k < DLarr.Length; k++){
                DLarr[k] = DL.transform.Find((k+1).ToString()).gameObject;
            }
            for(int j = 0;j < 7;j++){
                if(i == 1){
                    int before = firstcount - j;
                    if(before > 0){
                        if(month == 1){
                            DLarr[j].GetComponentInChildren<TextMeshProUGUI>().text = (Monthlist[12] - before + 1).ToString();
                        }else{
                            DLarr[j].GetComponentInChildren<TextMeshProUGUI>().text = (Monthlist[month-1] - before + 1).ToString();
                        }
                        DLarr[j].GetComponentInChildren<TextMeshProUGUI>().alpha = alpha_value;
                    }else{
                        DLarr[j].GetComponentInChildren<TextMeshProUGUI>().text = (j + 7*(i-1) - firstcount + 1).ToString();
                        if((j + 7*(i-1) - firstcount + 1) == day){
                            DLarr[j].GetComponent<DayButton>().OnClick();
                        }
                    }
                    continue;
                }
                if((j + 7*(i-1) - firstcount + 1) <= Monthlist[month]){
                    DLarr[j].GetComponentInChildren<TextMeshProUGUI>().text = (j + 7*(i-1) - firstcount + 1).ToString();
                    if((j + 7*(i-1) - firstcount + 1) == day){
                        DLarr[j].GetComponent<DayButton>().OnClick();
                    }
                }else{
                    DLarr[j].GetComponentInChildren<TextMeshProUGUI>().text = (j + 7*(i-1) - firstcount + 1 - Monthlist[month]).ToString();
                    DLarr[j].GetComponentInChildren<TextMeshProUGUI>().alpha = alpha_value;
                }
            }
        }
    }

    public void ChangeClaneder(int year,int month){
        TextYear.GetComponent<TextMeshProUGUI>().text = year.ToString();
        TextMonth.GetComponent<TextMeshProUGUI>().text = month.ToString() + "月";
        DayOfWeek dow = new DateTime(year, month, 1).DayOfWeek;
        var today = DateTime.Now;
        int firstcount = 0;
        float alpha_value = .4f;
        if(beforebutton != null){
            beforebutton.GetComponent<Outline>().enabled = false;
        }
        beforebutton = null;

        // What day is the first day
        switch(dow){
            case DayOfWeek.Sunday:
                firstcount = 0;
                break;
            case DayOfWeek.Monday:
                firstcount = 1;
                break;
            case DayOfWeek.Tuesday:
                firstcount = 2;
                break;
            case DayOfWeek.Wednesday:
                firstcount = 3;
                break;
            case DayOfWeek.Thursday:
                firstcount = 4;
                break;
            case DayOfWeek.Friday:
                firstcount = 5;
                break;
            case DayOfWeek.Saturday:
                firstcount = 6;
                break;
        }
        for(int i = 1; i < 7; i++){
            string dayline = "DayLine" + i.ToString();
            GameObject DL = DayBox.transform.Find(dayline).gameObject;
            GameObject[] DLarr = new GameObject[DL.transform.childCount];
            for(int k = 0; k < DLarr.Length; k++){
                DLarr[k] = DL.transform.Find((k+1).ToString()).gameObject;
            }
            for(int j = 0;j < 7;j++){
                if(i == 1){
                    int before = firstcount - j;
                    if(before > 0){
                        if(month == 1){
                            DLarr[j].GetComponentInChildren<TextMeshProUGUI>().text = (Monthlist[12] - before + 1).ToString();
                        }else{
                            DLarr[j].GetComponentInChildren<TextMeshProUGUI>().text = (Monthlist[month-1] - before + 1).ToString();
                        }
                        DLarr[j].GetComponentInChildren<TextMeshProUGUI>().alpha = alpha_value;
                    }else{
                        DLarr[j].GetComponentInChildren<TextMeshProUGUI>().text = (j + 7*(i-1) - firstcount + 1).ToString();
                        DLarr[j].GetComponentInChildren<TextMeshProUGUI>().alpha = 1;
                        if(month == today.Month){
                            if((j + 7*(i-1) - firstcount + 1) == today.Day){
                                DLarr[j].GetComponent<DayButton>().OnClick();
                            }
                        }
                    }
                    continue;
                }
                if((j + 7*(i-1) - firstcount + 1) <= Monthlist[month]){
                    DLarr[j].GetComponentInChildren<TextMeshProUGUI>().text = (j + 7*(i-1) - firstcount + 1).ToString();
                    DLarr[j].GetComponentInChildren<TextMeshProUGUI>().alpha = 1;
                    if(month == today.Month){
                        if((j + 7*(i-1) - firstcount + 1) == today.Day){
                            DLarr[j].GetComponent<DayButton>().OnClick();
                        }
                    }
                }else{
                    DLarr[j].GetComponentInChildren<TextMeshProUGUI>().text = (j + 7*(i-1) - firstcount + 1 - Monthlist[month]).ToString();
                    DLarr[j].GetComponentInChildren<TextMeshProUGUI>().alpha = alpha_value;
                }
            }
        }
    }
    public void SelectDayButton(Outline selected){
        if(beforebutton != null){
            beforebutton.GetComponent<Outline>().enabled = false;
        }
        beforebutton = selected;
    }
}
