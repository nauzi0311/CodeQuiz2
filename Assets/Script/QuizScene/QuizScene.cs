using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class QuizScene : MonoBehaviour
{
    public static int _qcount = 0;
    // Start is called before the first frame update
    GameObject question_source,director,correct_panel,incorrect_panel;
    List<Dictionary<string,string>> color_code_list = new List<Dictionary<string,string>>(){
        //color_code1
        new Dictionary<string, string>(){
            {"background","#1E1E1E"},
            {"function","#DCDCAA"},
            {"type","#569CD6"},
            {"object","#4EC9B0"},
            {"special characters1","#FFD700"},
            {"special characters2","#DA70D6"},
            {"special characters3","#179FFF"},
            {"variable","#9CDCFE"},
            {"STLfunctions","#9CDCFE"},
            {"const charstrings","#CE9178"},
            {"const numbers","#B5CEA8"},
            {"comment","#6A9955"},
        },
    };
    Dictionary<int,string> spchar = new Dictionary<int,string>(){
        {0,"special characters1"},
        {1,"special characters2"},
        {2,"special characters3"},
    };

    void Start()
    {
        Debug.Log(++_qcount);
        int color_count_bracket = 0;
        int color_count_curly_bracket = 0;
        var color_theme = color_code_list[0];
        question_source = GameObject.Find("Content");
        director = GameObject.Find("GameDirector");
        correct_panel = GameObject.Find("Correct");
        Debug.Log(correct_panel.name);
        correct_panel.SetActive(false);
        incorrect_panel = GameObject.Find("Incorrect");
        Debug.Log(incorrect_panel.name);
        incorrect_panel.SetActive(false);
        Debug.Log(spchar[(color_count_bracket++)%3]);
        Debug.Log(color_theme[spchar[(color_count_bracket++)%3]]);
        string source = "<color=" + color_theme["type"] + ">int </color><color=" + color_theme["function"] + 
        ">main</color> <color=" + color_theme[spchar[(color_count_bracket++)%3]] + ">(</color><color=" + color_theme[spchar[(--color_count_bracket)%3]] +">)"+ 
        "</color><color=" + color_theme[spchar[(color_count_curly_bracket++)%3]] + "> {" + "</color>\n" +
        "   int"+" a "+"="+" 10;\n" + 
        "   printf(\"%d\",a)\n" + 
        "}\n\nvoid func(){}\n\nvoid test_func_name_is_too_long(){}";
        
        Debug.Log(source);        
        question_source.GetComponent<TextMeshProUGUI>().text = source;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveScene(int ans_num,float time = 0.5f){
        if(ans_num == 0){
            StartCoroutine(DelayScene(false,time));
        }else if(ans_num == 1){
            correct_panel.SetActive(true);
            StartCoroutine(DelayScene(true,time));
        }else{
            incorrect_panel.SetActive(true);
            StartCoroutine(DelayScene(false,time));
        }
    }

    private IEnumerator DelayScene(bool correct,float time = 1.0f){
        yield return new WaitForSeconds(0.5f);
        if(correct){
            if(_qcount == 7){
                director.GetComponent<GameDirector>().MoveScene("QuizPage","ResultPage");
            }else{
                director.GetComponent<GameDirector>().MoveScene("QuizPage","QuizPage",time);
            }
        }else{
            director.GetComponent<GameDirector>().MoveScene("QuizPage","FailurePage",time);
        }
    }
}

/*
color code
background:         1E1E1E
function:           DCDCAA
type:               569CD6
Object:             4EC9B0
special characters: FFD700,DA70D6,179FFF  e.g.)(){}
variable:           9CDCFE
STLfunctions:       9CDCFE
const charstrings:  CE9178
const numbers:      B5CEA8
comment:            6A9955
*/

