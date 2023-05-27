using System;
using UniRx;

namespace Code.Infrastructure.Services.Core
{
    public interface IButtonClickMediator
    {
        IObservable<Unit> OnButtonClick { get; }
        void NotifyButtonClick();
    }
}