using App.Common.AssetSystem.Runtime;
using UnityEngine;

namespace App.Core.Menu.External.Presenter.States.Dogs.Config
{
    public class DogsConfigController
    {
        private const string m_AssetKey = "DogsConfig";

        private readonly IAssetManager m_AssetManager;

        private DogsConfig m_Config;

        public DogsConfigController(IAssetManager assetManager)
        {
            m_AssetManager = assetManager;
        }

        public bool Initialize()
        {
            var config = m_AssetManager.LoadSync<DogsConfig>(new StringKeyEvaluator(m_AssetKey));
            if (!config.HasValue)
            {
                Debug.LogError("[DogsConfigController] In method Initialize, error load DogsConfig.");
                return false;
            }

            m_Config = config.Value;

            return true;
        }
     
        public long GetMaxBreedsCount()
        {
            return m_Config.MaxBreedsCount < 0 ? 10 : m_Config.MaxBreedsCount;
        }
    }
}