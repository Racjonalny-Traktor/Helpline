using System;
using charity.Models;
using GeoCoordinatePortable;

namespace charity.Utils
{
    public static class Converters
    {
        public static NearestCall ConvertCallToNearestCall(this Call call, Coords pos)
        {
            var p1 = new GeoCoordinate(call.User.Latitude, call.User.Longitude);
            var p2 = new GeoCoordinate(pos.Latitude, pos.Longitude);
            var dist = p1.GetDistanceTo(p2); //in meters
            dist /= 1000; // in km

            return new NearestCall
            {
                Latitude = call.User.Latitude,
                Longitude = call.User.Longitude,
                Distance = (float) Math.Round(dist, 1),
                DistanceString = $"{(float) Math.Round(dist, 1)}km"
            };
        }

        public static NearestCall ConvertCallToNearestCall(this Call call)
        {
            return new NearestCall
            {
                Latitude = call.User.Latitude,
                Longitude = call.User.Longitude,
                Distance = 0,
                DistanceString = "a few kilometers"
            };
        }
    }
}