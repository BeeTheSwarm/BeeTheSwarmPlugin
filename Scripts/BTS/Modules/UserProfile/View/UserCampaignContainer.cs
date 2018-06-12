using UnityEngine;
using System.Collections;
using BTS;
using System;
using System.Collections.Generic;

public class UserCampaignContainer : BaseView, IPostlistContainer {

    [SerializeField]
    private Transform m_itemsParent;
    [SerializeField]
    private FeedsListItem m_itemsReference;
    [SerializeField]
    private GameObject m_viewAllPosts;
    [SerializeField]
    private GameObject m_deleteCampaign;
    [SerializeField]
    private GameObject m_createCampaign;
    private ObservableList<PostViewModel> m_viewModel;
    private List<FeedsListItem> m_items = new List<FeedsListItem>();
    [SerializeField]
    private GameObject m_editButtonsContainer; 

    private void AddPost(PostViewModel obj) {
        if (!gameObject.activeInHierarchy) {
            gameObject.SetActive(true);
        }
        AddItem(obj);
        SetButtonsVisibility();
    }

    private void RemovePost(PostViewModel obj) {
        FeedsListItem item = m_items.Find(listItem => { return listItem.Model == obj; });
        if (item != null) {
            item.Unsubscribe();
            m_items.Remove(item);
            Destroy(item.gameObject);
        }
        SetButtonsVisibility();
    }

    private void SetButtonsVisibility() {
        m_deleteCampaign.SetActive(m_items.Count != 0);
        m_viewAllPosts.SetActive(m_items.Count != 0);
        m_createCampaign.SetActive(m_items.Count == 0);
        m_editButtonsContainer.SetActive(m_items.Count != 0);
    }

    private void AddItem(PostViewModel postModel) {
        FeedsListItem item = Instantiate(m_itemsReference);
        item.SetModel(postModel);
        item.transform.SetParent(m_itemsParent, false);
        item.transform.SetSiblingIndex(1);
        m_items.Add(item);
    }

    public void SetViewModel(ObservableList<PostViewModel> viewModel) {
        m_viewModel = viewModel;
        m_viewModel.OnAdd += AddPost;
        m_viewModel.OnRemove += RemovePost;
        m_viewModel.OnClear += ClearPosts;
        SetButtonsVisibility();
    }
    
    private void ClearPosts() {
        m_items.ForEach(item => {
            item.Unsubscribe();
            Destroy(item.gameObject);
        });
        m_items.Clear();
        SetButtonsVisibility();
    }

    
}
