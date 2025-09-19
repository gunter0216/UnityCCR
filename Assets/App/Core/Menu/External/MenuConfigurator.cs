using App.Common.FSM.External;
using App.Core.Startups.External;
using App.Core.Startups.External.Attributes;
using App.Core.Startups.External.Constants;

namespace App.Core.Menu.External
{
    [Configurator(ContextConstants.CoreContext)]    
    public class MenuConfigurator : Configurator
    {
        public override void Configuration()
        {
            Container.BindInterfacesAndSelfTo<MenuController>().AsSingle();
            
            FsmRegistrar.Register<MenuController>(FSMStage.CoreInitStage, 10000);
        }
    }
}