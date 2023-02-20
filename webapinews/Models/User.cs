﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using webapinews.Entities;

namespace webapinews.Models;

public partial class User : TrackableBaseEntity
{
   
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Role Role { get; set; }

    public virtual ICollection<BookMark> BookMarks { get; } = new List<BookMark>();
    
}

