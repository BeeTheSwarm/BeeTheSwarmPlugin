using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BTS {
    public class CategoryModel : DataModel {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public ImageUrlsModel Image { get; private set; }

        public override void ParseJSON(Dictionary<string, object> responseData) {
            Id = int.Parse(responseData["id"].ToString());
            Title = responseData["title"].ToString();
            Image = new ImageUrlsModel();
            Image.ImageUrl = responseData["image"].ToString();
        }
    }
}