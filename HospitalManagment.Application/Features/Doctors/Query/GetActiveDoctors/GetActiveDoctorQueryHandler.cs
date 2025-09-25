using AutoMapper;
using HospitalManagment.Application.Common;
using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Doctors.Query.GetActiveDoctors;

public class GetActiveDoctorQueryHandler : IRequestHandler<GetActiveDoctorQuery, IEnumerable<DoctorDto>>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public GetActiveDoctorQueryHandler(IDoctorRepository doctorRepository, IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DoctorDto>> Handle(GetActiveDoctorQuery request, CancellationToken cancellationToken)
    {
        var result = await _doctorRepository.GetActiveDoctorAsync();
        if (result == null) throw new NotFoundException("Their is no Active Appointment ");
        var doctorEntity = _mapper.Map<IEnumerable<DoctorDto>>(result);
        return doctorEntity;
    }
}