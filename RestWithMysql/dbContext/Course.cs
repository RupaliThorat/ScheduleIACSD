using System;
using System.Collections.Generic;

namespace RestWithMysql.dbContext;

public partial class Course
{
    public int Cid { get; set; }

    public string CourseName { get; set; } = null!;

    public int? Intake { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
