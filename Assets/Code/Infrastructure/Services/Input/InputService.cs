using UnityEngine;

namespace Code.Infrastructure.Services.Input
{
    public class InputService : IInputService
    {
        private const string MovementHorizontal = "Horizontal";
        private const string MovementVertical = "Vertical";

        private const string AimHorizontal = "AimHorizontal";
        private const string AimVertical = "AimVertical";
        
        public Vector2 AimAxis() => 
            new Vector2(SimpleInput.GetAxis(AimHorizontal), SimpleInput.GetAxis(AimVertical));

        public Vector2 MovementAxis() => 
            new Vector2(SimpleInput.GetAxis(MovementHorizontal), SimpleInput.GetAxis(MovementVertical));
    }
}
