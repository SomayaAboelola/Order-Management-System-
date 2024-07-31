
namespace Order_Management_System.Controllers
{
 
    public class UsersController : BaseAPIController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenServices _tokenServices;

        public UsersController(UserManager<IdentityUser> userManager ,
                            SignInManager<IdentityUser> signInManager , 
                            ITokenServices tokenServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenServices = tokenServices;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Login(LoginDto input)
        {
          
            var user=await _userManager.FindByEmailAsync(input.Email);
            

            if (user is null)
                return  Unauthorized(new ResponseApi(401, "Invalid Login"));

            var result = await _signInManager.CheckPasswordSignInAsync(user, input.Password ,false);
           
            if(!result.Succeeded)
                return Unauthorized(new ResponseApi(401, "Invalid Login"));

            return Ok(new UserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await _tokenServices.CreateTokenAsync(user, _userManager)
            });

        }
        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Register(RegisterDto input)
        {
            var user = new IdentityUser()
            {
                UserName = input.UserName,
                Email = input.Email,
                PhoneNumber = input.Phone

            };
            var result = await _userManager.CreateAsync(user, input.Password);
            if (!result.Succeeded)
                return BadRequest(new ValidationErrorResponse { Errors = result.Errors.Select(e => e.Description) });

            return Ok(new UserDto()
            {
                UserName = input.UserName,
                Email = input.Email,
                Token = await _tokenServices.CreateTokenAsync(user, _userManager)
            });

        }


    }
}
