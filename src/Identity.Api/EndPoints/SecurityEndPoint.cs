﻿using Identity.Api.DbContext;
using Identity.Api.Entities;
using Identity.Api.Hasher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Identity.Api.DTOs.DTOs;

namespace Identity.Api.EndPoints
{
    public static class SecurityEndPoint
    {

        public static void MapSecurityEndPoint(this IEndpointRouteBuilder routes, JWT jwt)
        {

            var group = routes.MapGroup("api/security");
            group.MapPost("/getToken", [AllowAnonymous] async (UserDto user, IdentityMSContext db) =>
            {

                PasswordHasher<UserDto> passwordHasher = new PasswordHasher<UserDto>();
                var dbUser = await db.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
                if (dbUser != null)
                {
                    var ok = passwordHasher.VerifyHashedPassword(user, dbUser.PasswordHash, user.Password);
                    var issuer = jwt.Issuer;
                    var audience = jwt.Audience;
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    // Now its ime to define the jwt token which will be responsible of creating our tokens
                    var jwtTokenHandler = new JwtSecurityTokenHandler();

                    // We get our secret from the appsettings
                    var key = Encoding.ASCII.GetBytes(jwt.Key);

                    // we define our token descriptor
                    // We need to utilise claims which are properties in our token which gives information about the token
                    // which belong to the specific user who it belongs to
                    // so it could contain their id, name, email the good part is that these information
                    // are generated by our server and identity framework which is valid and trusted
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                new Claim("Id", "1"),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                // the JTI is used for our refresh token which we will be convering in the next video
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                        // the life span of the token needs to be shorter and utilise refresh token to keep the user signedin
                        // but since this is a demo app we can extend it to fit our current need
                        Expires = DateTime.UtcNow.AddHours(6),
                        Audience = audience,
                        Issuer = issuer,
                        // here we are adding the encryption alogorithim information which will be used to decrypt our token
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                    };

                    var token = jwtTokenHandler.CreateToken(tokenDescriptor);

                    var jwtToken = jwtTokenHandler.WriteToken(token);

                    return Results.Ok(jwtToken);
                }
                else
                {
                    return Results.Unauthorized();
                }
            });
            //group.MapGet("/items", [Authorize] async (IdentityMSContext db) =>
            //{
            //    return await db.Items.ToListAsync();
            //});

            //group.MapPost("/items", [Authorize] async (IdentityMSContext db, Item item) => {
            //    if (await db.Items.FirstOrDefaultAsync(x => x.Id == item.Id) != null)
            //    {
            //        return Results.BadRequest();
            //    }

            //    db.Items.Add(item);
            //    await db.SaveChangesAsync();
            //    return Results.Created($"/Items/{item.Id}", item);
            //});

            //group.MapGet("/items/{id}", [Authorize] async (IdentityMSContext db, int id) =>
            //{
            //    var item = await db.Items.FirstOrDefaultAsync(x => x.Id == id);

            //    return item == null ? Results.NotFound() : Results.Ok(item);
            //});

            //group.MapPut("/items/{id}", [Authorize] async (IdentityMSContext db, int id, Item item) =>
            //{
            //    var existItem = await db.Items.FirstOrDefaultAsync(x => x.Id == id);
            //    if (existItem == null)
            //    {
            //        return Results.BadRequest();
            //    }

            //    existItem.Title = item.Title;
            //    existItem.IsCompleted = item.IsCompleted;

            //    await db.SaveChangesAsync();
            //    return Results.Ok(item);
            //});

            //group.MapDelete("/items/{id}", [Authorize] async (IdentityMSContext db, int id) =>
            //{
            //    var existItem = await db.Items.FirstOrDefaultAsync(x => x.Id == id);
            //    if (existItem == null)
            //    {
            //        return Results.BadRequest();
            //    }

            //    db.Items.Remove(existItem);
            //    await db.SaveChangesAsync();
            //    return Results.NoContent();
            //});
        }
    }
}
