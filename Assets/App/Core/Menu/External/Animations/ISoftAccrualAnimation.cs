using UnityEngine;

namespace App.Menu.UI.External.Animations
{
    public interface ISoftAccrualAnimation
    {
        void PlayAnimation(Vector3 globalPosition, Transform parent, int amount);
    }
}