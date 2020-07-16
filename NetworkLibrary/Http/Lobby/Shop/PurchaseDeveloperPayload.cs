using System;

namespace NetworkLibrary.Http.Lobby.Shop
{
    [Serializable]
    public class PurchaseDeveloperPayload
    {
        public string ServiceId;
        public int ShopModelId;
        public int ProductId;
        public byte[] SerializedProduct;
    }
}