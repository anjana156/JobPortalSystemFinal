using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Application.Features.Job.DTO
{

    public class SavedJobsDtos
    {

        public SavedJobsDtos(Guid job, DateTime dateSaved, JobPost jobPost)
        {

            DateSaved = dateSaved;
            JobPost = jobPost;

        }


        public DateTime DateSaved { get; set; }
        [JsonIgnore]
        public JobPost JobPost { get; set; }



    }
}
