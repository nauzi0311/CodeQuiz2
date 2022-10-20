using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ranking : MonoBehaviour
{
    [SerializeField]
    GameObject prefab_RankingList;
    static RankingData _data;
    List<GameObject> objlist = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        _data = RankingData.Deserialize(GameDirector.GetResponse());
        Vector3 _pos = new Vector3(0.0f,-95.0f,0.0f);
        for(int i = 0; i < _data.ranking.Length; i++){
            objlist.Add(Instantiate(prefab_RankingList));
            var obj = objlist[i];
            obj.transform.SetParent(transform,false);
            obj.transform.Find("Count").GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
            obj.transform.Find("Level").GetComponent<TextMeshProUGUI>().text = "Lv" + _data.ranking[i].level;
            obj.transform.Find("UserName").GetComponent<TextMeshProUGUI>().text = _data.ranking[i].username;
            obj.transform.localPosition = _pos + new Vector3(0.0f,-190*i,0);
            Debug.Log(obj.transform.localPosition.y);
            // if(i == 0){
            //     obj.GetComponent<CourseItem>().available = true;
            // }
        }
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x,190*_data.ranking.Length-95);
        if(_data.count <= 3){
            string[] color = {"#ffd700","#c9caca","#815a2b"};
            GameObject.Find("Ranking").GetComponent<TextMeshProUGUI>().text = "<color=" + color[_data.count-1] + ">" + _data.count.ToString() + "</color>";
        }else{
            GameObject.Find("Ranking").GetComponent<TextMeshProUGUI>().text = _data.count.ToString();
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static IEnumerator RankingPost(){
        string json = "{\"device\":\"" + PlayerPrefs.GetString("UUID") + "\"}";
        yield return GameDirector.WebReqPost("rank",json);
    }

    [System.Serializable]
    class UserRank{
        public string device;
        public string username;
        public int level;
        public int exp;
    }

    [System.Serializable]
    class RankingData{
        public UserRank[] ranking;
        public int count;

        public static RankingData Deserialize (string json){
            return JsonUtility.FromJson<RankingData>(json);
        }
    }
}
