// using System.Collections.Generic;
//
// namespace Code.Infrastructure.Services.Pause
// {
//     public class PauseService : IPauseHandler
//     {
//         private readonly List<IPauseHandler> _handlers =
//             new List<IPauseHandler>();
//
//         public bool IsPaused { get; private set; }
//         
//         public void Register(IPauseHandler pauseHandler) => 
//             _handlers.Add(pauseHandler);
//
//         public void UnRegister(IPauseHandler pauseHandler) =>
//             _handlers.Remove(pauseHandler);
//         
//         public void SetPaused(bool isPaused)
//         {
//             IsPaused = isPaused;
//             foreach (var handler in _handlers)
//             {
//                 handler.SetPaused(isPaused);
//             }
//         }
//     }
// }