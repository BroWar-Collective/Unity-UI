using UnityEngine;

namespace BroWar.UI.Input
{
    public interface IUiInputHandler
    {
        Vector2 PointerPosition { get; }
    }
}