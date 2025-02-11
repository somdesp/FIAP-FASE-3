//using Microsoft.AspNetCore.Identity;

//namespace FIAP.TECH.AUTH
//{
//    public class SystemUser : IdentityUser
//    {
//        public string Name { get; set; }
//        public string Phone { get; set; }
//        public string Address { get; set }
//    }
//    public interface IAuthService
//    {
//        Task<Response<IdentityResult>> RegisterSystemUser(SystemRegisterUserDTO user);
//        Task<Response<LoginResponseDTO>> LoginSystemUser(SystemSignInUserDTO credentials);
//    }
//    public class AuthService : IAuthService
//    {
//        private readonly SignInManager<SystemUser> _signInManager;
//        private readonly UserManager<SystemUser> _userManager;

//        public AuthService(SignInManager<SystemUser> signInManager,
//            UserManager<SystemUser> userManager)
//        {
//            _signInManager = signInManager;
//            _userManager = userManager;
//        }
//        public async Task<Response<LoginResponseDTO>> LoginSystemUser(SystemSignInUserDTO credentials)
//        {
//            var user = await _userManager.FindByEmailAsync(credentials.Email);
//            if (user == null)
//            {
//                return new()
//                {
//                    Success = false,
//                    Data = new LoginResponseDTO() { },
//                    Message = "Email or password is incorrect",
//                };
//            }
//            var result = await _signInManager.PasswordSignInAsync(user.UserName, credentials.Password, false, true);
//            if (!result.Succeeded)
//            {
//                return new()
//                {
//                    Success = false,
//                    Data = new LoginResponseDTO() { },
//                    Message = "Email or password is incorrect",
//                };
//            }
//            return new Response<LoginResponseDTO>()
//            {
//                Message = "Login Successfull!",
//                Data = new()
//                {
//                    Id = user.Id,
//                    Username = user.UserName,
//                    Name = user.Name,
//                    Email = user.Email,
//                    Phone = user.Phone,
//                    Address = user.Address,
//                },
//            };
//        }

//        public async Task<Response<IdentityResult>> RegisterSystemUser(SystemRegisterUserDTO user)
//        {
//            SystemUser _user = new()
//            {
//                UserName = user.Username,
//                Email = user.Email,
//                Name = user.Name,
//            };
//            var result = await _userManager.CreateAsync(_user, user.Password);
//            return new Response<IdentityResult>()
//            {
//                Success = true,
//                Data = result,
//                Message = result.Succeeded ? "User Registration Successfull!" : "User Registration Failed!"
//            };
//        }
//    }
//}
