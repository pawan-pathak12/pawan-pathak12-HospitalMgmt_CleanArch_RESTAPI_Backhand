using AutoMapper;
using HospitalManagment.Application.Interfaces;
using HospitalManagment.Domain.Entity;
using MediatR;

namespace HospitalManagment.Application.Features.Appointments.Commands.CreateAppointments
{
    public class BookAppointmentCommadHandler : IRequestHandler<BookAppointmentCommand, Appointment>
    {
        #region Private Field
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IAppointmentLogicTester _appointmentLogicTester;
        private readonly IMapper _mapper;
        #endregion

        public BookAppointmentCommadHandler(IAppointmentRepository repository, IDoctorRepository doctorRepository, IPatientRepository patientRepository, IAppointmentLogicTester appointmentLogicTester ,IMapper mapper)
        {
            this._appointmentRepository = repository;
            this._doctorRepository = doctorRepository;
            this._patientRepository = patientRepository;
            this._appointmentLogicTester = appointmentLogicTester;
            this._mapper = mapper;
        }
        public async Task<Appointment> Handle(BookAppointmentCommand request, CancellationToken cancellationToken)
        {
            var endtime = request.Appointment.StartTime.Add(TimeSpan.FromMinutes(30));

            // Doctor-related validations
            await CheckDoctorAvailabilityAsync(request);
            await CheckDoctorInfoAsync(request);
            await CheckMaxDailyAppointmentsAsync(request);
            await IsDoctorActiveAsync(request);

            // Patient-related validations
            await CheckPatientExistance(request);

            // Time-related validations
            await CheckSpacing(request);
            await CheckStartTimeWithinWorkingHoursAsync(request);   // not tested fully 
            ValidateLunchBreakTimeAsync(request);

            //pre -validation
            await BlockBookingOnSunday(request.Appointment.AppointmentDate);
            await CheckNumberOfBookingOfPatient(request);
            await BlockBookingIfOutOfDate(request.Appointment.AppointmentDate);
            await BookingDateValidationAsync(request);
            await CheckAndBlockPatientAsync(request.Appointment.PatientId); // not tested in swagger

            // auto-mapper 
            var appointment = _mapper.Map<Appointment>(request.Appointment);
            //Manual Override For endtime
            appointment.EndTime = endtime;
            return await _appointmentRepository.AddAsync(appointment);
        }

        #region Patient Validations

        // Check if patient exists in the system
        private async Task CheckPatientExistance(BookAppointmentCommand request)
        {
            var patient = new Patient(); // Placeholder, not used
            var isExisted = await _appointmentRepository.CheckPatientExisting(request.Appointment.PatientId);
            if (!isExisted)
            {
                throw new Exception($"Patient with Id {request.Appointment.PatientId} dont Exists");
            }
        }

        // Check how many bookings the patient has on the given date
        private async Task CheckNumberOfBookingOfPatient(BookAppointmentCommand request)
        {
            var result = await _appointmentRepository.CheckNumberOfBookingOfPatient(request.Appointment.PatientId, request.Appointment.AppointmentDate);
            if (result > 3)
            {
                throw new Exception($"Pateint with Id {request.Appointment.PatientId} Cross Maximum Limit Of Book");
            }
        }

        // Block patient from booking if they have too many cancellations or miss
        private async Task CheckAndBlockPatientAsync(int patientId)
        {
            var patient = await _doctorRepository.GetByIdAsync(patientId); // Should ideally use patient repo
            if (patient == null)
            {
                throw new KeyNotFoundException($"Patient with Id {patientId} not found");
            }

            var cancelCount = await _appointmentLogicTester.CheckAndBlockPatientIfNeededAsync(patientId);
            if (cancelCount > 3)
            {
                var blockUntil = DateTime.UtcNow.AddDays(3);
                await _patientRepository.SetPatientBlockUntilAsync(blockUntil, patientId);
                throw new Exception($"patient with Id {patientId} is blocked till date {blockUntil} from Booking Appointment.");
            }
        }

        #endregion

        #region Doctor Validations

