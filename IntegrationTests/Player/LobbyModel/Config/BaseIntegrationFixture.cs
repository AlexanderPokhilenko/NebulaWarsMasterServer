using AmoebaGameMatcherServer.Controllers.ProfileServer.Lobby;
using AmoebaGameMatcherServer.Experimental;
using AmoebaGameMatcherServer.Services.Experimental;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using DataLayer;
using NUnit.Framework;

namespace IntegrationTests.Player.LobbyModel.Config
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
        protected WarshipImprovementFacadeService WarshipImprovementFacadeService => SetUpFixture.WarshipImprovementFacadeService;
        protected WarshipImprovementCostChecker WarshipImprovementCostChecker => SetUpFixture.WarshipImprovementCostChecker;
        protected DefaultAccountFactoryService DefaultAccountFactoryService => SetUpFixture.DefaultAccountFactoryService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SetUpFixture.Initialize();
        }
        
        [SetUp]
        public void ResetChangeTracker()
        {
            SetUpFixture.SetUp();
        }
    }
}