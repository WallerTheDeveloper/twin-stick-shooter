﻿using UnityEngine;

namespace Code.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 MovementAxis();
        Vector2 AimAxis();
    }
}