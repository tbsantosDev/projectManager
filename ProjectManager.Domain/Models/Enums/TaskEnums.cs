using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Models.Enums
{
    public enum TaskStatusEnums
    {
        Pending = 0,
        InProgress = 1,
        Completed = 2,
    }

    public enum TaskPriorityEnums
    {
        high = 0,
        Medium = 1,
        Low = 2,
    }
}
