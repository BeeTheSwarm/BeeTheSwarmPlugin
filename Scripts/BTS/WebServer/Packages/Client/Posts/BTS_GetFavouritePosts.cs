using UnityEngine;
using System;
using System.Collections.Generic;

namespace BTS {

	internal class BTS_GetFavouritePosts : BTS_BasePackage<GetPostsResponse> {

		public const string PackId = "GetFavouritePosts";
        private int m_limit;
        private int m_offset;
		public BTS_GetFavouritePosts(int offset, int limit) : base(PackId)
        {
            m_limit = limit;
            m_offset = offset;
        }

        public override Dictionary<string, object> GenerateData () {
			Dictionary<string, object> requestFields =  new Dictionary<string, object>();
            requestFields.Add("limit", m_limit);
            requestFields.Add("offset", m_offset);
            return requestFields;
		}
        
		public override Int32 Timeout {
			get {
				return 5;
			}
		}
	}
}