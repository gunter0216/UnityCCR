using App.Common.Autumn.Runtime.Attributes;
using App.Core.Startups.External;

namespace App.Common.Events.External
{
    [Configurator(ContextConstants.GlobalContext)]
    public class EventManagerConfigurator : Autumn.Runtime.Collection.Configurator
    {
        public override void Configuration()
        {
            Container.Bind<EventManager>().AsSingle();
        }
    }
}