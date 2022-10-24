using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetailScene : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject director,Id,QText,Source,Output;
    void Start()
    {
        director = GameObject.Find("GameDirector");
        Id = GameObject.Find("Id");
        QText = GameObject.Find("QText");
        Source = GameObject.Find("Source");
        Output = GameObject.Find("Output");
        QuestData detail_data = QuestData.Deserialize(GameDirector.GetResponse());

        Id.GetComponent<TextMeshProUGUI>().text = detail_data.title;
        QText.GetComponent<TextMeshProUGUI>().text = detail_data.question;
        string source = detail_data.source;
        source = source.Replace("\t","  ");
        source = source.Replace("???","<color=\"red\">???</color>");
        source = source.Replace("ffd70>","ffd700>");
        Source.GetComponent<TextMeshProUGUI>().text = source;
        string output = detail_data.output;
        output = output.Replace("???","<color=\"red\">???</color>");
        Output.GetComponent<TextMeshProUGUI>().text = output;
    }
}
