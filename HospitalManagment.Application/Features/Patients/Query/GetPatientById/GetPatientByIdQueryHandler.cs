using AutoMapper;
using HospitalManagment.Application.Common;
using HospitalManagment.Application.Features.Patients.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Patients.Query.GetPatientById;

public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientDto>
{
    private readonly IMapper _mapper;
    private readonly IPatientRepository _repository;

    public GetPatientByIdQueryHandler(IPatientRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PatientDto> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        var patient = await _repository.GetByIdAsync(request.Id);
        if (patient == null) throw new NotFoundException($"Patient with id {request.Id} not found");
        var patientData = _mapper.Map<PatientDto>(patient);
        return patientData;
    }
}