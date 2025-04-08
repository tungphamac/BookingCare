using BookingCare.Data.Data;
using BookingCare.Data.Models;
using BookingCare.Data.Repositories;

namespace BookingCare.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IGeneralRepository<Appointment> _appointmentRepository;
        private IGeneralRepository<Clinic> _clinicRepository;
        private IGeneralRepository<Doctor> _doctorRepository;
        private IGeneralRepository<Feedback> _feedbackRepository;
        private IGeneralRepository<MedicalRecord> _medicalRecordRepository;
        private IGeneralRepository<Patient> _patientRepository;
        private IGeneralRepository<Schedule> _scheduleRepository;
        private IGeneralRepository<Specialization> _specializationRepository;
        private IGeneralRepository<User> _userRepository;
        private IGeneralRepository<Notification> _notificationRepository;
        private IGeneralRepository<Message> _messageRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public AppDbContext Context => _context;

        public IGeneralRepository<Appointment> AppointmentRepository => _appointmentRepository ??= new GeneralRepository<Appointment>(Context);

        public IGeneralRepository<Clinic> ClinicRepository => _clinicRepository ??= new GeneralRepository<Clinic>(Context);

        public IGeneralRepository<Doctor> DoctorRepository => _doctorRepository ??= new GeneralRepository<Doctor>(Context);

        public IGeneralRepository<Feedback> FeedbackRepository => _feedbackRepository ??= new GeneralRepository<Feedback>(Context);

        public IGeneralRepository<MedicalRecord> MedicalRecordRepository => _medicalRecordRepository ??= new GeneralRepository<MedicalRecord>(Context);

        public IGeneralRepository<Patient> PatientRepository => _patientRepository ??= new GeneralRepository<Patient>(Context);

        public IGeneralRepository<Schedule> ScheduleRepository => _scheduleRepository ??= new GeneralRepository<Schedule>(Context);

        public IGeneralRepository<Specialization> SpecializationRepository => _specializationRepository ??= new GeneralRepository<Specialization>(Context);

        public IGeneralRepository<User> UserRepository => _userRepository ??= new GeneralRepository<User>(Context);

        public IGeneralRepository<Notification> NotificationRepository => _notificationRepository ??= new GeneralRepository<Notification>(Context);

        public IGeneralRepository<TEntity> GenericRepository<TEntity>() where TEntity : class
        {
            return new GeneralRepository<TEntity>(Context);
        }
        public IGeneralRepository<Message> MessageRepository =>
        _messageRepository ??= new GeneralRepository<Message>(_context);

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}