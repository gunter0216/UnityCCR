using App.Common.Autumn.Runtime.Attributes;
using App.Common.Autumn.Runtime.Collection;
using App.Common.Data.Runtime;
using App.Common.FSM.External;
using App.Core.Startups.External;
using Zenject;

namespace App.Menu.UI.External
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