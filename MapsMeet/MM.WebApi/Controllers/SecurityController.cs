using MM.ApplicationCore.Entities;
using MM.ApplicationCore.Utilities;

namespace MM.WebApi.Controllers
{
    #region Namespaces

    using MM.Infrastructure.Repository;
    using MM.WebApi.Models;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.IO;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web.Http;

    #endregion

    [RoutePrefix("api/security")]
    public class SecurityController : ApiController
    {
        //[HttpPost]
        //[Route("authenticate")]
        //public IHttpActionResult Authenticate([FromBody] LoginRequest login)
        //{
        //    IHttpActionResult response;
        //    var loginResponse = new LoginResponse { };
        //    try
        //    {
        //        //string userName = DecryptStringAES(login.Username).ToLower();
        //        //string password = DecryptStringAES(login.Password);

        //        string userName =login.Username.ToLower();
        //        string password = login.Password;

        //        HttpResponseMessage responseMsg = new HttpResponseMessage();

        //        // hash and save a password
        //        string hashedPassword = BCrypt.HashPassword("bluepi@123"); // Should be used at client end to hash the password and send to the api

        //        EmployeeData employee = GetEmployee(userName, password);

        //        // if credentials are valid
        //        if (employee != null)
        //        {
	       //         string token = createToken(userName, employee.Roles.OrderBy(r => r.Id).First().Name);
        //            employee.token = token;

        //            return Ok<EmployeeData>(employee);
        //        }
        //        else
        //        {
        //            // if credentials are not valid send unauthorized status code in response
        //            loginResponse.responseMsg.StatusCode = HttpStatusCode.Unauthorized;
        //            response = ResponseMessage(loginResponse.responseMsg);
        //            return response;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        loginResponse.responseMsg.StatusCode = HttpStatusCode.InternalServerError;
        //        response = ResponseMessage(loginResponse.responseMsg);
        //        return response;
        //    }
        //}

        //[HttpPost]
        //[Route("reset")]
        //public bool Reset([FromBody] ResetPassword reset)
        //{
        //    bool status = false;
        //    try
        //    {
	       //     int userId = reset.UserId;
        //        string password = reset.Password;

        //        string hashedPassword = BCrypt.HashPassword(password);

        //        status = AccountRepository.ResetPassword(userId, hashedPassword);

        //        if (status)
        //        {
        //            //SendMail.SendAsyncMail(userName);
        //            Thread email = new Thread(delegate ()
        //            {
	       //             SendMail.Send(reset.email);
        //            });

        //            email.IsBackground = true;
        //            email.Start();
        //        }

