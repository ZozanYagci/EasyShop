using Core.Entities.Concrete;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper:ITokenHelper
    {

        private readonly TokenOptions _tokenOptions;


        public JwtHelper(TokenOptions tokenOptions)
        {
            _tokenOptions = tokenOptions;
        }

        public AccessToken CreateToken(AuthUser user, List<OperationClaim> operationClaims)
        {
            
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecretKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            var tokenHandler = new JwtSecurityTokenHandler();
            var token= tokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = jwt.ValidTo
            };
        }

        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, AuthUser user, SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var claims= SetClaims(user, operationClaims);

            return new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration),
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials,
                claims: claims
                );
        }

        private IEnumerable<Claim> SetClaims(AuthUser user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };
            
            claims.AddRange(operationClaims.Select(c => new Claim(ClaimTypes.Role, c.Name)));
            return claims;

        }
    }
}
