using AutoMapper;
using HospitalManagment.Application.Features.Doctors.DTOs;
using HospitalManagment.Application.Interfaces;
using HospitalManagment.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagment.Application.Features.Doctors.Query.GetDoctorById
{
    internal class GetDoctorByIdQueryHandler:IRequestHandler<GetDoctorByIdQuery , DoctorDto>
    {
        private readonly IDoctorRepository _repository;
        private readonly IMapper _mapper;

        public GetDoctorByIdQueryHandler(IDoctorRepository repository , IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        public async Task<DoctorDto> Handle(GetDoctorByIdQuery request , CancellationToken cancellationToken)
        {
            var result  = await _repository.GetByIdAsync(request.Id);
            if(result==null)
            {
                throw new Exception($"Doctor with id {request.Id} not found");
            }
            var doctor = _mapper.Map<DoctorDto>(result);
         
            return doctor;
        }
    }
}
