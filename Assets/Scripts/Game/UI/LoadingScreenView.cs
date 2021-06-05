using System;
using UI;
using Game;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

namespace Game.UI
{
    public class LoadingScreenView: BaseView
    {
        [Header("Animations")]
        [SerializeField] 
        private Slider ProgressBar;
        public class Payload
        {
            
        }

        private void Awake()
        {
            Name = Constants.UI.UI_LoadingScreen;
            SetProgress(0);
        }

        public void SetProgress(float progress)
        {
            if(ProgressBar != null)
            {
                ProgressBar.value = progress;
            }
        }

        public void BumpProgress(float progress)
        {
            if (ProgressBar != null)
            {
                ProgressBar.value += progress;
            }            
        }
    }
}
