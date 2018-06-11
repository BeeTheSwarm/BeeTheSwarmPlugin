using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Globalization;
namespace BTS
{
public class ShortFeedsList : BaseFeedsList
{
    [SerializeField]
    private Text m_impact;
    [SerializeField]
    private ShortFeedSublist m_newCampaigns;
    [SerializeField]
    private ShortFeedSublist m_hotCampaigns;
    [SerializeField]
    private ShortFeedSublist m_favouriteCampaigns;
    [SerializeField]
    private ShortFeedSublist m_recentlyUpdatedCampaigns;
    
    private void Awake()
    {
        m_newCampaigns.gameObject.SetActive(false);
        m_hotCampaigns.gameObject.SetActive(false);
        m_favouriteCampaigns.gameObject.SetActive(false);
        m_recentlyUpdatedCampaigns.gameObject.SetActive(false);
    }
    
    internal void SetImpact(float impact)
    {
        CultureInfo ci = new CultureInfo("en-us");
        m_impact.text = impact.ToString("C", ci);
    }
}
}