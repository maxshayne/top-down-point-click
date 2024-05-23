using Game.Data;
using Game.PlayerMovement;
using JetBrains.Annotations;
using UnityEngine.AI;

namespace Game.Gameplay
{
    [UsedImplicitly]
    public class SaveLoadPresenter
    {
        public SaveLoadPresenter(NavMeshAgent playerAgent, IPathProvider pathProvider)
        {
            _playerAgent = playerAgent;
            _pathProvider = pathProvider;
        }
        
        public void Load(SaveData data)
        {
            if (data == null)
            {
                _saveData = new SaveData();
                return;
            }
            _saveData = data;
            _saveData.GetPoints().ForEach(_pathProvider.AddPointToPath);
            
            var tr = _playerAgent.transform;
            tr.localPosition = _saveData.LocalPosition;
            tr.localEulerAngles  = _saveData.LocalEulerRotation;
            tr.localScale  = _saveData.LocalScale;
        }

        public SaveData GetLatestSave()
        {
            _saveData.SetPoints(_pathProvider.GetPath());
            var transform = _playerAgent.transform;
            _saveData.LocalPosition = transform.localPosition;
            _saveData.LocalEulerRotation = transform.localEulerAngles;
            _saveData.LocalScale = transform.localScale;
            return _saveData;
        }
        
        private readonly NavMeshAgent _playerAgent;
        private readonly IPathProvider _pathProvider;
        
        private SaveData _saveData;
    }
}