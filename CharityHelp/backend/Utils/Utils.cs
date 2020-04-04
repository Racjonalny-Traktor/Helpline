namespace charity.Utils
{
    public class Coords
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }

    public class NearestCall
    {
        public string DistanceString { get; set; }
        public float Distance { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}