using System;

namespace Cinema.Persistence.DTOs
{
    public class ImageDto
    {
        /// <summary>
        /// Kis kép lekérdezése, vagy beállítása.
        /// </summary>
        public Byte[] ImageSmall { get; set; }

        /// <summary>
        /// Nagy kép lekérdezése, vagy beállítása.
        /// </summary>
        public Byte[] ImageLarge { get; set; }
    }
}
