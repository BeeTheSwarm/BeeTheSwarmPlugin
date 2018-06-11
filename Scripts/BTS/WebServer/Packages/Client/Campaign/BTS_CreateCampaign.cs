using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_CreateCampaign : BTS_BasePackage<GetPostsResponse> {

		public const string PackId = "CreateCampaign";

        private string m_campaignTitle;
        private string m_postTitle;
        private string m_description;
        private string m_address;
        private string m_website;
        private int m_category;
        private Texture2D m_image;
        private string packId;

        public BTS_CreateCampaign(string campaignTitle, int category, string website, string postTitle, string postDescription, Texture2D image) : base (PackId) {
	        m_campaignTitle = campaignTitle;
	        m_postTitle = postTitle;
            m_description = postDescription;
            m_website = website;
            m_category = category;
            m_image = image;
        }

        public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> requestFields =  new Dictionary<string, object>();
            requestFields.Add("title", m_campaignTitle);
            requestFields.Add("address", "some address");
            requestFields.Add("website", m_website);
            requestFields.Add("category", m_category);
            Dictionary<string, object> postInfo = new Dictionary<string, object>();
            postInfo.Add("title", m_postTitle);
            postInfo.Add("description", m_description);
            postInfo.Add("image", "data:image/png;base64,"+Convert.ToBase64String(m_image.EncodeToPNG()));
            requestFields.Add("post", postInfo);
            return requestFields;
		}
        
		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}