using Factories;
using Patterns;
using UnityEngine;


namespace Game
{
	public class GameApplicationContext: SingletonMonoBehaviour<GameApplicationContext>
	{		
		public GameUIManager UIManager { get; set; }
		[SerializeField]
		private UISpriteFactory SpriteFactory;
		[SerializeField]
		private UIFactory UIFactory;

		public override void Awake()
		{
			base.Awake();
			
			UIManager = new GameUIManager(this, UIFactory, Toolbox.Instance);
			Toolbox.Instance.Register<GameUIManager>(UIManager);			
			Toolbox.Instance.Register<UISpriteFactory>(SpriteFactory);
		}
	}

}
