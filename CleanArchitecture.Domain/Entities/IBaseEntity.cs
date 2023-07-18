using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
        int? CreatedByUserId { get; set; }
        int? ModifiedByUserId { get; set; }
        bool IsSoftDeleted { get; set; }
    }
}