        //        return status;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        [HttpPost]
        [Route("user/add")]
        public User Add([FromBody] Users user)
        {
            User usr = new User();
            Users result = new Users();
            string token = "";

            if (user.LoginType.Equals("facebook") || user.LoginType.Equals("google"))
            {
                token = user.Token;
            }
            else if (user.LoginType.Equals("mobile"))
            {
                token = SecurityManager.Encrypt(user.MobileNo);
            }

            try
            {
                result = AccountRepository<Users>.CheckUser(token);
                if (result == null)
                {
                    Users userInfo = new Users
                    {
                        UserId = 1,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Gender = user.Gender,
                        DOB = user.DOB,
                        MobileNo = user.MobileNo,
                        Token = token,
                        LoginType = user.LoginType,
                        CreatedOn = user.CreatedOn,
                        UpdatedOn = user.UpdatedOn
                    };
                    result = AccountRepository<Users>.AddUser(userInfo);
                }

                if (result == null)
                {
                    usr.status = "failed";
                    usr.message = "Invalid User Info";
                }
                else
                {
                    usr.status = "success";
                    usr.message = "";
                    usr.user_id = result.Token;
                    usr.name = result.FirstName + " " + result.LastName;
                    usr.dob = result.DOB;
                    usr.gender = result.Gender;
                    usr.mobile = result.MobileNo;
                    usr.login_type = result.LoginType;
                    usr.email = result.Email;
                    usr.profile_pic = result.ImageUrl;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                usr.status = "failed";
                usr.message = ex.Message;
            }
            return usr;
        }

        #region  Encrypt Decrypt

        //[HttpGet]
        //[Route("encryption/{email}/{name}")]
        //public string[] Encrypt(string email,string name)
        //{
        //    string[] data = new string[2];

        //    data[0] = SecurityManager.Encrypt(email);
        //    data[1] = SecurityManager.Encrypt(name);
        //    return data; ;
        //}

        //[HttpPost]
        //[Route("decryption")]
        //public string[] Decrypt([FromBody] string[] userData)
        //{
        //    string[] data = new string[2];

        //    data[0] = SecurityManager.Decrypt(userData[0]);
        //    data[1] = SecurityManager.Decrypt(userData[1]);
        //    return data; ;
        //}

        #endregion

        #region Create Token
        
        //private string createToken(string username, string roleList)
        //{
        //    //Set issued at date
        //    DateTime issuedAt = DateTime.UtcNow;
        //    //set the time when it expires
        //    DateTime expires = DateTime.UtcNow.AddDays(30);

        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    string roles = roleList;

        //    //foreach (var role in roleList)
        //    //{
        //    //    roles += role + ",";
        //    //}

        //    //create a identity and add claims to the user which we want to log in
        //    ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
        //    {
        //        new Claim(ClaimTypes.Name, username)
        //        //new Claim(ClaimTypes.Role,roles.Substring(0,roles.Length-1))
        //    });

        //    const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
        //    var now = DateTime.UtcNow;
        //    var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
        //    var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);

        //    //create the jwt
        //    var token =
        //        (JwtSecurityToken)tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:65070", audience: "http://localhost:65070",
        //              subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
        //    var tokenString = tokenHandler.WriteToken(token);

        //    return tokenString;
        //}

        //    private EmployeeData GetEmployee(string userName, string password)
        //    {
        //        EmployeeData emp = new EmployeeData();
        //        Employee employee = AccountRepository.Login(userName);

        //        if (employee != null)
        //        {
        //            // check a password
        //            bool validPassword = BCrypt.Verify(password, employee.Password);
        //            if (!validPassword)
        //                return emp = null;

        //            emp.Name = employee.Name;
        //            emp.EmailId = employee.Email;
        //            emp.EmployeeId = employee.Id;
        //            emp.EmployeeNo = employee.EmployeeNo;
        //            emp.ContactNo = employee.ContactNo;
        //            emp.ImageUrl = employee.ImageUrl;
        //            emp.NewUser = employee.NewUser;

        //         emp.ReportingManager = employee.ReportingManager != null
        //          ? new Data {Id = employee.ReportingManager.Id, Name = employee.ReportingManager.Name}
        //          : null;
        //            emp.Department = employee.Department != null
        //             ? new Data { Id = employee.Department.Id, Name = employee.Department.Name }
        //             : null;
        //emp.Designation = employee.Designation != null
        //	? new Data { Id = employee.Designation.Id, Name = employee.Designation.Name }
        //	: null;
        //emp.Organization = employee.Organization != null
        //	? new Data { Id = employee.Organization.Id, Name = employee.Organization.Name }
        //	: null;

        //List<Data> roles = new List<Data>();
        //            foreach (var employeeRole in employee.Roles)
        //            {
        //             roles.Add(new Data {Id = employeeRole.Id, Name = employeeRole.Name});
        //            }
        //            emp.Roles = roles;

        //         var currentCycle = MasterRepository.GetCurrentCycle();

        //         emp.ReviewCycle = new Data() {Id = currentCycle.Id, Name = currentCycle.Name};
        //        }
        //        else
        //            emp = null;

        //        return emp;
        //    }

        #endregion
        
        public string DecryptStringAES(string cipherText)
        {
            var keybytes = Encoding.UTF8.GetBytes("7061737323313233");
            var iv = Encoding.UTF8.GetBytes("7061737323313233");

            var encrypted = Convert.FromBase64String(cipherText);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
            return string.Format(decriptedFromJavascript);
        }

        private string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.  
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold  
            // the decrypted text.  
            string plaintext = null;

            // Create an RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings  
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for decryption.  
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {

                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream  
                                // and place them in a string.  
                                plaintext = srDecrypt.ReadToEnd();

                            }

                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }
            return plaintext;
        }
    }
}
