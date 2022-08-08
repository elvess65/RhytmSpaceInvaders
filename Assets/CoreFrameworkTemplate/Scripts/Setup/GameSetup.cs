﻿using CoreFramework;
using CoreFramework.Input;

namespace inGame.AbstractShooter.Setup
{
    public class GameSetup : IGameSetup
    {
        private IGameSetup[] m_Setups;

        public GameSetup(params IGameSetup[] setups)
        {
            m_Setups = setups;
        }

        public void Setup()
        {
            Dispatcher dispatcher = Dispatcher.Instance;

            // Models
            dispatcher.CreateModel<UpdateModel>();
            dispatcher.CreateModel<CameraModel>();
            dispatcher.CreateModel<InputModel>();

            // Controllers
            dispatcher.CreateController<UpdateController>();
            dispatcher.CreateController<InputController>();

            for (int i = 0; i < m_Setups.Length; i++)
            {
                m_Setups[i].Setup();
            }

            dispatcher.InitializeComplete();
        }
    }
}
