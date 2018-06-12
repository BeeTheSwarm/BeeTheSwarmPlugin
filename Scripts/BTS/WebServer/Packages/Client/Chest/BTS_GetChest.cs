namespace BTS  {
    public class BTS_GetChest : BTS_BasePackage<ChestCountResponse> {
        public const string PackId = "GetChest";

        public BTS_GetChest() : base(PackId) {
        }
    }
}