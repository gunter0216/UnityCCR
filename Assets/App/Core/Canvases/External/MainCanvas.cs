using UnityEngine;

namespace App.Core.Canvases.External
{
    public class MainCanvas : MonoBehaviour, ICanvas
    {
        public Transform GetContent()
        {
            return transform;
        }
    }
}