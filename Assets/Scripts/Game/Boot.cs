using Factories;
using Game.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Boot : MonoBehaviour
    {
        [SerializeField]
        private GameApplicationContext GameContext;        
        private LoadingScreenView LoadingView;
        
        public GameUIManager UIManager { get; set; }
        private void Start()
        {
            UIManager = GameContext.UIManager;
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

            SceneManager.LoadScene("Game");

            yield return null;
        }
    }
}
