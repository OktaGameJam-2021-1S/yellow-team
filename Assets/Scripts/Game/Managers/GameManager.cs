using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers {
    public class GameManager : MonoBehaviour
    {                
        // Start is called before the first frame update
        void Start()
        {
            GameApplicationContext.instance.UIManager.OpenGameScreen(new GameScreenView.Payload());
        }
    }
}