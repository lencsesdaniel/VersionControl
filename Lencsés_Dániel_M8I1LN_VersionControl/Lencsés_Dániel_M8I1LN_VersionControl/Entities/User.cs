using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lencsés_Dániel_M8I1LN_VersionControl.Entities
{
    class User
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }

    }
}
