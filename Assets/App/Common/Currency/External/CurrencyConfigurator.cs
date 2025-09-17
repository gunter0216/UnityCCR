using App.Common.Autumn.Runtime.Attributes;
using App.Common.Autumn.Runtime.Collection;
using App.Common.FSM.External;
using App.Core.Startups.External;
using UnityEngine;

namespace Core.Currency.External
{
    [Configurator(ContextConstants.GlobalContext)]
    public class CurrencyConfigurator : Configurator
    {
        public override void Configuration()
        {
            Container.BindInterfacesAndSelfTo<EnergyCurrencyController>().AsSingle();
            Container.BindInterfacesAndSelfTo<SoftCurrencyController>().AsSingle();
            
            FsmRegistrar.Register<EnergyCurrencyController>(FSMStage.StartInitStage, 100);
            FsmRegistrar.Register<SoftCurrencyController>(FSMStage.StartInitStage, 100);
            
            DataRegistrar.Register<EnergyCurrencyData>();
            DataRegistrar.Register<SoftCurrencyData>();
        }
    }
}