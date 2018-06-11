using System;

namespace BTS {
    public class LoadInitDataService: BaseService, ILoadInitDataService {
        [Inject] private IGetImpactService m_getImpactService;
        [Inject] private IGetCampaignCategoriesService m_getCampaignCategoriesService;
        [Inject] private IGetCampaingLevelsService m_getCampaingLevelsService;
        [Inject] private IGetCountersService m_getCounters;
        [Inject] private IGetSettingService m_getSettings;
        
        private Action m_callback;

        public bool Loaded { get; private set; }
        public event Action OnLoad = delegate {  };

        public void Execute(Action callback) {
            m_callback = callback;
            m_getCampaingLevelsService.Execute();
            m_getImpactService.Execute();
            m_getCampaignCategoriesService.Execute();
            m_getSettings.Execute();
            m_getCounters.OnSuccessFinish += CountersReceivedHandler;
            m_getCounters.Execute();
        }

        private void CountersReceivedHandler() {
            m_getCounters.OnSuccessFinish -= CountersReceivedHandler;
            m_callback.Invoke();
            Loaded = true;
            OnLoad.Invoke();
        }
    }
}