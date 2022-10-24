using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BadgeScene : MonoBehaviour
{
    GameObject director,LSlider,LText,LScore,Canvas;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.Find("GameDirector");
        LSlider = GameObject.Find("LevelSlider");
        LText = GameObject.Find("LevelText");
        LScore = GameObject.Find("LevelScore");
        director.GetComponent<GameDirector>().LevelReload(
            LSlider.GetComponent<Slider>(),
            LText.GetComponent<TextMeshProUGUI>(),
            LScore.GetComponent<TextMeshProUGUI>()
        );
        Canvas = GameObject.Find("DetailCanvas");
        Canvas.SetActive(false);
    }

    public void MoveScene(){
        director.GetComponent<GameDirector>().MoveScene("BadgeScene",GameDirector.GetFromPage());
    }

    public void ViewDetail(int num){
        string[] _desc = new string[]{
            "ソフトウェア演習Ⅰ第1回の問題を7種類解く",
            "Level 3になる",
            "Level 5になる",
            "Level 7になる",
            "Level 9になる",
            "Level 10になる",
            "Level 13になる",
            "Level 15になる",
            "3日ログインする",
            "5日ログインする",
            "7日ログインする",
            "10日ログインする",
            "3日連続ログインする",
            "5日連続ログインする",
            "7日連続ログインする",
            "10日連続ログインする",
            "14日連続ログインする",
            "17日連続ログインする",
            "21日連続ログインする",
            "10問正解する",
            "20問正解する",
            "30問正解する",
            "50問正解する",
            "70問正解する",
            "100問正解する",
            "これより手前のバッジを全て獲得する",
            "Level 20になる",
            "Level 25になる",
            "Level 30になる"
        };
        TextMeshProUGUI description = Canvas.transform.Find("Description").gameObject.GetComponent<TextMeshProUGUI>();
        description.text = _desc[num];
        Canvas.SetActive(true);
    }

    public void DeleteDetail(){
        Canvas.SetActive(false);
    }
}
