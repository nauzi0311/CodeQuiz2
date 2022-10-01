using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStageScene : MonoBehaviour
{
    GameObject director;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("GameDirector");
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
        Debug.Log(_data.ToString());
        Debug.Log(Data.Serialize(_data));
        yield return GameDirector.WebReqPost("quest",Data.Serialize(_data));
        QuestDataSet _questions = QuestDataSet.Deserialize(GameDirector.GetResponse());
        Debug.Log(_questions.quest[0].ToString());
        director.GetComponent<GameDirector>().MoveScene("SelectStagePage",ToPage);
    }
}

class Data{
    string device;
    string course;
    int times;

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
        return string.Format("id:{0} title:{1} question:{2} source:{3} output:{4} choice:{5} {6} {7} {8} answer:{9} exp: {10} point {11}",
        id,title,question,source,output,choice[0],choice[1],choice[2],choice[3],answer,exp,point);
    }
}

public class QuestDataSet{
    public QuestData[] quest;

    public static QuestDataSet Deserialize(string json){
        return JsonUtility.FromJson<QuestDataSet>(json);
    }
}
