using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ZonartUsers.Models.Users
{
    public class FileFormModel
    {
        public IFormFile FormFile { get; set; }
    }
}
