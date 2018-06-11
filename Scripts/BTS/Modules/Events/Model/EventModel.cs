using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EventModel
{ 
    public string Title { get; private set; }
    public int Reward { get; private set; }
    public int Score { get; private set; }

    public void ParseJSON(Dictionary<string, object> responseData)
    { 
        Title = responseData["title"].ToString();
        Reward = int.Parse(responseData["reward"].ToString());
        Score = int.Parse(responseData["score"].ToString());
    }
}
