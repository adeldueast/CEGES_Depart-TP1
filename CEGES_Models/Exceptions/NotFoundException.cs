using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_Models.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entity, int entityId) : base($"{entity} with ID '{entityId}' does not exist.")
        {
        }
    }
}
