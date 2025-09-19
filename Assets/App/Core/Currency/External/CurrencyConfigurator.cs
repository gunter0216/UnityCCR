using App.Common.FSM.External;
using App.Core.Currency.External.Energy;
using App.Core.Currency.External.Soft;
using App.Core.Startups.External;
using App.Core.Startups.External.Attributes;
using App.Core.Startups.External.Constants;

namespace App.Core.Currency.External
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