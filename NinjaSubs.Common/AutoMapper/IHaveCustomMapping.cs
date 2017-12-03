using AutoMapper;

namespace NinjaSubs.Common.AutoMapper
{ 
    public interface IHaveCustomMapping
    {   
        void ConfigureMapping(Profile profile);
    }
}
