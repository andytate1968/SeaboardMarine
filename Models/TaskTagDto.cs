using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TaskTagDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string TagName { get; set; }
        public bool IsNew { get; set; }
        public bool IsDelete { get; set; }
    }
}
