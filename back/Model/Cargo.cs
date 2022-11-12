using System;
using System.Collections.Generic;

namespace back.Model;

public partial class Cargo
{
    public int Id { get; set; }

    public int? CargoId { get; set; }

    public virtual Usuario? CargoNavigation { get; set; }
}
