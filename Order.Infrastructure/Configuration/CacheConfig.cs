namespace Order.Infrastructure.Configuration
{
    public class CacheConfig()
    {
        public string Host { get; set; }
        /// <summary>
        /// Duration in minutes
        /// </summary>
        public int Duration { get; set; }
    }
}
