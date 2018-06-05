using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_CreatePost : BTS_BasePackage<GetPostResponse> {

		public const string PackId = "CreatePost";

        private string m_title;
        private string m_description;
        private Texture2D m_image = null;

        public BTS_CreatePost(string title, string description, Texture2D image) : base (PackId) {
            m_title = title;
            m_description = description;
            m_image = image;
		}
        
        public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> requestFields =  new Dictionary<string, object>();
            requestFields.Add("title", m_title);
            requestFields.Add("description", m_description);
            requestFields.Add("image", "data:image/png;base64,"+Convert.ToBase64String(m_image.EncodeToPNG()));
            return requestFields;
		}
        
		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}