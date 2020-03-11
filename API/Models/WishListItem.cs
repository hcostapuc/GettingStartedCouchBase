using System;

namespace API.Models
{
    public class WishListItem
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Type => typeof(WishListItem).Name;
    }
}
