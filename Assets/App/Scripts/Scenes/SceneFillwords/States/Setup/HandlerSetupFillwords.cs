using System.Threading.Tasks;
using App.Scripts.Infrastructure.GameCore.States.SetupState;
using App.Scripts.Infrastructure.LevelSelection;
using App.Scripts.Scenes.SceneFillwords.Features.FillwordModels;
using App.Scripts.Scenes.SceneFillwords.Features.FillwordModels.View.ViewGridLetters;
using App.Scripts.Scenes.SceneFillwords.Features.ProviderLevel;

namespace App.Scripts.Scenes.SceneFillwords.States.Setup
{
    public class HandlerSetupFillwords : IHandlerSetupLevel
    {
        private readonly ContainerGrid _containerGrid;
        private readonly IProviderFillwordLevel _providerFillwordLevel;
        private readonly IServiceLevelSelection _serviceLevelSelection;
        private readonly ViewGridLetters _viewGridLetters;

        public HandlerSetupFillwords(IProviderFillwordLevel providerFillwordLevel,
            IServiceLevelSelection serviceLevelSelection,
            ViewGridLetters viewGridLetters, ContainerGrid containerGrid)
        {
            _providerFillwordLevel = providerFillwordLevel;
            _serviceLevelSelection = serviceLevelSelection;
            _viewGridLetters = viewGridLetters;
            _containerGrid = containerGrid;
        }

        private int _lastLevelIndex;
        public Task Process()
        {
            GridFillWords model = _providerFillwordLevel.LoadModel(_serviceLevelSelection.CurrentLevelIndex);

            while (model == null)
            {
                int bias = _lastLevelIndex < _serviceLevelSelection.CurrentLevelIndex ? 1 : -1;
                _serviceLevelSelection.UpdateSelectedLevel(_serviceLevelSelection.CurrentLevelIndex + bias);
                model = _providerFillwordLevel.LoadModel(_serviceLevelSelection.CurrentLevelIndex);
            }

            _viewGridLetters.UpdateItems(model);
            _containerGrid.SetupGrid(model, _serviceLevelSelection.CurrentLevelIndex);
            _lastLevelIndex = _serviceLevelSelection.CurrentLevelIndex;
            return Task.CompletedTask;
        }
    }
}