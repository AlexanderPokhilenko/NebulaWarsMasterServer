using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Отвечает за наполнение меню со скинами в магазине.
    /// Предлагаемый набор скинов ограничен корабляеми, которые есть у аккаунта.
    /// Предлагаемый набор может зависеть от того, какие корабли предпочитает игрок.
    /// Предлагаемый набор может зависет от того, на какие корабли игрок уже купил скины.
    /// </summary>
    public class ShopSkinsService
    {
        public async Task<UiContainerModel> GetOrCreate([NotNull] string playerServiceId)
        {
            throw new NotImplementedException();
        }
    }
}