using AutoMapper;
using HospitalManagment.Application.Common;
using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Query.GetDoctorById;

internal class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, DoctorDto>
{
    private readonly IMapper _mapper;
    private readonly IDoctorRepository _repository;

    public GetDoctorByIdQueryHandler(IDoctorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DoctorDto> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(request.Id);
        if (result == null) throw new NotFoundException($"Doctor with id {request.Id} not found");
        var doctor = _mapper.Map<DoctorDto>(result);

        return doctor;
    }
}