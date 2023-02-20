using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace webapinews.Models;

public partial class News : TrackableBaseEntity
{
   
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Aurthor { get; set; } = null!;

    public string Content { get; set; } = null!;

    public virtual ICollection<BookMark> BookMarks { get; } = new List<BookMark>();
}
