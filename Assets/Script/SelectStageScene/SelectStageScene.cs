using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectStageScene : MonoBehaviour
{
    GameObject director,LSlider,LText,LScore;
    // Start is called before the first frame update
    void Start()
    {
        QuizScene.DeleteQuestions();
        director = GameObject.Find("GameDirector");
        LSlider = GameObject.Find("LevelSlider");
        LText = GameObject.Find("LevelText");
        LScore = GameObject.Find("LevelScore");
        director.GetComponent<GameDirector>().LevelReload(
            LSlider.GetComponent<Slider>(),
            LText.GetComponent<TextMeshProUGUI>(),
            LScore.GetComponent<TextMeshProUGUI>()
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveScene(string ToPage,string course,int times){
        //internet process
        switch (course){
            case "ソフトウェア演習1":
            course = "soft1"; break;
            default: course = "Unknown"; break;
        }
        StartCoroutine(Postquest(ToPage, course, times));
    }

    IEnumerator Postquest(string ToPage,string course,int times){
        Data _data = new Data(PlayerPrefs.GetString("UUID"),course,times);
        Debug.Log(Data.Serialize(_data));
        yield return GameDirector.WebReqPost("quest",Data.Serialize(_data));
        QuestDataSet _questions = QuestDataSet.Deserialize(GameDirector.GetResponse());
        Debug.Log(GameDirector.GetResponse());
        director.GetComponent<GameDirector>().MoveScene("SelectStagePage",ToPage);
        QuizScene.SetQuestions(_questions.quest);
    }

    class Data{
        public string device;
        public string course;
        public int times;

        public Data(string _d,string _c,int _t){
            device = _d;course = _c;times = _t;
        }

        static public string Serialize(Data _d){
            string json = JsonUtility.ToJson(_d);
            return json;
        }

        public override string ToString()
        {
            return "device:" + device + "course:" + course + "times" + times;
        }
    }
}

//https://nobushiueshi.com/unityjsonutility%E3%81%AB%E3%81%A4%E3%81%84%E3%81%A6%E3%81%BE%E3%81%A8%E3%82%81%E3%81%A6%E3%81%BF%E3%81%9F/#toc4
[System.Serializable]//必須
public class QuestData{
    public int id;
    public string title;
    public string question;
    public string source;
    public string output;
    public string[] choice;
    public int answer;
    public int exp;
    public int point;

    public override string ToString()
    {
        return string.Format("id:{0} title:{1} question:{2} source:{3} output:{4} choice:{5} {6} {7} {8} answer:{9} exp: {10} point {11}",id,title,question,source,output,choice[0],choice[1],choice[2],choice[3],answer,exp,point);
    }

    public static QuestData Deserialize(string json){
        return JsonUtility.FromJson<QuestData>(json);
    }
}

[System.Serializable]
public class QuestDataSet{
    public QuestData[] quest;

    public static QuestDataSet Deserialize(string json){
        return JsonUtility.FromJson<QuestDataSet>(json);
    }

    public int[] GetQuestIds(){
        int[] result = new int[7];
        for(int i = 0; i < 7; i++){
            result[i] = quest[i].id;
        }
        return result;
    }
}
