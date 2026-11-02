using UnityEngine;

namespace BroWar.UI.Input
{
    public interface IInputHandler
    {
        Vector2? PointerPosition { get; }
    }
}