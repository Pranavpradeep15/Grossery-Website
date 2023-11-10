using VegeSite.Models;
using VegeSite.Services.Contract;
using VegeSite.Utilities;
using BCrypt.Net;
using Azure;
using System.IdentityModel.Tokens.Jwt;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using VegeSite.Utilities;

namespace VegeSite.Services.Implementation
{
    public class UserServices : IUserServices
    {
        private readonly VegedataContext _companyContext;
        private readonly IConfiguration _configuration;

        public UserServices(VegedataContext companyContext, IConfiguration configuration)
        {
            _companyContext = companyContext;
            _configuration = configuration;
        }
        public VegetableRequest AddData(VegetableRequest vegetablerequest)
        {
            try
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(vegetablerequest.UserPassword);

                var newCustomer = new UserDetail
                {
                    UserPassword = hashedPassword,
                    Email = vegetablerequest.Email,


                };
                _companyContext.UserDetails.Add(newCustomer);
                _companyContext.SaveChanges();
                vegetablerequest.UserPassword = hashedPassword;
                return vegetablerequest;

            }
            catch (Exception EX)
            {

                throw EX;
            }
        }

        public string Verify(VegetableRequest vegetablerequest)
        {

            var storedCustomer = _companyContext.UserDetails.FirstOrDefault(c => c.Email == vegetablerequest.Email);

            if (storedCustomer == null)
            {
                return "Verification Failed";
            }
            if (!BCrypt.Net.BCrypt.Verify(vegetablerequest.UserPassword, storedCustomer.UserPassword))
            {
                return "Wrong Password";
            }

            var token = CreateToken(storedCustomer);
            return token;


        }
        public string CreateToken(UserDetail login)
        {
            List<Claim> claims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name,login.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
               _configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public List<Vegetable> GetDetails()
        {
            try
            {
                List<Vegetable> vegdetails = new List<Vegetable>();
                vegdetails = _companyContext.Vegetables.ToList();
                return vegdetails;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}






