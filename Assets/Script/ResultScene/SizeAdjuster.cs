using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SizeAdjuster : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int id;
    float cMaxHeight,this_y;
    GameObject parent,line;
    private bool is_debug = true;
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        transform.Find("Special").gameObject.SetActive(false);
        line = gameObject;
        cMaxHeight = 0;
        for(int i = 0; i < transform.childCount; i++){
            var child = transform.GetChild(i);
            if(cMaxHeight < child.GetComponent<RectTransform>().sizeDelta.y){
                cMaxHeight = child.GetComponent<RectTransform>().sizeDelta.y;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Find("ID").GetComponent<TextMeshProUGUI>().text = GameDirector.GetId(id).ToString();
        transform.Find("Answer").GetComponent<TextMeshProUGUI>().text = GameDirector.GetAnswer(id);
        transform.Find("UAnswer").GetComponent<TextMeshProUGUI>().text = GameDirector.GetUAnswer(id);
        if(GameDirector.correct_list[id] && GameDirector.second_list[id] <= 30){
            transform.Find("Special").gameObject.SetActive(true);
        }
        
        parent.GetComponent<LineManager>().SetHeight(id,400);
        //高さに合わせて、位置を変更
        //localPotionを使うことに注意
        Vector3 _pos = gameObject.transform.localPosition;
        this_y = parent.GetComponent<LineManager>().GetHeight(id);
        _pos.y = this_y;
        gameObject.transform.localPosition = _pos;
        if(id == 6){
            Debug.Log(gameObject.transform.localPosition.y);
            parent.GetComponent<LineManager>().SetLastHeight(gameObject.transform.localPosition.y - cMaxHeight/2 - 200);
        }
    }
}
