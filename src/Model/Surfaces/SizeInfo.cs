using System;

namespace MarsExploration.Model.Surfaces
{
    public struct SizeInfo
    {
        public int XLength { get; }
        public int YLength { get; }

        public SizeInfo(int xLength, int yLength)
        {
            if (xLength <= 0 || yLength <= 0)
            {
                throw new ArgumentException($"Plateau size not valid [{xLength},{yLength}]");
            }

            XLength = xLength;
            YLength = yLength;
        }
    }
}