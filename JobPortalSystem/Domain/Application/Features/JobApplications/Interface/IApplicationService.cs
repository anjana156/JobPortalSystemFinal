using Domain.Application.Features.JobApplications.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobApplications.Interface
{
    public interface IApplicationService
    {
        Task<IEnumerable<ApplicationDto>> GetApplicationByJobId(Guid jobId);
        Task<ApplicationDto?> GetById(Guid id);
        Task<bool> UpdateStatus(Guid id, UpdateStatusDTO dto);

    }
}
