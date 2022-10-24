using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RankingScene : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject director,LSlider,LText,LScore,Canvas;
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
    }
}
