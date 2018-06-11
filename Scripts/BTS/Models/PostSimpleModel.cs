using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
namespace BTS {
    public class PostSimpleModel : DataModel {
        public int Id { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public ImageUrlsModel Image { get; protected set; }
        public int CreatedAt { get; protected set; }
        public int Views { get; protected set; }
        public int Clicks { get; protected set; }

        public override void ParseJSON(Dictionary<string, object> responseData) {
            Id = int.Parse(responseData["id"].ToString());
            Title = responseData["title"].ToString();
            Description = responseData["description"].ToString();
            Image = new ImageUrlsModel();
            Image.ImageUrl = responseData["image"].ToString();
            Image.ThumbnailUrl = responseData["image_thumbnail"].ToString();
            CreatedAt = int.Parse(responseData["created_ts"].ToString());
            Views = responseData["views"] != null ? int.Parse(responseData["views"].ToString()) : 0;
            Clicks = responseData["clicks"] != null ? int.Parse(responseData["clicks"].ToString()) : 0;
        }
    }
}
