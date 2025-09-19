using UnityEngine;

namespace App.Core.Canvases.External
{
    public class PopupCanvas : MonoBehaviour, ICanvas
    {
        public Transform GetContent()
        {
            return transform;
        }
    }
}