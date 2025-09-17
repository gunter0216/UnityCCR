using App.Common.AssetSystem.Runtime;
using App.Common.Utilities.Utility.Runtime;
using App.Game.Canvases.External;
using App.Menu.UI.External.View;

namespace App.Menu.UI.External.Services
{
    public class MenuViewCreator
    {
        private const string m_AssetKey = "MenuView";
        
        private readonly IAssetManager m_AssetManager;
        private readonly ICanvas m_Canvas;

        public MenuViewCreator(IAssetManager assetManager, ICanvas canvas)
        {
            m_AssetManager = assetManager;
            m_Canvas = canvas;
        }

        public Optional<MenuView> Create()
        {
            var view = m_AssetManager.InstantiateSync<MenuView>(
                new StringKeyEvaluator(m_AssetKey), 
                m_Canvas.GetContent());
            if (!view.HasValue)
            {
                return Optional<MenuView>.Fail();
            }
            
            return view;
        }
    }
}