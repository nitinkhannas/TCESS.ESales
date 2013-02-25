#region Using directives

using System.Collections.Generic;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.DataTransferObjects.Users;

#endregion

namespace TCESS.ESales.BusinessLayer.Interfaces.Users
{
    public interface IUserAgentService
    {
        void SaveUserAgentMapping(UserAgentMappingDTO userAgentMapDetails);
        bool CheckIfAgentNotReferenced(int agentId);
        UserAgentMappingDTO GetAgentByUserId(int userId);
        void DeleteUserAgentMapping(int userAgentMapId);
        void UpdateUserAgentDetails(UserAgentMappingDTO userAgentMapDetails);
        UserAgentMappingDTO GetUserAgentMappingByMappingId(int userAgentMapId);
        IList<UserAgentMappingDTO> GetUsersAndAgentDetails();
    }
}