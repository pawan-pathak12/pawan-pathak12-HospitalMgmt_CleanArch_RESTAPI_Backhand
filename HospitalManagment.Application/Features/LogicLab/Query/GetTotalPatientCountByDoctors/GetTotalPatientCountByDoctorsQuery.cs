using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagment.Application.Features.LogicLab.Query.GetTotalPatientCountByDoctors
{
    public class GetTotalPatientCountByDoctorsQuery:IRequest<int>
    {
        public int DoctorId { get; set; }
    }
}
