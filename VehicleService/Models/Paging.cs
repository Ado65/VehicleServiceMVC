using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleService.Models
{
    public class Paging : IPaging
    {
      public  int currentPage { get; set; }
      public  int itemsPerPage { get; set; }
    }
}