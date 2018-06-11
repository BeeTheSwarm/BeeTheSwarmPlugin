using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_CreatePostComment : BTS_BasePackage<NoDataResponse> {

		public const string PackId = "CreatePostComment";
        private int m_postId;
        private int m_replyId;
        private string m_text;
		public BTS_CreatePostComment(int postId, string text, int replyId = 0) : base (PackId) {
            m_postId = postId; 
            m_replyId = replyId;
            m_text = text;
		}
		
		public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> requestFields =  new Dictionary<string, object>();
            requestFields.Add("post_id", m_postId);
            requestFields.Add("reply_id", m_replyId);
            requestFields.Add("text", m_text);
            return requestFields;
		}
        
		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}