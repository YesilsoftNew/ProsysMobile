using Acr.UserDialogs;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.ViewParamModels;
using ProsysMobile.Services.Navigation;
using ProsysMobile.ViewModels.Pages.System;
using System;
using System.Threading;
using System.Threading.Tasks;
using ProsysMobile.Models.CommonModels.Enums;
using Xamarin.Forms;

namespace ProsysMobile.Services.Dialog
{
    public class DialogService : IDialogService
    {
        INavigationService _navigationService;
        public DialogService(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public Task<string> ActionSheetAsync(string title, string cancel, string destructive, CancellationToken? cancelToken = null, params string[] buttons)
        {
            return UserDialogs.Instance.ActionSheetAsync(title, cancel, destructive, cancelToken, buttons);
        }

        public Task AlertAsync(string message, string title = null, string okText = null, CancellationToken? cancelToken = null)
        {
            return UserDialogs.Instance.AlertAsync(message, title, okText, cancelToken);
        }

        public Task AlertAsync(AlertConfig config, CancellationToken? cancelToken = null)
        {
            return UserDialogs.Instance.AlertAsync(config, cancelToken);
        }

        public Task<bool> ConfirmAsync(string message, string title = null, string okText = null, string cancelText = null, CancellationToken? cancelToken = null)
        {
            return UserDialogs.Instance.ConfirmAsync(message, title, okText, cancelText, cancelToken);
        }

        public Task<bool> ConfirmAsync(ConfirmConfig config, CancellationToken? cancelToken = null)
        {
            return UserDialogs.Instance.ConfirmAsync(config, cancelToken);
        }

        public Task<DatePromptResult> DatePromptAsync(string title = null, DateTime? selectedDate = null, CancellationToken? cancelToken = null)
        {
            return UserDialogs.Instance.DatePromptAsync(title, selectedDate, cancelToken);
        }

        public Task<DatePromptResult> DatePromptAsync(DatePromptConfig config, CancellationToken? cancelToken = null)
        {
            return UserDialogs.Instance.DatePromptAsync(config, cancelToken);
        }

        public void HideLoading()
        {
            UserDialogs.Instance.HideLoading();
        }

        public IProgressDialog Loading(string title = null, Action onCancel = null, string cancelText = null, bool show = true, MaskType? maskType = null)
        {
            return UserDialogs.Instance.Loading(title, onCancel, cancelText, show, maskType);
        }

        public Task<LoginResult> LoginAsync(string title = null, string message = null, CancellationToken? cancelToken = null)
        {
            return UserDialogs.Instance.LoginAsync(title, message, cancelToken);
        }

        public Task<LoginResult> LoginAsync(LoginConfig config, CancellationToken? cancelToken = null)
        {
            return UserDialogs.Instance.LoginAsync(config, cancelToken);
        }

        public IProgressDialog Progress(ProgressDialogConfig config)
        {
            return UserDialogs.Instance.Progress(config);
        }

        public IProgressDialog Progress(string title = null, Action onCancel = null, string cancelText = null, bool show = true, MaskType? maskType = null)
        {
            return UserDialogs.Instance.Progress(title, onCancel, cancelText, show, maskType);
        }

        public Task<PromptResult> PromptAsync(string message, string title = null, string okText = null, string cancelText = null, string placeholder = "", InputType inputType = InputType.Default, CancellationToken? cancelToken = null)
        {
            return UserDialogs.Instance.PromptAsync(message, title, okText, cancelText, placeholder, inputType, cancelToken);
        }

        public Task<PromptResult> PromptAsync(PromptConfig config, CancellationToken? cancelToken = null)
        {
            return UserDialogs.Instance.PromptAsync(config, cancelToken);
        }

        public void ShowLoading(string title = null, MaskType? maskType = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                title = Resources.Language.Resource.PleaseWait;
            
            UserDialogs.Instance.ShowLoading(title, maskType);
        }

        public Task<TimePromptResult> TimePromptAsync(TimePromptConfig config, CancellationToken? cancelToken = null)
        {
            return UserDialogs.Instance.TimePromptAsync(config, cancelToken);
        }

        public Task<TimePromptResult> TimePromptAsync(string title = null, TimeSpan? selectedTime = null, CancellationToken? cancelToken = null)
        {
            return UserDialogs.Instance.TimePromptAsync(title, selectedTime, cancelToken);
        }

        public async Task ToastMessage(string message, double duration, bool showOnTop, string icon, Color bgColor, Color msgColor)
        {
            UserDialogs.Instance.Toast(new ToastConfig(message)
                                .SetBackgroundColor(bgColor)
                                .SetMessageTextColor(msgColor)
                                .SetDuration(TimeSpan.FromSeconds(duration))
                                .SetPosition(showOnTop ? ToastPosition.Top : ToastPosition.Bottom)
                                .SetIcon(icon)
                            );
        }

        public async Task InfoToastMessage(string message)
        {
            NavigationModel<ToastMessagePageViewParamModel> navigationModel = new NavigationModel<ToastMessagePageViewParamModel>
            {
                Model = new ToastMessagePageViewParamModel
                {
                    MessageText = message,
                    ToastMessageType = enToastMessageType.Info
                },
            };

            _navigationService.NavigateToPopupAsync<ToastMessagePageViewModel>(navigationModel);
        }

        public async Task SuccessToastMessage(string message)
        {
            NavigationModel<ToastMessagePageViewParamModel> navigationModel = new NavigationModel<ToastMessagePageViewParamModel>
            {
                Model = new ToastMessagePageViewParamModel
                {
                    MessageText = message,
                    ToastMessageType = enToastMessageType.Success
                },
            };

            _navigationService.NavigateToPopupAsync<ToastMessagePageViewModel>(navigationModel);
        }

        public async Task ErrorToastMessage(string message)
        {
            NavigationModel<ToastMessagePageViewParamModel> navigationModel = new NavigationModel<ToastMessagePageViewParamModel>
            {
                Model = new ToastMessagePageViewParamModel
                {
                    MessageText = message,
                    ToastMessageType = enToastMessageType.Error
                },
            };

            _navigationService.NavigateToPopupAsync<ToastMessagePageViewModel>(navigationModel);
        }

        public async Task WarningToastMessage(string message)
        {
            NavigationModel<ToastMessagePageViewParamModel> navigationModel = new NavigationModel<ToastMessagePageViewParamModel>
            {
                Model = new ToastMessagePageViewParamModel
                {
                    MessageText = message,
                    ToastMessageType = enToastMessageType.Warning
                },
            };

            _navigationService.NavigateToPopupAsync<ToastMessagePageViewModel>(navigationModel);
        }
    }
}