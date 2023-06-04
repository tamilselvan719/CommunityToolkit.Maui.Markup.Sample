using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace CommunityToolkit.Maui.Markup.Sample.Pages.Base;

abstract class BaseContentPage : ContentPage
{
    protected BaseContentPage(in bool shouldUseSafeArea = false)
    {
        On<iOS>().SetUseSafeArea(shouldUseSafeArea);
        On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.FormSheet);

        Build();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

#if DEBUG
        HotReloadService.UpdateApplicationEvent += ReloadUI;
#endif
    }

    private void ReloadUI(Type[]? obj)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Build();
        });
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);

#if DEBUG
        HotReloadService.UpdateApplicationEvent -= ReloadUI;
#endif
    }

    public abstract void Build();
}

abstract class BaseContentPage<T> : BaseContentPage where T : BaseViewModel
{
    protected BaseContentPage(T viewModel, string pageTitle)
    {
        base.BindingContext = viewModel;

        Title = pageTitle;
    }

    protected new T BindingContext => (T)base.BindingContext;
}