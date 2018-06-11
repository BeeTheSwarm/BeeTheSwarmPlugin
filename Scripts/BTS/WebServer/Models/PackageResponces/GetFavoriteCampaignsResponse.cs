using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GetFavoriteCampaignsResponse : PackageResponse
{
    public List<CampaignItem> CampaignsList { get; private set; }
    public override void Parse(Dictionary<string, object> data)
    {
        CampaignsList = new List<CampaignItem>();
        var campaignsSource = (List<object>)data["campaigns"];
        foreach (object obj in campaignsSource)
        {
            CampaignItem item = new CampaignItem();
            item.ParseJSON((Dictionary<string, object>)obj);
            PostItem post = new PostItem();
            post.ParseJSON((Dictionary<string, object>)obj);
            item.AddPost(post);
            CampaignsList.Add(item);
        }
    }
}
