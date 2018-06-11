namespace BTS {
    public class GetSettingsService : BaseNetworkService<GetSettingResponce>, IGetSettingService {
        [Inject] private ISettingsModel m_settings;

        public void Execute() {
            SendPackage(new GetSettingsPackage());
        }

        protected override void HandleSuccessResponse(GetSettingResponce data) {
            data.Settings.ForEach(settingsValue => { m_settings.SaveSetting(settingsValue.Type, settingsValue.Value); });
        }
    }
}