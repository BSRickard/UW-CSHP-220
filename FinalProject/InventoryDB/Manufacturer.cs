//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OurAbbreviation { get; set; }
        public string ParentCorp { get; set; }
        public int DefaultCategory { get; set; }
        public string DefaultGoodsOrigin { get; set; }
        public Nullable<int> SourceOfOriginInfo { get; set; }
        public string WebSite { get; set; }
    }
}