using AutoMapper;
using Domain.Application.Features.JobApplications.DTO;
using Domain.Application.Features.JobApplications.Interface;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Features.JobApplications.Service
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _repository;
        private readonly IMapper _mapper;
        public ApplicationService(IApplicationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ApplicationDto>> GetApplicationByJobId(Guid jobId)
        {
            var applications = await _repository.GetApplicationByJobId(jobId);
            return _mapper.Map<IEnumerable<ApplicationDto>>(applications);

        }
        public async Task<ApplicationDto?> GetById(Guid id)
        {
            var application = await _repository.GetById(id);
            if (application == null)
                return null;
            return _mapper.Map<ApplicationDto>(application);
        }
        public async Task<bool> UpdateStatus(Guid id, UpdateStatusDTO dto)
        {
            var application = await _repository.GetById(id);

            if (application == null)
                return false;

            // Validate enum value
            if (!Enum.IsDefined(typeof(ApplicationStatus), dto.Status))
                return false;

            application.Status = (Status)dto.Status;

            await _repository.Update(application);

            return true;
        }

    }
    }

