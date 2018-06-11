using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS {
    public class CampaignModel : DataModel {
        public int Id { get; private set; }
        public string CharityId { get; private set; }
        public string Address { get; private set; }
        public string Title { get; private set; }
        public string Website { get; private set; }
        public int Category { get; private set; }
        public int Level { get; private set; }
        public int Funded { get; private set; }
        public UserModel User { get; private set; }
        public event Action AddedToFavorite = delegate { };
        public event Action RemovedFromFavorite = delegate { };
        public event Action OnCampaignUpdated = delegate { };
        public bool IsFavorite { get; private set; }

        public override void ParseJSON(Dictionary<string, object> responseData) {
            Id = int.Parse(responseData["id"].ToString());
            CharityId = responseData["charity_id"].ToString();
            Address = responseData["address"].ToString();
            Title = responseData["title"].ToString();
            Website = responseData["website"].ToString();
            Category = int.Parse(responseData["category"].ToString());
            Level = int.Parse(responseData["level"].ToString());
            Funded = int.Parse(responseData["funded"].ToString());
            User = new UserModel();
            User.ParseJSON((Dictionary<string, object>)responseData["user"]);
            IsFavorite = int.Parse(responseData["favourite"].ToString()) == 1;
        }

        internal void Update(CampaignModel campaign) {
            Title = campaign.Title;
            CharityId = campaign.CharityId;
            Address = campaign.Address;
            Website = campaign.Website;
            Category = campaign.Category;
            Level = campaign.Level;
            Funded = campaign.Funded;
            OnCampaignUpdated.Invoke();
        }


        internal void SetIsFavorite(bool value) {
            if (!IsFavorite && value) {
                IsFavorite = true;
                AddedToFavorite.Invoke();
            }

            if (IsFavorite && !value) {
                IsFavorite = false;
                RemovedFromFavorite.Invoke();
            }
        }

    }
}