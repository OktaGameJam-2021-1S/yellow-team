using Factories;
using Game;
using Game.UI;
using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game
{
    

    public class GameUIManager : UIManager
    {
        public static string UILoadingScreen = "LoadingScreen";

        public GameUIManager(GameApplicationContext context, UIFactory uiFactory, Toolbox serviceLocator): base(context, uiFactory, serviceLocator)
        {

        }

        public override void Push(string sScreenName, object pPayload)
        {
            base.Push(sScreenName, pPayload);

            switch (sScreenName)
            {
                case Constants.UI.UI_LoadingScreen:
                    OpenLoadingScreen(pPayload as LoadingScreenView.Payload);
                    break;

                default:
                    break;
            }
        }

        public TitleView OpenTitleScreen()
        {
            var view = CreateScreen<TitleView>(Constants.UI.UI_TitleScreen, "Prefab_Title", null);
            UIStackManager.PushScreen(view);
            return view;
        }

        public LoadingScreenView OpenLoadingScreen(LoadingScreenView.Payload payload)
        {
            LoadingScreenView view = CreateScreen<LoadingScreenView>(Constants.UI.UI_LoadingScreen, "Prefab_LoadingScreen", payload);
            UIStackManager.PushScreen(view);
            return view;
        }

        public GenericPopup OpenGenericPopup(GenericPopup.Payload pPayload)
        {
            var popup = CreateScreen<GenericPopup>(Constants.UI.UI_GenericPopup, "Prefab_GenericPopup", pPayload);
            UIStackManager.PushPopup(popup);
            return popup;
        }

        public GameScreenView OpenGameScreen(GameScreenView.Payload payload)
        {
            GameScreenView view = CreateScreen<GameScreenView>(Constants.UI.UI_LoadingScreen, "Prefab_GameScreen", payload);
            UIStackManager.PushScreen(view);
            return view;
        }
    }
}
