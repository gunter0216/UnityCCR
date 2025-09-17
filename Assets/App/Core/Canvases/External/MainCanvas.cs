﻿using App.Common.Autumn.Runtime.Attributes;
using UnityEngine;

namespace App.Game.Canvases.External
{
    public class MainCanvas : MonoBehaviour, ICanvas
    {
        public Transform GetContent()
        {
            return transform;
        }
    }
}