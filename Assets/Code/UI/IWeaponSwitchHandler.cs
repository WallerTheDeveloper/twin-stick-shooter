using System;

namespace Code.UI
{
    public interface IWeaponSwitchHandler
    {
        event Action OnSwitchButtonClick;
    }
}