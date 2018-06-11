using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_UpdatePost : BTS_BasePackage<GetPostResponse> {

		public const string PackId = "UpdatePost";

        private int m_postId;
        private string m_title;
        private string m_description;
        private Texture2D m_image;
		public BTS_UpdatePost(int postId, string title, string description, Texture2D image) : base (PackId) {
            m_postId = postId;
            m_title = title;
            m_description = description;
			m_image = image;
		}
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> requestFields =  new Dictionary<string, object>();
            requestFields.Add("post_id", m_postId);
            requestFields.Add("title", m_title);
            requestFields.Add("description", m_description);
			if (m_image != null) {
				requestFields.Add("image", "data:image/png;base64,"+Convert.ToBase64String(m_image.EncodeToPNG()));
			}
            return requestFields;
		}

		public override bool AuthenticationRequired {
			get {
				return false;
			}
		}

		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}