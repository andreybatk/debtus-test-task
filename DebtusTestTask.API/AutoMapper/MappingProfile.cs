using AutoMapper;
using DebtusTestTask.API.Contracts;
using DebtusTestTask.DB.Entities;

namespace DebtusTestTask.API.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewEmployeeRequest, Employee>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.WorkShifts, opt => opt.Ignore());

            CreateMap<UpdateEmployeeRequest, Employee>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.WorkShifts, opt => opt.Ignore());

            CreateMap<Employee, EmployeeResponse>();
            CreateMap<WorkShift, WorkShiftResponse>();
        }
    }
}