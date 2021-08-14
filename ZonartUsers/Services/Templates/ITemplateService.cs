using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZonartUsers.Services.Templates
{
    public interface ITemplateService
    {
        bool Edit(
           int templateId,
           string name,
           double price,
           string description,
           string imageUrl);
    }
}
