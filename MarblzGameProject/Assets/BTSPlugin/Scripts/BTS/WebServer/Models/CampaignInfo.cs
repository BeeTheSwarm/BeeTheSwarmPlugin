using UnityEngine;
using System.Collections;

namespace BTS {

internal class CampaignInfo {

	private int _id; 			//database player id
	private int _type; 			//type of add
	private string _short_text; //small description
	private string _full_text; 	//big description
	private string _src; 		//charity link
	private string _image; 		//image link
	private Sprite _image_sprite;

	public CampaignInfo(int id, int type, string short_text, string full_text, string src, string image) {
		this._id 			= id;
		this._type 			= type;
		this._short_text 	= short_text;
		this._full_text 	= full_text;
		this._src 			= src;
		this._image 		= image;
	}

	public int Id {
		get {
			return _id;
		}
	}

	public int Type {
		get {
			return _type;
		}
	}

	public string Short_text {
		get {
			return _short_text;
		}
	}

	public string Full_text {
		get {
			return _full_text;
		}
	}

	public string Src {
		get {
			return _src;
		}
	}

	public string Image {
		get {
			return _image;
		}
	}

	public Sprite ImageSprite {
		get {
			return _image_sprite;
		}
		set{
			this._image_sprite = value;
		}
	}
}
}