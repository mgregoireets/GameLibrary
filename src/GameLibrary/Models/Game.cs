using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.Models
{
    public class Game
    {
        [Key]
        public int id { get; set; }
        public string name;
    }
}
