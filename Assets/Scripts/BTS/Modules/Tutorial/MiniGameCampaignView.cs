using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
	public class MiniGameCampaignView : BaseView, IPostlistContainer {

		[SerializeField] private Transform m_itemsParent;
		[SerializeField] private FeedsListItem m_itemsReference;

		private ObservableList<PostViewModel> m_viewModel;
		private List<FeedsListItem> m_items = new List<FeedsListItem>();

		private void AddPost(PostViewModel obj)
		{
			if (!gameObject.activeInHierarchy)
			{
				gameObject.SetActive(true);
			}
			AddItem(obj);
		}

		private void RemovePost(PostViewModel obj)
		{
			FeedsListItem item = m_items.Find(listItem => { return listItem.Model == obj; });
			if (item != null)
			{
				item.Unsubscribe();
				m_items.Remove(item);
				Destroy(item.gameObject);
			}
            
		}

		private void AddItem(PostViewModel postModel)
		{
			FeedsListItem item = Instantiate(m_itemsReference);
			item.SetModel(postModel);
			item.transform.SetParent(m_itemsParent, false);
			item.transform.SetAsLastSibling();
			m_items.Add(item);
		}

		public IPostlistContainer GetPostlistContainer() {
			return this;
		}

		public void SetViewModel(ObservableList<PostViewModel> viewModel) {
			m_viewModel = viewModel;
			m_viewModel.OnAdd += AddPost;
			m_viewModel.OnRemove += RemovePost;
			m_viewModel.OnClear += ClearHandler;
		}

		private void ClearHandler() {
			m_items.ForEach(item => {
				item.Unsubscribe();
				Destroy(item.gameObject);
			});
			m_items.Clear();
		}
	}
}
