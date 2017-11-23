using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public enum ShopItemState{

	None,
	Available,
	Unavailable,
	Bought,
	Equipped
};

public class ShopItem: MonoBehaviour{

	[SerializeField] private int _id;
	[SerializeField] private ShopItemState _state;
	[SerializeField] private string _name;
	[SerializeField] private int _price;
	[SerializeField] int _parameter;
	[SerializeField] private Color32 _color;

	ShopController _shopController;

	[SerializeField] GameObject _buyImage;
	[SerializeField] Image _equipImage;
	[SerializeField] Image _unequipImage;


	///////Get/Set

	public int ID{

		get{ 
			return _id;
		}
	}

	public string Name{
			
		get{ 
			return _name;
		}
	}

	public int Price {

		get{ 
			return _price;
		}
	}

	public int Parameter {

		get{ 
			return _parameter;
		}
	}

	public Color32 Color {

		get{ 
			return _color;
		}
	}
	/////////Unity Funtcions


	void Start(){

		//add item in shop
		_shopController = ShopController.Instance;
		if(!_shopController.TryAddItem(this)){
				//Debug.LogError(ToString());

			}
	}

	public void PurchaseItem(){
		//_shopController.TryPurchaseItem (this);
	}

	public void SetState(ShopItemState state){
		_state = state;

		switch (state) {
		case ShopItemState.None:
			Debug.LogError ("ShopItem.SetState() error, state none " + this._id + " " + this.name);
			break;
		
		case ShopItemState.Available:
			_buyImage.SetActive (true);
			_equipImage.enabled = false;
			_unequipImage.enabled = false;
			break;

		case ShopItemState.Unavailable:
			_buyImage.SetActive (false);
			_equipImage.enabled = false;
			_unequipImage.enabled = false;
			break;

		case ShopItemState.Bought:
			_buyImage.SetActive (false);
			_equipImage.enabled = true;
			_unequipImage.enabled = false;
			break;

		case ShopItemState.Equipped:
			_buyImage.SetActive (false);
			_equipImage.enabled = false;
			_unequipImage.enabled = true;
			break;


		}
	}

	public override string ToString ()
	{
		return this._id + " " + this._name + " " + " " + this._state + " " + this._price;
	}

	private void updateUI(){
		
	}
}




