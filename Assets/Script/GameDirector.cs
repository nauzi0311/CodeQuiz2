using System;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;
using System.IO;

using Debug = UnityEngine.Debug;

public class GameDirector : MonoBehaviour
{
    // Start is called before the first frame update
    static bool is_debug = false;
    [SerializeField] Fade fade = null;
    TextMeshProUGUI DebugText;
    public static string _debug = "";
    GameObject fadecanvas = null;
    public static int[] UAnswer;
    public static int[] second_list;
    public static bool[] correct_list;
    public static UserData userdata;
    public static Config config;
    static string HerePage, response;
    //static string server = "https://se.is.kit.ac.jp/beakfish/api/";
    static string server = "http://localhost:3000/";

    private string output,stack;
    void Awake() {
        if(userdata == null){
            userdata = new UserData();
        }
        if(config == null){
            config = new Config();
        }
        if(UAnswer == null){
            UAnswer = new int[7];
        }
        if(second_list == null){
            second_list = new int[7];
        }
        if(correct_list == null){
            correct_list = new bool[7];
        }
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }
        void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }
        void HandleLog(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace;
        if(type == LogType.Error || type == LogType.Warning){
            _debug = "<color=\"red\">" + output + "</color>" + "\n" + _debug;
        }
        //_debug = "<color=\"red\">" + output + " on " + stack + "</color>" + "\n" + _debug;
    }
    void Start()
    {
        if(is_debug){
            DebugText = GameObject.Find("Debug").GetComponent<TextMeshProUGUI>();
            DebugText.color = Color.black;
        }else{
            GameObject.Find("DebugCanvas").SetActive(false);
        }
        fadecanvas = GameObject.Find("FadeCanvas");
    }

    void Update(){
        if (fadecanvas.GetComponent<FadeImage>().GetCutoutRange() == 1)
        {
            fade.FadeOut(1);
        }
        if(is_debug){
            DebugText.text = _debug;
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            // エスケープキー取得
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // アプリケーション終了
                Application.Quit();
                return;
            }
        }
    }

    public void MoveScene(string Here, string ToPage, float time = 1.0f)
    {
        fade.FadeIn(time, () =>
        {
            HerePage = Here;
            SceneManager.LoadScene(ToPage);
        });
    }

    public void LevelReload(Slider _LSlider,TextMeshProUGUI _LText,TextMeshProUGUI _LScore){
        int _ulevel = GameDirector.userdata.level;
        int _uexp = GameDirector.userdata.exp;
        int _umax = GameDirector.config.level[_ulevel];
        _LText.text = "Lv" + _ulevel;
        _LScore.text = _uexp + "/" + _umax;
        _LSlider.value = (float)_uexp/_umax;
    }

    public float GetFadeValue(){
        return fadecanvas.GetComponent<FadeImage>().GetCutoutRange();
    }


    public static string GetFromPage()
    {
        return HerePage;
    }

    public static string GetResponse()
    {
        return response;
    }

    public static IEnumerator WebReqGet(string uri)
    {
        using(UnityWebRequest req = UnityWebRequest.Get(server + uri)){
            yield return req.SendWebRequest();
            if (req.result != UnityWebRequest.Result.Success)
            {
                switch (req.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                        Debug.Log("Connection Error");
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.Log("Data Processing Error");
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.Log("Protocol Error");
                        break;
                    default:
                        Debug.Log("Unknown Error" + req.error);
                        break;
                }
            }
            else
            {
                response = req.downloadHandler.text;
                //You need calls GameDirector.GetResponse();
            }
        }
    }

    public static IEnumerator Upgrade(string version,string url){
        using(UnityWebRequest req = UnityWebRequest.Get(url)){
            yield return req.SendWebRequest();
            if (req.result != UnityWebRequest.Result.Success)
            {
                switch (req.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                        Debug.Log("Connection Error");
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.Log("Data Processing Error");
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.Log("Protocol Error");
                        break;
                    default:
                        Debug.Log("Unknown Error" + req.error);
                        break;
                }
            }
            else
            {
                string path = Application.temporaryCachePath;
                string filePath = path + "/CodeQuiz-" + version + ".apk";

                FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(req.downloadHandler.data);
                writer.Flush();
                writer.Close();

                Process process = new Process();
                process.StartInfo.FileName = filePath;
                process.Start();
            }
        }
    }

    public static IEnumerator WebReqPost(string uri, string data)
    {
        //You need to change data json format
        _debug = "url:" + server + uri + "\n" + _debug;
        _debug = "data:" + data + "\n" + _debug;
        _debug = "uri:" + uri + "\n" + _debug;
        byte[] post_data = Encoding.UTF8.GetBytes(data);
        using(UnityWebRequest req = new UnityWebRequest(server + uri, UnityWebRequest.kHttpVerbPOST)){
            req.uploadHandler = (UploadHandler)new UploadHandlerRaw(post_data);
            req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");
            yield return req.SendWebRequest();
            if (req.result != UnityWebRequest.Result.Success)
            {
                switch (req.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                        Debug.Log("Connection Error");
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.Log("Data Processing Error");
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.Log("Protocol Error");
                        break;
                    default:
                        Debug.Log("Unknown Error" + req.error);
                        break;
                }
            }
            else
            {
                response = req.downloadHandler.text;
                //You need calls GameDirector.GetResponse();
                _debug = "\n" + GameDirector.GetResponse() + "\n" + _debug;
                Debug.Log(GameDirector.GetResponse());
            }
        }
    }

    public static void Checklist(){
        string seconds = "second_list:";
        string answers = "UAnswer:";
        string corrects = "correct_list:";
        for(int i = 0; i < 7;i++){
            seconds += " " + second_list[i];
            answers += " " + UAnswer[i];
            corrects += " " + correct_list[i];
        }
    }

    public static int GetId(int access_num){
        return QuizScene.GetQuestDatas()[access_num].id;
    }
    public static string GetUAnswer(int access_num){
        if(UAnswer[access_num] == 5){
            return "Skip";
        }
        if(UAnswer[access_num] == 0){
            return "Time Out";
        }  
        return QuizScene.GetQuestDatas()[access_num].choice[UAnswer[access_num]-1];
    }
    public static string GetAnswer(int access_num){
        QuestData _tmp = QuizScene.GetQuestDatas()[access_num];
        return _tmp.choice[_tmp.answer-1];
    }

    public static void CalculateInResult(){
        QuestData[] _quest = QuizScene.GetQuestDatas();
        int exp = GameDirector.userdata.exp;
        int point = GameDirector.userdata.point;
        for(int i = 0; i < _quest.Length; i++){
            if(correct_list[i]){
                if(second_list[i] <= 30){
                    exp += (int)(_quest[i].exp*1.5);
                }else{
                    exp += _quest[i].exp;
                }
                point += _quest[i].point;
                GameDirector.userdata.correct_count++;
                GameDirector.userdata.correct_id.Add(_quest[i].id);
            }
        }
        while(GameDirector.config.level[GameDirector.userdata.level] <= exp){
            exp -= GameDirector.config.level[GameDirector.userdata.level];
            GameDirector.userdata.level++;
        }
        //userdataの更新
        GameDirector.userdata.exp = exp;
        GameDirector.userdata.point = point;
        GameDirector.userdata.correct_id.Distinct();
        GameDirector.userdata.date.Add(DateTime.Now.ToString("yyyy-MM-dd"));
        GameDirector.userdata.date.Distinct();
        List<bool> _tmp = new List<bool>(GameDirector.userdata.badge);
        GameDirector.userdata.badge = new List<bool>(BadgeChecker.Check(GameDirector.userdata));
        for(int i = 0;i < GameDirector.userdata.badge.Count; i++){
            if(!_tmp[i] && GameDirector.userdata.badge[i]){
                ResultScene.Is_new_badge = true;
            }
        }
    }
}