        // Check if doctor exists
        private async Task CheckDoctorInfoAsync(BookAppointmentCommand request)
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.Appointment.DoctorId);
            if (doctor == null)
            {
                throw new Exception("Doctor not found");
            }
        }

        // Check if doctor is active on the appointment date
        private async Task IsDoctorActiveAsync(BookAppointmentCommand request)
        {
            var result = await _doctorRepository.IsDoctorActiveAsync(request.Appointment.DoctorId);
            if (!result)
            {
                throw new InvalidOperationException($"Doctor with Id {request.Appointment.DoctorId} is inactive in time {request.Appointment.AppointmentDate}.");
            }
        }

        // Check if doctor is available at the requested time
        private async Task CheckDoctorAvailabilityAsync(BookAppointmentCommand request)
        {
            var endTime = request.Appointment.StartTime.Add(TimeSpan.FromMinutes(30));

            bool isAvailable = await _appointmentRepository.CheckAvailability(
                request.Appointment.DoctorId,
                request.Appointment.AppointmentDate,
                request.Appointment.StartTime,
                endTime
            );

            if (!isAvailable)
            {
                throw new Exception("Doctor is not available for the requested time.");
            }
        }

        // Check if doctor has reached maximum daily appointments
        private async Task CheckMaxDailyAppointmentsAsync(BookAppointmentCommand request)
        {
            var doctors = new Doctor(); // Placeholder, not populated
            int maxBooking = (int)((doctors.AvailableEndTime - doctors.AvailableStartTime).TotalMinutes / 30);
            var currentBookingCount = await _appointmentRepository.CountBookingsAsync(request.Appointment.DoctorId, request.Appointment.AppointmentDate);
            if (currentBookingCount > maxBooking)
            {
                throw new Exception($"Booking is out of limit for Doctor with id{request.Appointment.DoctorId}");
            }
        }

        #endregion

        #region Time & Date Validations

        // Check that appointment time is within working hours
        private async Task CheckStartTimeWithinWorkingHoursAsync(BookAppointmentCommand request)
        {
            var workingHourOfDoctor = await _doctorRepository.GetDoctorWorkingHourAsync(request.Appointment.DoctorId);
            if (workingHourOfDoctor == null)
            {
                throw new Exception("Doctor's working hours not found.");
            }
            var appointmentDate = request.Appointment.AppointmentDate.TimeOfDay;
            if (appointmentDate < workingHourOfDoctor.AvailableStartTime || appointmentDate > workingHourOfDoctor.AvailableEndTime)
            {
                throw new Exception("Appointment time is outside the doctor's working hours.");
            }
        }

        // Block booking during lunch break (12:00–13:00)
        private void ValidateLunchBreakTimeAsync(BookAppointmentCommand request)
        {
            TimeSpan lunchStartTime = new TimeSpan(12, 0, 0);
            TimeSpan lunchEndTime = new TimeSpan(13, 0, 0);
            if (request.Appointment.StartTime >= lunchStartTime && request.Appointment.StartTime < lunchEndTime)
            {
                throw new Exception($"Appointment creation failed as this is launch time .Try again after {lunchEndTime}");
            }
        }

        // Block booking on Sunday
        private async Task BlockBookingOnSunday(DateTime appointmentDate)
        {
            var isSunday = await _appointmentRepository.BlockBookingOnSundayAsync(appointmentDate);
            if (isSunday)
            {
                throw new Exception("Today is holiday , you can try next Day.");
            }
        }

        // Block booking more than 30 days in advance
        private async Task BlockBookingIfOutOfDate(DateTime appointmentDate)
        {
            var isOutOfDate = await _appointmentRepository.BlockBookingOutOfDate(appointmentDate);
            if (isOutOfDate)
            {
                throw new Exception("Appointment date must be within 1 to 30 days from today.");
            }
        }

        // Block booking less than 2 hours in advance and appointment date is not in past 
        private async Task BookingDateValidationAsync(BookAppointmentCommand request)
        {
            var appointmentDateTime = request.Appointment.AppointmentDate.Date.Add(request.Appointment.StartTime);

            if (appointmentDateTime <= DateTime.Now)
            {
                throw new Exception("Appointment time must be in the future.");
            }
            var isBookingValid = await _appointmentRepository.BookingDateValidationAsync(request.Appointment.AppointmentDate);
            if (isBookingValid)
            {
                throw new Exception("You must Book Appointment before 2 hours");
            }
        }

        #endregion

        #region Overlap & Spacing Validations

        // Check that requested time slot does not overlap with another appointment
        private async Task CheckSpacing(BookAppointmentCommand request)
        {
            var isSpaced = await _appointmentRepository.IsTimeSlotSpacedAsync(
                request.Appointment.DoctorId,
                request.Appointment.AppointmentDate,
                request.Appointment.StartTime
            );
            if (!isSpaced)
            {
                throw new Exception("Requested time is too close to another appointment");
            }
        }

        #endregion
    }
}

