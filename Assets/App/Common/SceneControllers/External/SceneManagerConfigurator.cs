using App.Common.Autumn.Runtime.Attributes;
using App.Core.Startups.External;

namespace App.Common.SceneControllers.External
{
    [Configurator(ContextConstants.GlobalContext)]    
    public class SceneManagerConfigurator : Autumn.Runtime.Collection.Configurator
    {
        public override void Configuration()
        {
            Container.BindInterfacesAndSelfTo<SceneManager>().AsSingle();
        }
    }
}