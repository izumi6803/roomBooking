using DAL.Repositories.Interfaces;
using DAL.Repositories.Classes;
using DAL.Dbcontext;

namespace DAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepo { get; }
        IFacilityRepository FacilityRepo { get; }
        ICampusRepository CampusRepo { get; }
        IRoleRepository RoleRepo { get; }
        IFacilityTypeRepository FacilityTypeRepo { get; }
        IBookingRepository BookingRepo { get; }
        IBookingFeedbackRepository BookingFeedbackRepo { get; }
        INotificationRepository NotificationRepo { get; }
        ISystemSettingsRepository SystemSettingsRepo { get; }
        Task<int> SaveChangesAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly FacilityBookingDbContext _context;
        private IUserRepository? _userRepo;
        private IFacilityRepository? _facilityRepo;
        private ICampusRepository? _campusRepo;
        private IRoleRepository? _roleRepo;
        private IFacilityTypeRepository? _facilityTypeRepo;
        private IBookingRepository? _bookingRepo;
        private IBookingFeedbackRepository? _bookingFeedbackRepo;
        private INotificationRepository? _notificationRepo;
        private ISystemSettingsRepository? _systemSettingsRepo;

        public UnitOfWork(FacilityBookingDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepo => _userRepo ??= new UserRepository(_context);
        public IFacilityRepository FacilityRepo => _facilityRepo ??= new FacilityRepository(_context);
        public ICampusRepository CampusRepo => _campusRepo ??= new CampusRepository(_context);
        public IRoleRepository RoleRepo => _roleRepo ??= new RoleRepository(_context);
        public IFacilityTypeRepository FacilityTypeRepo => _facilityTypeRepo ??= new FacilityTypeRepository(_context);
        public IBookingRepository BookingRepo => _bookingRepo ??= new BookingRepository(_context);
        public IBookingFeedbackRepository BookingFeedbackRepo => _bookingFeedbackRepo ??= new BookingFeedbackRepository(_context);
        public INotificationRepository NotificationRepo => _notificationRepo ??= new NotificationRepository(_context);
        public ISystemSettingsRepository SystemSettingsRepo => _systemSettingsRepo ??= new SystemSettingsRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
