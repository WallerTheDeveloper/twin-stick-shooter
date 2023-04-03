using UnityEngine;

namespace Code.Services.Input
{
    public class InputService : IInputService
    {
        private const string MovementHorizontal = "MovementHorizontal";
        private const string MovementVertical = "MovementVertical";

        private const string AimHorizontal = "AimHorizontal";
        private const string AimVertical = "AimVertical";
        
        public Vector2 AimAxis() => 
            new Vector2(SimpleInput.GetAxis(AimHorizontal), SimpleInput.GetAxis(AimVertical));

        public Vector2 MovementAxis() => 
            new Vector2(SimpleInput.GetAxis(MovementHorizontal), SimpleInput.GetAxis(MovementVertical));
    }
}
