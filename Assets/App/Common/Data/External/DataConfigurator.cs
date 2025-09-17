using App.Common.Autumn.Runtime.Attributes;
using App.Common.Data.Runtime;
using App.Common.FSM.External;
using App.Core.Startups.External;

namespace App.Common.Data.External
{
    [Configurator(ContextConstants.GlobalContext)]    
    public class DataConfigurator : Autumn.Runtime.Collection.Configurator
    {
        public override void Configuration()
        {
            Container.BindInterfacesAndSelfTo<DataSavePathCreator>().AsSingle();
            Container.BindInterfacesAndSelfTo<DataManager>().AsSingle();
            
            FsmRegistrar.Register<DataManager>(FSMStage.StartInitStage, 0);
        }
    }
}