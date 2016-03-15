using System;
using Microsoft.SqlServer.Types;

namespace Model.PocoEntity
{
    public class Advertisement
    {
        public int Id { get; set; }

        public string Description { get; set;}

        public DateTime MaintenanceTime { get; set;}

        public double Lt { get; set; }
        public double Ln { get; set; }

        public string @Type { get; set;}

        public double Height { get; set;}

        public double Width { get; set;}

        public double MonthlyCost { get; set; }
    }
}
