﻿using Infrastructure.SceneManagement;
using JetBrains.Annotations;
using VContainer.Unity;

namespace Game.Boot
{
    [UsedImplicitly]
    public class BootEntry : IInitializable
    {
        private readonly ISceneLoader _sceneLoader;

        public BootEntry(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public void Initialize()
        {
            _sceneLoader.LoadScene(SceneKey.Menu);
        }
    }
}