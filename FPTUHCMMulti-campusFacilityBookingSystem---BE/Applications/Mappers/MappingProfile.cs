using AutoMapper;
using Applications.DTOs.Request;
using Applications.DTOs.Response;
using DAL.Models;

namespace Applications.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ==================
            // AUTH & USER MAPPINGS
            // ==================

            // User → UserResponseDto
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.IsVerify, opt => opt.MapFrom(src => src.IsVerify.ToString()));

            // UpdateUserProfileDto → User (partial)
            CreateMap<UpdateUserProfileDto, User>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ==================
            // CAMPUS MAPPINGS
            // ==================
            
            // CreateCampusDto → Campus
            CreateMap<CreateCampusDto, Campus>()
                .ForMember(dest => dest.CampusId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // Campus → CampusResponseDto
            CreateMap<Campus, CampusResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            // UpdateCampusDto → Campus (partial)
            CreateMap<UpdateCampusDto, Campus>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ==================
            // ROLE MAPPINGS
            // ==================
            
            // CreateRoleDto → Role
            CreateMap<CreateRoleDto, Role>()
                .ForMember(dest => dest.RoleId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // Role → RoleResponseDto
            CreateMap<Role, RoleResponseDto>();

            // UpdateRoleDto → Role (partial)
            CreateMap<UpdateRoleDto, Role>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ==================
            // FACILITY TYPE MAPPINGS
            // ==================
            
            // CreateFacilityTypeDto → FacilityType
            CreateMap<CreateFacilityTypeDto, FacilityType>()
                .ForMember(dest => dest.TypeId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // FacilityType → FacilityTypeResponseDto
            CreateMap<FacilityType, FacilityTypeResponseDto>();

            // UpdateFacilityTypeDto → FacilityType (partial)
            CreateMap<UpdateFacilityTypeDto, FacilityType>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ==================
            // FACILITY MAPPINGS
            // ==================
            
            // CreateFacilityDto → Facility
            CreateMap<CreateFacilityDto, Facility>()
                .ForMember(dest => dest.FacilityId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // Facility → FacilityResponseDto
            CreateMap<Facility, FacilityResponseDto>()
                .ForMember(dest => dest.CampusName, opt => opt.MapFrom(src => src.Campus.Name))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.FacilityType.Name))
                .ForMember(dest => dest.FacilityManagerName, opt => opt.MapFrom(src => src.FacilityManager != null ? src.FacilityManager.FullName : null))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            // Facility → FacilityAvailabilityDto
            CreateMap<Facility, FacilityAvailabilityDto>()
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.FacilityType.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.BookedSlots, opt => opt.MapFrom(src => src.Bookings));

            // UpdateFacilityDto → Facility (partial)
            CreateMap<UpdateFacilityDto, Facility>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ==================
            // BOOKING MAPPINGS
            // ==================
            
            // CreateBookingDto → Booking
            CreateMap<CreateBookingDto, Booking>()
                .ForMember(dest => dest.BookingId, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            // Booking → BookingResponseDto
            CreateMap<Booking, BookingResponseDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.UserPhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.UserStudentId, opt => opt.MapFrom(src => src.User.StudentId))
                .ForMember(dest => dest.UserRoleName, opt => opt.MapFrom(src => src.User.Role != null ? src.User.Role.RoleName : null))
                .ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.Facility.Name))
                .ForMember(dest => dest.FacilityRoomNumber, opt => opt.MapFrom(src => src.Facility.RoomNumber))
                .ForMember(dest => dest.FacilityFloorNumber, opt => opt.MapFrom(src => src.Facility.FloorNumber))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.CheckInImages, opt => opt.MapFrom(src => 
                    string.IsNullOrEmpty(src.CheckInImages) 
                        ? null 
                        : System.Text.Json.JsonSerializer.Deserialize<List<string>>(src.CheckInImages, (System.Text.Json.JsonSerializerOptions)null!)))
                .ForMember(dest => dest.CheckOutImages, opt => opt.MapFrom(src => 
                    string.IsNullOrEmpty(src.CheckOutImages) 
                        ? null 
                        : System.Text.Json.JsonSerializer.Deserialize<List<string>>(src.CheckOutImages, (System.Text.Json.JsonSerializerOptions)null!)))
                .ForMember(dest => dest.Feedback, opt => opt.MapFrom(src => src.Feedback));

            // BookingFeedback → BookingFeedbackDto (for embedding in BookingResponseDto)
            CreateMap<BookingFeedback, BookingFeedbackDto>();

            // UpdateBookingDto → Booking (partial)
            CreateMap<UpdateBookingDto, Booking>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Booking → BookingSlot (for availability check)
            CreateMap<Booking, BookingSlot>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            // ==================
            // BOOKING FEEDBACK MAPPINGS
            // ==================
            
            // CreateBookingFeedbackDto → BookingFeedback
            CreateMap<CreateBookingFeedbackDto, BookingFeedback>()
                .ForMember(dest => dest.FeedbackId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            // BookingFeedback → BookingFeedbackResponseDto
            CreateMap<BookingFeedback, BookingFeedbackResponseDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.FacilityName, opt => opt.MapFrom(src => src.Booking.Facility.Name));

            // UpdateBookingFeedbackDto → BookingFeedback (partial)
            CreateMap<UpdateBookingFeedbackDto, BookingFeedback>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ==================
            // NOTIFICATION MAPPINGS
            // ==================
            
            // Notification → NotificationResponseDto
            CreateMap<Notification, NotificationResponseDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}

