using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class UserPluginData
    {
        public string PluginId { get; set; }
        public string UserId { get; set; }
        public string Data { get; set; }
    }
}
