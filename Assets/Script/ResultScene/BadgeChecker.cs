using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeChecker : MonoBehaviour
{
    static public bool[] Check(UserData _user){
        var _badge = new List<bool>();
        _badge.Add(FirstBadge(_user.correct_id));
        _badge.AddRange(LevelBadge(_user.level));
        _badge.AddRange(DayBadge(_user.date));
        _badge.AddRange(DayContinueBadge(_user.date));
        _badge.AddRange(CorrectCountBadge(_user.correct_count));
        _badge.Add(IsAllBadge(_badge));
        _badge.AddRange(ExtraBadge(_user));
        return _badge.ToArray();
    }

    static bool FirstBadge(List<int> correct){
        int count = 0;
        for(int i = 0; i < correct.Count; i++){
            if(count >= 7) return true;
            if((correct[i] % 1100) < 100){
                count++;
            }
        }
        if(count >= 7) return true;
        return false;
    }

    static List<bool> LevelBadge(int level){
        int[] _level_list = new int[] {3,5,7,9,10,13,15};
        List<bool> result = new List<bool>();
        foreach(int i in _level_list){
            if(i <= level){
                result.Add(true);
                continue;
            }
            result.Add(false);
        }
        return result;
    }

    static List<bool> DayBadge(List<string> date){
        int[] _day_list = new int[] {3,5,7,10};
        List<bool> result = new List<bool>();
        Debug.Log(date.Count); 
        foreach(int i in _day_list){
            if(i <= date.Count){
                result.Add(true);
                continue;
            }
            result.Add(false);
        }
        return result;
    }

    static List<bool> DayContinueBadge(List<string> date){
        int[] _day_continue_list = new int[] {3,5,7,10,14,17,21};
        List<bool> result = new List<bool>();
        for(int i = 0; i < _day_continue_list.Length; i++) result.Add(false);
        DateTime[] _date = new DateTime[date.Count];
        //変換
        for(int i = 0; i < date.Count; i++){
            _date[i] = DateTime.Parse(date[i]);
        }

        for(int i = 0; i < _date.Length; i++){
            for(int j = 0;j < _day_continue_list.Length; j++){
                if(_date.Length - i < _day_continue_list[j])break;
                if(_date[i].AddDays(_day_continue_list[j]-1) == _date[i + _day_continue_list[j] - 1]){
                    result[j] = true;
                }else{
                    break;
                }
            }
        }
        return result;
    } 

    static List<bool> CorrectCountBadge(int correct_count){
        int[] _correct_list = new int[] {10,20,30,50,70,100};
        List<bool> result = new List<bool>();
        foreach(int i in _correct_list){
            if(i <= correct_count){
                result.Add(true);
                continue;
            }
            result.Add(false);
        }
        return result;
    }

    static bool IsAllBadge(List<bool> badge){
        for(int i = 0; i < badge.Count; i++){
            if(!badge[i]) break;
            if(i == badge.Count-1) return true;
        }
        return false;
    }
    static List<bool> ExtraBadge(UserData _userdata){
        int[] _extra_level = new int[] {20,25,30};
        List<bool> result = new List<bool>();
        foreach(int i in _extra_level){
            if(i <= _userdata.level){
                result.Add(true);
                continue;
            }
            result.Add(false);
        }
        return result;
    }
}
