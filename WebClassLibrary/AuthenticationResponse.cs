using CaseStudy1.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRoleProcess;

namespace WebClassLibrary
{
    public class AuthenticationResponse
    {
        public int UserId { get; set; }
        
        public string Token { get; set; }

        public AuthenticationResponse(User userDetail, string token)
        {
            Token = token;
            UserId = userDetail.UserId;
        }
        public AuthenticationResponse() { }
    }
}
