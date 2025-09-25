using AutoMapper;
using HospitalManagment.Application.Common;
using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.GetAllAppointments;

public class GetAllAppointmentQueryHandler : IRequestHandler<GetAllAppointmentQuery, IEnumerable<AppointmentDto>>
{
    private readonly IMapper _mapper;
    private readonly IAppointmentRepository _repository;

    public GetAllAppointmentQueryHandler(IAppointmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AppointmentDto>> Handle(GetAllAppointmentQuery request,
        CancellationToken cancellationToken)
    {
        var appointments = await _repository.GetAllAsync();
        if (appointments == null || !appointments.Any()) throw new NotFoundException("Their is no appointments");
        var appointmentEntity = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);

        return appointmentEntity;
    }
}