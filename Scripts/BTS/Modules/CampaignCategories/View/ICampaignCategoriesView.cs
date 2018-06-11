using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTS {
    public interface ICampaignCategoriesView : IControlledView {
        void SetCategories(List<CategoryItemViewModel> items);
    }

}
