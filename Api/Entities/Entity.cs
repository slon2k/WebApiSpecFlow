using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Entities
{
    public abstract class Entity
    {
        [Key]
        int Id { get; set; }
    }
}
