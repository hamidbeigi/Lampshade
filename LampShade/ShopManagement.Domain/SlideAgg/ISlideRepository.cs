using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SliderAgg;
using System.Collections.Generic;

namespace ShopManagement.Domain.SlideAgg
{
    public interface ISlideRepository:IRepository<long,Slide>
    {
        EditSlide GetDetails(long id);
        List<SlideViewModel> GetList();
    }
}
