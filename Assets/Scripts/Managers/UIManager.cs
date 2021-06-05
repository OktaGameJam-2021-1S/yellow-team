using System;
using UnityEngine;
using System.Collections.Generic;
using Factories;
using Game;
using UI;

namespace Managers
{
    public interface IUINavigationController
    {
        bool Pop(string name);

        void Push(string screenName, object param);
        void PopScreen();
        string[] GetScreenStack();

        void PopPopup();

        string[] GetPopup();

        int GetTotalViewModels();       // total amount of popups and screens
        bool IsFocused(string name);    // returns true if the given vm name is the currently on top of all vm's (used for Android's back button)
    }

    /// <summary>
    /// Creates ViewModel and attaches to a  given View.
    /// Also knows which view to instantiate based on a name.
    /// </summary>
    public class UIManager : IUINavigationController
    {

        protected UIStackViewManager UIStackManager;	// the stack manager for screens and popups
        protected UIFactory UIFactory;				// the provider for View.
        protected GameApplicationContext ApplicationContext;	// context to inject some dependencies
        protected Toolbox ServiceLocator;


        public UIManager(GameApplicationContext context, UIFactory uiFactory, Toolbox serviceLocator)
        {
            UIStackManager = new UIStackViewManager();
            UIFactory = uiFactory;
            ApplicationContext = context;
            ServiceLocator = serviceLocator;
        }

        protected T CreateScreen<T>(string screenName, string prefabName, object payload)
                where T : BaseView
        {
            var view = UIFactory.CreateView(prefabName, delegate (BaseView pView)
            {
                // inject dependencies:
                BaseView pGameView = pView as BaseView;
                if (pGameView != null)
                {
                    pGameView.SetSpriteFactory(ServiceLocator.GetComponentThatImplements<UISpriteFactory>());
                }
            });

            return (T)view;

        }

        /// <summary>
        /// Closes all popups and screens;
        /// </summary>
        public void CloseAll()
        {
            UIStackManager.PopAll();
        }
        
        #region Implements - IUINavigationController

        public string[] GetPopup()
        {
            throw new System.NotImplementedException();
        }

        public string[] GetScreenStack()
        {
            return UIStackManager.GetScreenStack();
        }

        public virtual void Push(string sScreenName, object pPayload)
        {
            Debug.Log(sScreenName);            
        }

        public bool Pop(string sName)
        {
            bool bPopped = false;
            if (UIStackManager.PopScreen(sName))
            {
                bPopped = true;
            }
            else
            {
                if (UIStackManager.PopPopup(sName))
                {
                    bPopped = true;
                }

            }
            return bPopped;
        }

        public void PopScreen()
        {
            UIStackManager.PopScreen(UIStackManager.GetCurrentScreen());
        }

        public void PopPopup()
        {
            UIStackManager.PopPopup(UIStackManager.GetCurrentPopup());
        }

        public int GetTotalViewModels()
        {
            return UIStackManager.GetTotalPopup() + UIStackManager.GetTotalScreen();
        }

        public bool IsFocused(string sName)
        {

            return UIStackManager.GetFocused().Name == sName;
        }
        #endregion
    }
}