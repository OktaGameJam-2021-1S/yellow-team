using Factories;
using Game.UI;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class Boot : MonoBehaviour
    {
        [SerializeField]
        private GameApplicationContext GameContext;
        [SerializeField] 
        private UISpriteFactory SpriteFactory;
        [SerializeField]
        private UIFactory UIFactory;

        private GameUIManager UIManager;
        private LoadingScreenView LoadingView;
        private void Start()
        {
            UIManager = new GameUIManager(GameContext, UIFactory, Toolbox.Instance);
            Toolbox.Instance.Register<GameUIManager>(UIManager);
            Toolbox.Instance.Register<UISpriteFactory>(SpriteFactory);

            GameContext.UIManager = UIManager;

            StartCoroutine(StartLoading());
        }

        IEnumerator StartLoading()
        {                        
            LoadingView = UIManager.OpenLoadingScreen(new UI.LoadingScreenView.Payload());
            for (int i = 0; i < 4; i++)
            {
                LoadingView.BumpProgress(0.25f);
                yield return new WaitForSeconds(0.25f);
            }

            UIManager.OpenGameScreen(new GameScreenView.Payload());

            yield return null;
        }
    }
}
