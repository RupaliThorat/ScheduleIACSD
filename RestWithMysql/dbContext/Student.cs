using System;
using System.Collections.Generic;

namespace RestWithMysql.dbContext;

public partial class Student
{
    public int Sid { get; set; }

    public string FirstName { get; set; } = null!;

    public int Age { get; set; }

    public string? City { get; set; }

    public int? Cid { get; set; }

    public virtual Course? CidNavigation { get; set; }
}
