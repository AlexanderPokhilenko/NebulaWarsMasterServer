using AmoebaGameMatcherServer.Controllers;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using DataLayer;
using NUnit.Framework;

namespace IntegrationTests
{
    /// <summary>
    /// Отвечает за очистку БД после каждого теста.
    /// </summary>
    internal class BaseIntegrationFixture
    {
        protected ApplicationDbContext Context => SetUpFixture.DbContext;
        protected AccountDbReaderService AccountDbReaderService => SetUpFixture.AccountReaderService;
        protected NotShownRewardsReaderService NotShownRewardsReaderService => SetUpFixture.NotShownRewardsReaderService;
        protected AccountFacadeService AccountFacadeService => SetUpFixture.AccountFacadeService;
        protected LobbyModelFacadeService LobbyModelFacadeService => SetUpFixture.LobbyModelFacadeService;
        protected LobbyModelController LobbyModelController => SetUpFixture.LobbyModelController;
        
        [SetUp]
        public void ResetChangeTracker()
        {
            SetUpFixture.SetUp();
        }
    }
}