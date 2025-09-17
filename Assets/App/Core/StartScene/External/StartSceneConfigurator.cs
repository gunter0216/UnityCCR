using App.Common.Autumn.Runtime.Attributes;
using App.Common.Autumn.Runtime.Collection;
using App.Common.FSM.External;
using App.Core.Startups.External;

namespace App.Core.StartScene.External
{
    [Configurator(ContextConstants.StartContext)]    
    public class StartSceneConfigurator : Configurator
    {
        public override void Configuration()
        {
            Container.BindInterfacesAndSelfTo<StartSceneController>().AsSingle();
            
            FsmRegistrar.Register<StartSceneController>(FSMStage.StartInitStage, 100_100);
        }
    }
}