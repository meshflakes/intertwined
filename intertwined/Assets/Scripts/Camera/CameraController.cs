﻿namespace Camera
{
    public abstract class CameraController
    {
        public bool YieldingCameraControl = false;
        public abstract void UpdateCamera();
    }
}