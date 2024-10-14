using Game.UI.Data;
using UnityEngine;
using Zenject;

namespace Game.Tips
{
    public abstract class TrainingTrigger : MonoBehaviour
    {
        [SerializeField] private TrainingInfo _trainingInfo;
        [SerializeField] private bool _toCallOnce = true;

        protected GameTrainingController TrainingController;

        private bool _isCalled = false;

        [Inject]
        private void Init(GameTrainingController gameTrainingController)
        {
            TrainingController = gameTrainingController;
        }

        protected void StartTraining()
        {
            if (_toCallOnce && _isCalled)
                return;

            TrainingController.StartTraining(_trainingInfo);

            _isCalled = true;
        }
    }
}