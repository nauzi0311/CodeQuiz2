using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScene : MonoBehaviour
{
    GameObject director;

    [SerializeField]
    GameObject prefab_ListItem;

    GameObject Content;
    List<GameObject> objlist = new List<GameObject>();
    public static ScoreData _data = null;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 _pos = new Vector3(0.0f,-95f,0.0f);
        director = GameObject.Find("GameDirector");
        Content = GameObject.Find("Content");
        Debug.Log(GameDirector.GetFromPage());
        if(GameDirector.GetFromPage() == "HomePage"){
            _data = ScoreData.Deserialize(GameDirector.GetResponse());
        } else{
            if(_data == null){
                StartCoroutine(GetScoreData());
                _data = ScoreData.Deserialize(GameDirector.GetResponse());
            }
            Debug.Log(_data.id_list[0]);
        }
        for(int i = 0; i < _data.id_list.Length; i++){
            objlist.Add(Instantiate(prefab_ListItem));
            var obj = objlist[i];
            obj.transform.SetParent(Content.transform,false);
            obj.transform.Find("Id").GetComponent<TextMeshProUGUI>().text = _data.id_list[i].ToString();
            obj.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = _data.title_list[i];
            obj.transform.localPosition = _pos + new Vector3(0.0f,-190*i,0);
        }
        Content.GetComponent<adjuster>().SizeAdjuster(_data.id_list.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator MoveDetail(int _id){
        string json = "{\"device\":\"" + PlayerPrefs.GetString("UUID") + "\",\"id\":\"" + _id + "\"}";
        Debug.Log(json);
        yield return GameDirector.WebReqPost("detail",json);
        director.GetComponent<GameDirector>().MoveScene("ScorePage","DetailPage");
    }

    public static IEnumerator GetScoreData(){
        string json = "{\"device\":\"" + PlayerPrefs.GetString("UUID") + "\",\"level\":" + GameDirector.userdata.level + "}";
        Debug.Log(json);
        yield return GameDirector.WebReqPost("score",json);
        Debug.Log(GameDirector.GetResponse());
    }

    [System.Serializable]
    public class ScoreData{
        public int[] id_list;
        public string[] title_list;

        public static ScoreData Deserialize(string json){
            return JsonUtility.FromJson<ScoreData>(json);
        }
    }
}