// dictionary型が必要な場合↓
// https://qiita.com/toRisouP/items/53be639f267da8845a42#%E4%BD%BF%E7%94%A8%E4%BE%8Bdictionary%E3%82%92json%E3%81%AB%E3%81%99%E3%82%8B

[System.Serializable]
public class Config
{
    public int[] level;
    public string version;

    public Config(){
        level = new int[] {70,140};
    }
    public static Config Deserialize(string json)
    {
        Config _data = JsonUtility.FromJson<Config>(json);
        return _data;
    }
}

public class UserData
{
    public int school_num;
    public int level;
    public int exp;
    public int point;
    public List<int> correct_id;
    public int correct_count;
    public List<bool> badge;
    public List<string> date;

    public UserData(int _snum){
        school_num = _snum;level = 1;exp = 0;point = 0;correct_count =0;
        correct_id = new List<int>();
        badge = new List<bool>(){false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false};
        date = new List<string>();
    }

    public UserData(){
        school_num = 0;level = 1;exp = 0;point = 0;correct_count =0;
        correct_id = new List<int>();
        badge = new List<bool>(){false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false};
        date = new List<string>();
    }

    public static UserData Deserialize(string json)
    {
        UserData _data = JsonUtility.FromJson<UserData>(json);
        return _data;
    }

    public static string Serialize(UserData _data)
    {
        string json = JsonUtility.ToJson(_data);
        return json;
    }

    public string BadgeToString(){
        string result = "[";
        for(int i = 0; i < badge.Count; i++){
            result += badge[i] ? ",true" : ",false";
        }
        return result + "]";
    }
    public string CorrectIdToString(){
        string result = "[";
        for(int i = 0; i < correct_id.Count; i++){
            result += correct_id[i] + ",";
        }
        return result + "]";
    }

    public override string ToString(){
        string result = "school_num: " + school_num + "\n" +
        "level: " + level + "\n" +
        "point: " + point + "\n" +
        "exp: " + exp + "\n" +
        "correct_id: " + correct_id + "\n" +
        "correct_count: " + CorrectIdToString() + "\n" +
        "badge: " + BadgeToString() + "\n" +
        "date: " + date;
        return result;
    }
}



/* Deserializer Example
public class MemoDate{

    public string message;
    public static MemoDate Deserialize(string json){
        MemoDate memo = JsonUtility.FromJson<MemoDate>(json);
        return memo;
    }

    public static string Serialize(MemoDate memo){
        **Attention** You need add public attributes if you need to change the parameters to json format.
        return JsonUtility.ToJson(memo);
    }
}
*/
