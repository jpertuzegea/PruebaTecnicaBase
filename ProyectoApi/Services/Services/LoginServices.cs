//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Septiembre 2025</date>
//-----------------------------------------------------------------------

using Commons.Dtos.Configurations;
using Interfaces.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Services
{

    public class LoginServices : ILoginServices
    {
        private readonly IConfiguration configuration;

        public LoginServices(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        /// <inheritdoc />
        public async Task<ResultModel<LoginDto>> Login(LoginDto LoginDto)
        {
            ResultModel<LoginDto> ResultModel = new ResultModel<LoginDto>();

            try
            {
                if (string.IsNullOrWhiteSpace(LoginDto.UserName) || string.IsNullOrWhiteSpace(LoginDto.Password))
                {
                    ResultModel.HasError = false;
                    ResultModel.Data = null;
                    ResultModel.Messages = "Usuario y Clave son requeridos";
                    return ResultModel;
                }

                if (LoginDto.UserName.ToLower() == "Jorge".ToLower() || LoginDto.Password == "123456789")
                {
                    LoginDto.IsLogued = true;
                    LoginDto.Token = BuildToken();
                    LoginDto.Password = "";

                    ResultModel.HasError = false;
                    ResultModel.Data = LoginDto;
                    ResultModel.Messages = "Usuario Logueado Con Exito";
                }
                else
                {
                    LoginDto.IsLogued = false;
                    LoginDto.Token = "";
                    LoginDto.Password = "";

                    ResultModel.HasError = true;
                    ResultModel.Data = LoginDto;
                    ResultModel.Messages = "Usuario NO Logueado";

                }

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = $"Error Técnico Al Iniciar Sesion: {Error.Message}";
                ResultModel.Data = null;
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }


        private string BuildToken()
        {
            int user = 1;

            var Claims = new[] {
                new Claim(JwtRegisteredClaimNames.UniqueName, "jpertuzegea@hotmail.com"),

                new Claim("UserId", user.ToString()),
                new Claim("UserEmail", "jpertuzegea@hotmail.com"),
                new Claim("UserFullName", "Jorge David Pertuz Egea"),
                new Claim("UserNetwork", "JpertuzEgea"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            JWTAuthentication JWTAuthenticationSection = configuration.GetSection("JWTAuthentication").Get<JWTAuthentication>();

            SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTAuthenticationSection.Secret));
            var Credenciales = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            DateTime Expiration = DateTime.Now.AddMinutes(JWTAuthenticationSection.ExpirationInMinutes);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "",
               audience: "",
               claims: Claims,
               expires: Expiration,
               signingCredentials: Credenciales,
               notBefore: DateTime.Now.AddMilliseconds(2)
               );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}