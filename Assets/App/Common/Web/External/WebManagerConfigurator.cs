using App.Core.Startups.External;
using App.Core.Startups.External.Attributes;
using App.Core.Startups.External.Constants;

namespace App.Common.Web.External
{
    [Configurator(ContextConstants.GlobalContext)]    
    public class WebManagerConfigurator : Core.Startups.External.Configurator
    {
        public override void Configuration()
        {
            Container.BindInterfacesAndSelfTo<WebRequestManager>().AsSingle();
        }
    }
}