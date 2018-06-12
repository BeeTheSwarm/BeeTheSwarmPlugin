using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BTS
{
    public interface ICampaignCategoriesModel : IModel
    {
        event Action OnCategoriesLoaded;
        List<CategoryModel> GetCategories();
        CategoryModel GetCategory(int id);
        void SetCategories(List<CategoryModel> categories);
    }
}