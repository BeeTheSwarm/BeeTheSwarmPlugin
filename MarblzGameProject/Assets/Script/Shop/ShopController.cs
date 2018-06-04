using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {

	private const string BOUGHT_ITEMS_TOKEN = "BOUGHT_ITEMS_8";
	private const string EQUIPPED_ITEMS_TOKEN = "EQUIPPED_ITEMS_8";

	private static ShopController _instance;

	public static Action<int> OnItemPurchased = delegate {};
	public static Action<int> OnItemEquipped = delegate{};
	public static Action <int> OnItemUnequipped = delegate {};
	public static Action<int> OnItemPreviewed = delegate {};
    public static event Action OnShowNoCoinsPopup = delegate { };
    public static event Action OnShowCongratsPopup = delegate { };
   
    public static Action OnItemsLoaded = delegate {};

	private Dictionary<int, ShopItem> _items = new Dictionary<int, ShopItem>();

	[SerializeField]private List <int> _purchasedItems;

	[SerializeField] private int currentEquippedItems; 


	private static int colorsAmount;
	private Color32[] BallzColors;

    public GameManager GM;
	public BallControl BC;
	public PlayerPrefsManager PPM;
	public static ShopController Instance{

		get{ 
			return _instance;
		}
	}

    public Dictionary<int, ShopItem> Items{

		get { 
			return _items;
		}
	}

	//////////Unity functions

	private void Awake(){
		DontDestroyOnLoad (this.gameObject);
		_instance = this;
		//PlayerPrefs.DeleteAll();
		Load ();

	}

	private void OnEnable(){
		
	}

	private void OnDisable(){
		

		Save ();
	}

	/////public functions

	//Item
	//----------------------------------------------------

	public bool TryAddItem(ShopItem item){
		if (!_items.ContainsKey (item.ID)) {
			_items.Add (item.ID, item);

			if (_purchasedItems != null && _purchasedItems.Contains (item.ID)) {
				if (currentEquippedItems != null && currentEquippedItems == item.ID) {
					item.SetState (ShopItemState.Equipped);
					BC.ballColor = _items [currentEquippedItems].Color;
					OnItemEquipped (item.ID);

				} else {
					item.SetState (ShopItemState.Bought);
				}
			} else {
				item.SetState (ShopItemState.Available);
			}

			return true;
		} else {
			//Debug.LogError ("error");
		}

		return false;
	}

	public void TryPurchaseItem(ShopItem item){
        
		int price = item.Price;

		if (!_purchasedItems.Contains (item.ID)) {

                     
            if (GM.TrySpendCoins (price)) {

                _purchasedItems.Add (item.ID);
				item.SetState (ShopItemState.Bought);

				OnItemPurchased (item.ID);
				EquipItem (item);
                OnShowCongratsPopup();
                GM.UpdateScoreText();
			} else {
                OnShowNoCoinsPopup();
				Debug.Log("Not enough coins!");
			}
					} else {
					EquipItem(item);
		}
					Save();
	}

	public void EquipItem (ShopItem item){
		
			foreach (int id in _purchasedItems){
				ShopItem eqItem = _items[id];
				_items [eqItem.ID].SetState (ShopItemState.Bought);
			}

		currentEquippedItems = item.ID;

		item.SetState (ShopItemState.Equipped);
		OnItemEquipped (item.ID);

		BC.ballColor = _items [currentEquippedItems].Color;
		BC.initialBall.GetComponent<SpriteRenderer> ().color = _items [currentEquippedItems].Color;
		Save ();
	}

	public void Unequip(ShopItem item){
		
		if(currentEquippedItems == item.ID){
			
			item.SetState (ShopItemState.Bought);

			OnItemUnequipped (item.ID);

			EquipItem (_items [0]);

		}
		Save ();
	}

	///////////////private functions

	private void Save(){

		//bought

		string items = " ";
		foreach (int id in _purchasedItems) {
			items += id.ToString () + ",";
		}
		PlayerPrefs.SetString (BOUGHT_ITEMS_TOKEN, items);

		PlayerPrefs.SetInt (EQUIPPED_ITEMS_TOKEN, currentEquippedItems);
	}

	private void Load(){
		_purchasedItems = new List<int> ();

		if (PlayerPrefs.HasKey (BOUGHT_ITEMS_TOKEN)) {
			string itemsInput = PlayerPrefs.GetString (BOUGHT_ITEMS_TOKEN);

			try{
				string[] itemsStrings = itemsInput.Split(new char[]{ ','});
				foreach(string idString in itemsStrings){
					int res = -1;
					if(int.TryParse (idString, out res)){
						_purchasedItems.Add(int.Parse(idString));
					}
				}
			} catch (Exception exc){
				Debug.LogError("load purchased items error." + exc.Message);
			}
		} else {
			_purchasedItems.Add(0);
			currentEquippedItems = 0;			

			return;
		}

		currentEquippedItems = PlayerPrefs.GetInt (EQUIPPED_ITEMS_TOKEN);

	}

}
