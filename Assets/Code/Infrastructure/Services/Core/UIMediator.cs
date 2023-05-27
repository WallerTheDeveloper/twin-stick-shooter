using System;
using UniRx;

namespace Code.Infrastructure.Services.Core
{
    public class UIMediator : IButtonClickMediator
    {
        private Subject<Unit> _buttonClickSubject = new Subject<Unit>();
        public IObservable<Unit> OnButtonClick => _buttonClickSubject;
        
        public void NotifyButtonClick()
        {
            _buttonClickSubject.OnNext(Unit.Default);
        }
    }
}