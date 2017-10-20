using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace AspNetCore.Common.Infrastructure
{
    public class CmsApplicationModelProvider : IApplicationModelProvider
    {
        public int Order {
            get {
                return -990;
            }
        }

        public void OnProvidersExecuted(ApplicationModelProviderContext context) {
        }

        public void OnProvidersExecuting(ApplicationModelProviderContext context) {
            if (context == null) {
                throw new ArgumentNullException("context");
            }
            foreach (ControllerModel model in context.Result.Controllers) {
                IEnumerator<IAllowAnonymous> enumerator2;
                using (enumerator2 = Enumerable.OfType<IAllowAnonymous>(model.Attributes).GetEnumerator()) {
                    while (enumerator2.MoveNext()) {
                        IAllowAnonymous current = enumerator2.Current;
                        model.Filters.Add(new AllowAnonymousFilter());
                    }
                }
                foreach (ActionModel model2 in model.Actions) {
                    using (enumerator2 = Enumerable.OfType<IAllowAnonymous>(model2.Attributes).GetEnumerator()) {
                        while (enumerator2.MoveNext()) {
                            IAllowAnonymous local2 = enumerator2.Current;
                            model2.Filters.Add(new AllowAnonymousFilter());
                        }
                    }
                }
            }
        }
    }
}