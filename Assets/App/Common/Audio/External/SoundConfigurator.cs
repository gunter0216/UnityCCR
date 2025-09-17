using App.Common.Autumn.Runtime.Attributes;
using App.Common.FSM.External;
using App.Core.Startups.External;

namespace App.Common.Audio.External
{
    [Configurator(ContextConstants.CoreContext)]
    public class SoundConfigurator : Autumn.Runtime.Collection.Configurator
    {
        public override void Configuration()
        {
            Container.BindInterfacesAndSelfTo<SoundManager>().AsSingle();
            
            FsmRegistrar.Register<SoundManager>(FSMStage.CoreInitStage, 0);
        }
    }
}