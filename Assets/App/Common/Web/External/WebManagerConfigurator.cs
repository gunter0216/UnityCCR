using App.Common.Autumn.Runtime.Attributes;
using App.Core.Startups.External;

namespace App.Common.Web.External
{
    [Configurator(ContextConstants.GlobalContext)]    
    public class WebManagerConfigurator : Autumn.Runtime.Collection.Configurator
    {
        public override void Configuration()
        {
            Container.BindInterfacesAndSelfTo<WebRequestManager>().AsSingle();
        }
    }
}