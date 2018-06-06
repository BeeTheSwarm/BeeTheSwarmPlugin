using System.Collections.Generic;
using UnityEngine;

namespace BTS {
    public class CampaignCategoriesView : BaseControlledView<ICampaignCategoriesViewListener>, ICampaignCategoriesView {
        [SerializeField] private Transform m_itemsParent;
        [SerializeField] private CategoryItemView m_itemOrigin;

        public void SetCategories(List<CategoryItemViewModel> items) {
            items.ForEach(item => {
                var view = Instantiate(m_itemOrigin);
                view.SetViewModel(item);
                view.gameObject.SetActive(true);
                view.transform.SetParent(m_itemsParent, false);
                view.transform.SetAsLastSibling();
            });
        }
    }
}