using AutoMapper;
using HospitalManagment.Application.Features.Patients.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Patients.Query.GetAllPatientsByDate;

public class GetAllPatientsByDateHandler : IRequestHandler<GetAllPatientsByDateQuery, IEnumerable<PatientDto>>
{
    private readonly IMapper _mapper;
    private readonly IPatientRepository _patientRepository;

    public GetAllPatientsByDateHandler(IPatientRepository patientRepository, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PatientDto>> Handle(GetAllPatientsByDateQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _patientRepository.GetPatientsByAppointmentDateAsync(request.Type, request.Date);
        if (!result.Any()) throw new Exception($" Their is no entry of appointment on date {request.Date}");

        var patientEnity = _mapper.Map<IEnumerable<PatientDto>>(result);
        return patientEnity;
    }
}