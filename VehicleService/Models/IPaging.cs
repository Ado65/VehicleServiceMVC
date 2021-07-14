using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleService.Models
{
    public interface IPaging
    {
         int CurrentPage { get; set; }
         int ItemsPerPage { get; set; }
    }
}
