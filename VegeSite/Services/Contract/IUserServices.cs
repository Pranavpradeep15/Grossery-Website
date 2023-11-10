using VegeSite.Models;
using VegeSite.Utilities;
using VegeSite.Utilities;

namespace VegeSite.Services.Contract
{
    public interface IUserServices
    {
        public VegetableRequest AddData(VegetableRequest vegetablerequest);
        public string Verify(VegetableRequest vegetablerequest);
        public string CreateToken(UserDetail login);

        public List<Vegetable> GetDetails();




    }
}
