using System.Threading.Tasks;
using Code.Scenes.LobbyScene.Scripts;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Отвечает за наполнение магазина. Наполнение магазина зависит от набора кораблей в аккаунте.
    /// </summary>
    public class ShopFactoryService
    {
        public async Task<ShopModel> GetShopModelAsync([NotNull] string playerServiceId)
        {
            return new ShopModelBuilder().Create();
        }
    }
}