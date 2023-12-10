using System.Globalization;
using Quizify.Common;
using Quizify.Common.BL.Facades;


namespace Quizify.Web.BL.Facades
{
    public abstract class FacadeBase
    {
        protected virtual string culture => CultureInfo.DefaultThreadCurrentCulture?.Name ?? "cs";
    }
}
