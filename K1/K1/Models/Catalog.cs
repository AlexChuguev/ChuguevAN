//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace K1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Catalog
    {
        public int Id { get; set; }
        public Nullable<int> idstuff { get; set; }
        public Nullable<int> idact { get; set; }
        public string name { get; set; }
        public string tech { get; set; }
        public Nullable<System.DateTime> date { get; set; }
    
        public virtual Act Act { get; set; }
        public virtual Stuff Stuff { get; set; }
    }
}
