using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public partial class AuthUser:SystemUser
{
   

    public string Password { get; set; } = null!;

    public string? ConnectionId { get; set; }

    public bool? OnlineStatus { get; set; }

    
}
