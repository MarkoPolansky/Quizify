using Microsoft.Extensions.DependencyInjection;

namespace Quizify.Common.Installers
{
    public interface IInstaller
    {
        void Install(IServiceCollection serviceCollection);
    }
}
