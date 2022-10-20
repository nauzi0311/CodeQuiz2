using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultScene : MonoBehaviour
{
    static public bool Is_new_badge = false;
    GameObject director,Score,LSlider,LText,LScore,NewBadge;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("GameDirector");
        NewBadge = GameObject.Find("NewBadgeCanvas");
        NewBadge.SetActive(false);
        LSlider = GameObject.Find("LevelSlider");
        LText = GameObject.Find("LevelText");
        LScore = GameObject.Find("LevelScore");
        Score = GameObject.Find("Score");
        int correct_count = 0;
        for(int i = 0; i < GameDirector.correct_list.Length; i++){
            if(GameDirector.correct_list[i]){
                correct_count++;
            }
        }
        Score.GetComponent<TextMeshProUGUI>().text = correct_count + "/7";
        director.GetComponent<GameDirector>().LevelReload(
            LSlider.GetComponent<Slider>(),
            LText.GetComponent<TextMeshProUGUI>(),
            LScore.GetComponent<TextMeshProUGUI>()
        );
        if(GameDirector.GetFromPage() == "QuizPage" || GameDirector.GetFromPage() == "FailurePage"){
            StartCoroutine(ResultPost());
        }
        GameDirector.Checklist();
        if(Is_new_badge){
            StartCoroutine(NotionNewBadge());
            Is_new_badge = false;
        }
    }

    IEnumerator NotionNewBadge(){
        NewBadge.SetActive(true);
        yield return new WaitForSeconds(2);
        NewBadge.SetActive(false);
    }

    IEnumerator ResultPost(){
        QuestDataSet _tmp = new QuestDataSet();
        _tmp.quest = QuizScene.GetQuestDatas();
        Data _data = new Data(PlayerPrefs.GetString("UUID"),
            GameDirector.userdata.level,
            GameDirector.userdata.exp,
            GameDirector.userdata.point,
            _tmp.GetQuestIds(),
            GameDirector.correct_list,
            GameDirector.second_list,
            GameDirector.UAnswer,
            GameDirector.userdata.badge.ToArray()
        );
        Debug.Log(Data.Serialize(_data));
        Debug.Log(GameDirector.userdata.ToString());
        yield return GameDirector.WebReqPost("result",Data.Serialize(_data));
        Debug.Log(GameDirector.GetResponse());
    }

    public IEnumerator DetailPost(int id){
        string json = "{\"device\":\"" + PlayerPrefs.GetString("UUID") + "\",\"id\":\"" + id + "\"}";
        Debug.Log(json);
        yield return GameDirector.WebReqPost("detail",json);
        this.MoveScene("DetailPage");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveScene(string Topage){
        director.GetComponent<GameDirector>().MoveScene("ResultPage",Topage);
    }

    class Data{
        //device, level, point, id_list, correct_list, second_list, user_answer, badge
        public string device;
        public int level;
        public int exp;
        public int point;
        public int[] id_list;
        public bool[] correct_list;
        public int[] second_list;
        public int[] user_answer;
        public bool[] badge;

        public Data(string _d,int _l,int _e,int _p,int[] _id_l,bool[] _co_l,int[] _s_l,int[] _u_a,bool[] _b){
            device = _d;exp = _e;level = _l;point = _p;id_list = _id_l;correct_list = _co_l;second_list = _s_l;user_answer = _u_a;badge = _b;
        }

        static public string Serialize(Data _d){
            string json = JsonUtility.ToJson(_d);
            Debug.Log(json);
            return json;
        }
    }
}
