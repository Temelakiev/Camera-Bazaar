using CameraBazaar.Data.Models;
using System;
using System.Collections.Generic;

namespace CameraBazaar.Services
{
    public interface ICameraService
    {
        void Create(
            CameraMakeType make,
            string model,
            decimal price,
            int quantity,
            int minShutterSpeed,
            int maxShutterSpeed,
            MinISOType minISO,
            int maxISO,
            bool isFullFrame,
            string videoResulution,
            IEnumerable<LightMetering> lightMetering,
            string description,
            string imageUrl,
            string userId);
    }
}
