using EMS.Data.dbo_Models;
using System;

namespace EMS.Services.Factories.Contracts
{
    public interface IAttachmentFactory
    {
        DboAttachment CreateAttachment(string name, double sizeMb, Guid emailId);
    }
}
