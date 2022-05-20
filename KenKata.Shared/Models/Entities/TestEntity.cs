using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenKata.Shared.Models.Entities
{
    public class TestEntity
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
    }
}
