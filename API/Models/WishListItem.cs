using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class WishListItem
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Type => typeof(WishListItem).Name;
    }
}
