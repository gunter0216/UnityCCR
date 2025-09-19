using App.Common.Autumn.Runtime.Attributes;
using App.Common.FSM.External;
using App.Core.Startups.External;

namespace App.Common.ApplicationQuit.External
{
    [Configurator(ContextConstants.GlobalContext)]
    public class ApplicationQuitConfigurator : Autumn.Runtime.Collection.Configurator
    {
        public override void Configuration()
        {
            Container.BindInterfacesAndSelfTo<ApplicationQuitController>().AsSingle();
            
            FsmRegistrar.Register<ApplicationQuitController>(FSMStage.StartInitStage, 0);
        }
    }
}