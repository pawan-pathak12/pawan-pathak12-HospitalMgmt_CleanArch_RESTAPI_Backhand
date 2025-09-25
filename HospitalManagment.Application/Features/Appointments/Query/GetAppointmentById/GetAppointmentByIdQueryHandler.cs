using AutoMapper;
using HospitalManagment.Application.Common;
using HospitalManagment.Application.Features.Appointments.DTOs;
using HospitalManagment.Application.Interfaces;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Query.GetAppointmentById;

public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto>
{
    private readonly IMapper _mapper;
    private readonly IAppointmentRepository _repository;

    public GetAppointmentByIdQueryHandler(IAppointmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AppointmentDto> Handle(GetAppointmentByIdQuery request, CancellationToken cancellation)
    {
        var appointment = await _repository.GetByIdAsync(request.Id);
        if (appointment == null) throw new NotFoundException($"Their is no appointment with id {request.Id}.");
        var appointmentEntity = _mapper.Map<AppointmentDto>(appointment);

        return appointmentEntity;
    }
}