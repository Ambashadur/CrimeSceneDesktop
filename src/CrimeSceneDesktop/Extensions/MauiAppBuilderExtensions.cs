using CS.Common.Exceptions;
using CS.Common.Exceptions.Impl;
using CS.Common.Services;
using CS.Common.Services.Impl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;

namespace CrimeSceneDesktop.Extensions;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder RegisterPages(this MauiAppBuilder mauiAppBuilder) {
        mauiAppBuilder.Services
            .AddSingleton<MainPage>()
            .AddSingleton<CommonPage>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder) {
        mauiAppBuilder.Services
            .AddSingleton<ISsoService, SsoService>()
            .AddSingleton<IUserService, UserService>()
            .AddSingleton<ISceneService, SceneService>();

        mauiAppBuilder.Services
            .AddSingleton<IExceptionHandler, ExceptionHandler>();

        return mauiAppBuilder;
    }
}
