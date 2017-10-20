using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using AspNet.Security.OpenIdConnect.Server;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Identity;
using AspNet.Security.OpenIdConnect.Extensions;

namespace AspNetCore.Common.Web.Providers
{
    public sealed class OpenIdOAuthProvider : OpenIdConnectServerProvider
    {
        //public override Task ValidateAuthorizationRequest(ValidateAuthorizationRequestContext context)
        //{

        //    // Note: the OpenID Connect server middleware supports the authorization code, implicit and hybrid flows
        //    // but this authorization provider only accepts response_type=code authorization/authentication requests.
        //    // You may consider relaxing it to support the implicit or hybrid flows. In this case, consider adding
        //    // checks rejecting implicit/hybrid authorization requests when the client is a confidential application.

        //    if (!context.Request.IsAuthorizationCodeFlow())
        //    {
        //        context.Reject(
        //            error: OpenIdConnectConstants.Errors.UnsupportedResponseType,
        //            description: "Only the authorization code flow is supported by this authorization server.");

        //        return Task.FromResult(0);
        //    }

        //    // Note: to support custom response modes, the OpenID Connect server middleware doesn't
        //    // reject unknown modes before the ApplyAuthorizationResponse event is invoked. To ensure
        //    // invalid modes are rejected early enough, a check is made here.
        //    if (!string.IsNullOrEmpty(context.Request.ResponseMode) && !context.Request.IsFormPostResponseMode() &&
        //                                                               !context.Request.IsFragmentResponseMode() &&
        //                                                               !context.Request.IsQueryResponseMode())
        //    {
        //        context.Reject(
        //            error: OpenIdConnectConstants.Errors.InvalidRequest,
        //            description: "The specified response_mode is unsupported.");

        //        return Task.FromResult(0);
        //    }

        //    context.Validate();

        //    return Task.FromResult(0);
        //}

        //public override Task ValidateTokenRequest(ValidateTokenRequestContext context)
        //{

        //    if (context.Request.IsAuthorizationCodeGrantType())
        //    {
        //        context.Reject(
        //         error: OpenIdConnectConstants.Errors.UnsupportedGrantType,
        //         description: "Only grant_type=client_credentials or grant_type=password or" +
        //                      "grant_type=refresh_token are accepted by this server.");

        //        return Task.FromResult(0);
        //    }

        //    // Since there's only one application and since it's a public client (i.e a client that
        //    // cannot keep its credentials private), call Skip() to inform the server the request
        //    // should be accepted without enforcing client authentication. context.Skip();
        //    //context.Validate();
        //    if (context.Request.IsPasswordGrantType())
        //    {
        //        context.Skip();
        //    }

        //    if (context.Request.IsClientCredentialsGrantType())
        //    {
        //        context.Validate();
        //    }

        //    return Task.FromResult(0);
        //}

        //public async override Task HandleTokenRequest(HandleTokenRequestContext context)
        //{
        //    // Only handle grant_type=password token requests and let the OpenID Connect server
        //    // middleware handle the other grant types.
        //    if (context.Request.IsClientCredentialsGrantType())
        //    {
        //        // Using password derivation and a time-constant comparer is STRONGLY recommended.
        //        if (!string.Equals(context.Request.ClientId, "Platform", StringComparison.Ordinal) ||
        //            !string.Equals(context.Request.ClientSecret, "eyJ0eXAiOiJKV1QiLCJhbGciOiJIU", StringComparison.Ordinal))
        //        {
        //            context.Reject(
        //                error: OpenIdConnectConstants.Errors.InvalidGrant,
        //                description: "Invalid user credentials.");

        //            return;
        //        }

        //        // Create a new ClaimsIdentity containing the claims that will be used to create an
        //        // id_token and/or an access token.
        //        var identity = new ClaimsIdentity(OpenIdConnectServerDefaults.AuthenticationScheme);
        //        identity.AddClaim(OpenIdConnectConstants.Claims.Subject, "Platform");
        //        identity.AddClaim(ClaimTypes.NameIdentifier, "Platform");
        //        identity.AddClaim(ClaimTypes.Role, "Platform");

        //        // Create a new authentication ticket holding the user identity.
        //        var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), new AuthenticationProperties(),
        //            OpenIdConnectServerDefaults.AuthenticationScheme);

        //        context.Validate(ticket);
        //    }

        //    if (context.Request.IsPasswordGrantType())
        //    {

        //        // Implement context.Request.Username/context.Request.Password validation here.
        //        // Note: you can call context Reject() to indicate that authentication failed.
        //        // Using password derivation and time-constant comparer is STRONGLY recommended.
        //        var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager>();
        //        //signInManager.CheckPasswordSignInAsync()
        //        var appUserManager = context.HttpContext.RequestServices.GetRequiredService<AppUserManager>();
        //        var appUser = await appUserManager.FindByNameAsync(context.Request.Username);
        //        var signResult = await signInManager.CheckPasswordSignInAsync(appUser, context.Request.Password, false);

        //        if (!signResult.Succeeded)
        //        {
        //            context.Reject(
        //                error: OpenIdConnectConstants.Errors.AccessDenied,
        //                description: "Invalid user credentials.");

        //            return;
        //        }

        //        var identity = new ClaimsIdentity(context.Scheme.Name);

        //        identity.AddClaim(OpenIdConnectConstants.Claims.Subject, context.Request.Username);

        //        // By default, claims are not serialized in the access/identity tokens.
        //        // Use the overload taking a "destinations" parameter to make sure
        //        // your claims are correctly inserted in the appropriate tokens.
        //        identity.AddClaim(ClaimTypes.Name, appUser.UserName, OpenIdConnectConstants.Destinations.AccessToken);
        //        identity.AddClaim(ClaimTypes.NameIdentifier, appUser.Id, OpenIdConnectConstants.Destinations.AccessToken);
        //        identity.AddClaim(ClaimTypes.MobilePhone, appUser.PhoneNumber, OpenIdConnectConstants.Destinations.AccessToken);

        //        //identity.AddClaim("type", ((int)user.Type).ToString(),
        //        //    OpenIdConnectConstants.Destinations.AccessToken);

        //        var ticket = new AuthenticationTicket(
        //            new ClaimsPrincipal(identity),
        //            new AuthenticationProperties(),
        //            context.Scheme.Name);

        //        // Call SetScopes with the list of scopes you want to grant
        //        // (specify offline_access to issue a refresh token).
        //        ticket.SetScopes(OpenIdConnectConstants.Scopes.OfflineAccess);

        //        context.Validate(ticket);
        //    }
        //}
    }
}