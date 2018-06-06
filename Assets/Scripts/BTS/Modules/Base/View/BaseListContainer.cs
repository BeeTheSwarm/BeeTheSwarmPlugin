using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BaseListContainer<ItemType, DataType> where ItemType: UnityEngine.Object, IListItem where DataType : IListItemViewModel {
    public Transform m_itemsParent;
    public ItemType m_itemOrigin;
    private List<ItemType> m_items = new List<ItemType>();
    private ObservableList<DataType> m_itemsData;

    public void Init(ItemType itemOrigin, Transform itemsParent) {
        m_itemOrigin = itemOrigin;
        m_itemsParent = itemsParent;
    }
    
    public void SetSource(ObservableList<DataType> itemsData) {
        m_itemsData = itemsData;
        m_itemsData.OnAdd += ItemAddHandler;
        m_itemsData.OnClear += ItemsClearHandler;
        m_itemsData.OnInsert += ItemInsertHandler;
        m_itemsData.OnRemove += ItemRemoveHandler;
        ItemsClearHandler();
        itemsData.Get().ForEach(item => {
            ItemAddHandler(item);
        });
    }

    private void ItemRemoveHandler(DataType data) {
        
    }

    private void ItemInsertHandler(DataType data) {
        ItemType item = CreateItem(data);
        item.transform.SetAsFirstSibling();
        m_items.Insert(0, item);
    }

    private ItemType CreateItem(DataType data) {
        ItemType item = GameObjectInstatiator.InstantiateFromObject(m_itemOrigin);
        item.transform.SetParent(m_itemsParent, false);
        item.SetViewModel(data);
        return item;
    }

    private void ItemsClearHandler() {
        m_items.ForEach(item => {
            UnityEngine.Object.Destroy(item.gameObject);
        });
        m_items.Clear();
    }

    private void ItemAddHandler(DataType data) {
        ItemType item = CreateItem(data);
        item.transform.SetAsLastSibling();
        m_items.Add(item);
    }
}
