using System;
using System.Runtime.CompilerServices;
using Argengrill.Core.Infrastructure;
using Argengrill.Core.Extensions;
using Argengrill.Core;
using ArgenGrill.Web.Framework.Localization;
using ArgenGrill.Web.Framework.Modeling;

namespace ArgenGrill.Framework.Localization
{
    public class SmartResourceDisplayName : System.ComponentModel.DisplayNameAttribute, IModelAttribute
    {
        private readonly string _callerPropertyName;

        public SmartResourceDisplayName(string resourceKey, [CallerMemberName] string propertyName = null)
            : base(resourceKey)
        {
            ResourceKey = resourceKey;
            _callerPropertyName = propertyName;
        }

        public string ResourceKey { get; set; }

        public override string DisplayName
        {
            get
            {
                string value = null;
                var langId = EngineContext.Current.Resolve<IWorkContext>().WorkingLanguage.Id;
                value = EngineContext.Current.Resolve<ILocalizationService>().GetResource(ResourceKey, langId, true, "" /* ResourceKey */, true);

                if (value.IsEmpty() && _callerPropertyName.HasValue())
                {
                    value = _callerPropertyName.SplitPascalCase();
                }

                return value;
            }
        }

        public string Name
        {
            get { return "SmartResourceDisplayName"; }
        }
    }
}