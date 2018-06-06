using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PostItem
{
    public int PostId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string ImageURL { get; private set; }
    public string ImagePreviewURL { get; private set; }
    public int TimeStart { get; private set; }
    public int Category { get; private set; }
    public int CharityId { get; private set; }
    public int CampaignId { get; private set; }
    public int Funded { get; private set; }
    public int MoneyEarned { get; private set; }
    public string Website { get; private set; }
    public int Level { get; private set; }
    public int Comments { get; private set; }
    public int UserId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Name { get; private set; }
    public int Views { get; private set; }
    public int Type { get; private set; }
    public string Video { get; private set; }

    public void ParseJSON(Dictionary<string, object> responseData)
    {
        PostId = int.Parse(responseData["post_id"].ToString());
        Title = responseData["title"].ToString();
        Description = responseData["description"].ToString();
        ImageURL = responseData["image"].ToString();
        TimeStart = int.Parse(responseData["time_start"].ToString());
        Category = int.Parse(responseData["category"].ToString());
        CharityId = int.Parse(responseData["charity_id"].ToString());
        Funded = int.Parse(responseData["funded"].ToString());
        Website =responseData["website"].ToString();
        UserId = int.Parse(responseData["user_id"].ToString());
        Name = responseData["name"].ToString();
        Views = int.Parse(responseData["views"].ToString());
        Type = int.Parse(responseData["type"].ToString());
    }

    internal void SetImageUrls(string imageBaseUrl, string imagePreviewBaseUrl)
    {
        ImageURL = imageBaseUrl + ImageURL;
        ImagePreviewURL = imagePreviewBaseUrl + ImageURL;
    }
}
