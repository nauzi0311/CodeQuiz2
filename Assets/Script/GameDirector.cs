using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

using Debug = UnityEngine.Debug;

public class GameDirector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Fade fade = null;

    GameObject fadecanvas = null;
    string _ans1 = "p = (int*)malloc(sizeof(int)*4)";
    string[] UAnswer = new string[7];
    public static UserData userdata;
    public static Config config;
    static string HerePage, response;
    static string server = "http://se.is.kit.ac.jp/beakfish/api/";
    void Start()
    {
        fadecanvas = GameObject.Find("FadeCanvas");
        if (fadecanvas.GetComponent<Fade>().is_start)
        {
            fade.FadeOut(1);
        }
        for (int i = 0; i < UAnswer.Length; i++)
        {
            UAnswer[i] = _ans1;
        }
    }

    void Update()
    {

    }

    public void MoveScene(string Here, string ToPage, float time = 1.0f)
    {
        Debug.Log(ToPage);
        fade.FadeIn(time, () =>
        {
            HerePage = Here;
            SceneManager.LoadScene(ToPage);
        });
    }

    public static string GetFromPage()
    {
        return HerePage;
    }

    public string[] GetUAnswer()
    {
        return UAnswer;
    }

    public static string GetResponse()
    {
        return response;
    }

    public static IEnumerator WebReqGet(string uri)
    {
        Debug.Log(server + uri);
        UnityWebRequest req = UnityWebRequest.Get(server + uri);
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

    public static IEnumerator WebReqPost(string uri, string data)
    {
        //You need to change data json format
        byte[] post_data = Encoding.UTF8.GetBytes(data);
        UnityWebRequest req = new UnityWebRequest(server + uri, UnityWebRequest.kHttpVerbPOST);
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
        }
    }
}

public class UserData
{
    public int level;
    public int point;
    public int[] correct_id;
    public bool[] badge;
    public string[] date;

    public static UserData Deserialize(string json)
    {
        UserData _data = JsonUtility.FromJson<UserData>(json);
        return _data;
    }

    public static string Serialize(UserData _data)
    {
        string json = JsonUtility.ToJson(_data);
        Debug.Log("json:" + json);
        return json;
    }
}

public class Config : ISerializationCallbackReceiver
{
    public List<int> level = new List<int>();
    [SerializeField] private List<int> _keyList;
    [SerializeField] private List<int> _valueList;

    //dictionary型が必要な場合↓
    //https://qiita.com/toRisouP/items/53be639f267da8845a42#%E4%BD%BF%E7%94%A8%E4%BE%8Bdictionary%E3%82%92json%E3%81%AB%E3%81%99%E3%82%8B
    public void OnBeforeSerialize()
    {
        //シリアライズする際にkeyとvalueをリストに展開
        // _keyList = level.Keys.ToList();
        // _valueList = level.Values.ToList();
    }

    // Jsonからデシリアライズされたあとに実行される
    public void OnAfterDeserialize()
    {
        //デシリアライズ時に元のDictionaryに詰め直す
        level = _keyList.Select((id, index) =>
        {
            var value = _valueList[index];
            return new { id, value };
        }).ToDictionary(x => x.id, x => x.value);

        _keyList.Clear();
        _valueList.Clear();

    }
    public static Config Deserialize(string json)
    {
        Config _data = JsonUtility.FromJson<Config>(json);
        return _data;
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
