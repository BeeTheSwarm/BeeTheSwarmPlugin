using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace BTS
{
    public class CampaignCategoriesModel : ICampaignCategoriesModel
    {
        private List<CategoryModel> m_data = new List<CategoryModel>();

        public event Action OnCategoriesLoaded = delegate { };

        public List<CategoryModel> GetCategories()
        {
            return m_data;
        }

        public CategoryModel GetCategory(int id)
        {
            return m_data.Find(category => { return category.Id == id; });
        }

        public void SetCategories(List<CategoryModel> categories)
        {
            m_data = categories;
            OnCategoriesLoaded.Invoke();
        }
    }
}