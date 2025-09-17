using App.Common.Autumn.Runtime.Attributes;
using App.Core.Startups.External;

namespace App.Common.AssetSystem.External
{
    [Configurator(ContextConstants.GlobalContext)]    
    public class AssetManagerConfigurator : Autumn.Runtime.Collection.Configurator
    {
        public override void Configuration()
        {
            Container.BindInterfacesAndSelfTo<AssetManager>().AsSingle();
        }
    }
}