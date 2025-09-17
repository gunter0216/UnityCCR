using App.Common.Autumn.Runtime.Attributes;
using App.Common.Autumn.Runtime.Collection;
using App.Common.FSM.External;
using App.Core.Startups.External;

namespace App.Game.SpriteLoaders.External
{
    [Configurator(ContextConstants.GlobalContext)]    
    public class SpriteLoaderConfigurator : Configurator
    {
        public override void Configuration()
        {
            Container.BindInterfacesAndSelfTo<SpriteLoader>().AsSingle();
            
            FsmRegistrar.Register<SpriteLoader>(FSMStage.StartInitStage, 0);
        }
    }
}