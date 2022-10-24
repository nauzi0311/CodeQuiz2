using System.Linq;
using System.Diagnostics;
using System.Net.Mime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Debug = UnityEngine.Debug;

public class HomeScene : MonoBehaviour
{
    [SerializeField]
    List<GameObject> DayLine;
    private GameObject TextYear;
    private GameObject TextMonth;
    private GameObject TextLevel;
    private GameObject SliderLevel;
    private GameObject LevelScore;
    private Outline beforebutton,today;
    private int[] Monthlist = new int[]{0,31,28,31,30,31,30,31,31,30,31,30,31};
    GameObject director;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.FindGameObjectWithTag("GameDirector");
        TextYear = GameObject.FindGameObjectWithTag("Year");
        TextMonth = GameObject.FindGameObjectWithTag("Month");
        int ulevel = GameDirector.userdata.level;
        int uexp = GameDirector.userdata.exp;
        int umaxpoint = GameDirector.config.level[ulevel];
        TextLevel = GameObject.FindGameObjectWithTag("LevelText");
        TextLevel.GetComponent<TextMeshProUGUI>().text = "Lv" + ulevel;
        LevelScore = GameObject.FindGameObjectWithTag("LevelScore");
        LevelScore.GetComponent<TextMeshProUGUI>().text = uexp + "/" + umaxpoint;
        SliderLevel = GameObject.FindGameObjectWithTag("LevelSlider");
        SliderLevel.GetComponent<Slider>().value = (float)uexp / umaxpoint;
        beforebutton = null;
        Invoke("InitialCalender",0.1f);
    }
    void InitialCalender(){
        DateTime today = DateTime.Now;
        int year = today.Year;
        int month = today.Month;
        TextYear.GetComponent<TextMeshProUGUI>().text = year.ToString();
        TextMonth.GetComponent<TextMeshProUGUI>().text = month.ToString() + "月";
        int day = today.Day;
        float alpha_value = .4f;//当該月じゃない日にちのアルファ値
        DayOfWeek dow = new DateTime(year, month, 1).DayOfWeek;//曜日判定
        GameObject DL1 = DayLine[0];
        GameObject[] DL1arr = new GameObject[7];
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
            GameObject DL = DayLine[i-1];
            GameObject[] DLarr = new GameObject[7];
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
                        if(GameDirector.userdata.date.Contains(year + "-" + month.ToString("00") +"-" + (j + 7*(i-1) - firstcount + 1).ToString("00"))){
                            DLarr[j].transform.Find("Image").gameObject.GetComponent<Image>().enabled = true;
                        }else{
                            DLarr[j].transform.Find("Image").gameObject.GetComponent<Image>().enabled = false;
                        }
                    }
                    continue;
                }
                if((j + 7*(i-1) - firstcount + 1) <= Monthlist[month]){
                    DLarr[j].GetComponentInChildren<TextMeshProUGUI>().text = (j + 7*(i-1) - firstcount + 1).ToString();
                    if((j + 7*(i-1) - firstcount + 1) == day){
                        DLarr[j].GetComponent<DayButton>().OnClick();
                    }
                    if(GameDirector.userdata.date.Contains(year + "-" + month.ToString("00") +"-" + (j + 7*(i-1) - firstcount + 1).ToString("00"))){
                        DLarr[j].transform.Find("Image").gameObject.GetComponent<Image>().enabled = true;
                    }else{
                            DLarr[j].transform.Find("Image").gameObject.GetComponent<Image>().enabled = false;
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
            GameObject DL = DayLine[i-1];
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
                        if(GameDirector.userdata.date.Contains(year + "-" + month.ToString("00") +"-" + (j + 7*(i-1) - firstcount + 1).ToString("00"))){
                            DLarr[j].transform.Find("Image").gameObject.GetComponent<Image>().enabled = true;
                        }else{
                            DLarr[j].transform.Find("Image").gameObject.GetComponent<Image>().enabled = false;
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
                    if(GameDirector.userdata.date.Contains(year + "-" + month.ToString("00") +"-" + (j + 7*(i-1) - firstcount + 1).ToString("00"))){
                        DLarr[j].transform.Find("Image").gameObject.GetComponent<Image>().enabled = true;
                    }else{
                        DLarr[j].transform.Find("Image").gameObject.GetComponent<Image>().enabled = false;
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

    public void MoveScene(string ToPage){
        director.GetComponent<GameDirector>().MoveScene("HomePage",ToPage);
    }

    public void ClickCalender(string day){
        string year = TextYear.GetComponent<TextMeshProUGUI>().text;
        string month = TextMonth.GetComponent<TextMeshProUGUI>().text[..^1];
        CalenderData data = new CalenderData(PlayerPrefs.GetString("UUID"), year + "-" + month + "-" + day);
        StartCoroutine(CalenderCoroutine(data));
    }

    IEnumerator CalenderCoroutine(CalenderData data){
        yield return StartCoroutine(GameDirector.WebReqPost("calender",CalenderData.Serialize(data)));
        string res = GameDirector.GetResponse();
    }

    class CalenderData{
        public string device,date;

        public CalenderData(string _d,string _da){
            device = _d;date = _da;
        }

        public static string Serialize(CalenderData data){
            string json = JsonUtility.ToJson(data);
            return json;
        } 
        public static CalenderData Deserialize(string json){
            CalenderData _data = JsonUtility.FromJson<CalenderData>(json);
            return _data;
        }
    }
}
