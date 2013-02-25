#region Using directives

using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using TCESS.ESales.BusinessLayer.Interfaces;
using TCESS.ESales.CommonLayer.Unity;
using TCESS.ESales.DataTransferObjects;
using TCESS.ESales.PersistenceLayer.Entity;
using TCESS.ESales.PersistenceLayer.Interfaces;

#endregion

namespace TCESS.ESales.BusinessLayer.Services
{
    public class AmeBlockService : IAmeBlockService
    {
        /// <summary>
        /// Get Block List
        /// </summary>
        /// <returns></returns>
        public IList<AmeBlockDTO> GetAmeBlockList()
        {
            List<AmeBlockDTO> listAMEBlock = new List<AmeBlockDTO>();
            List<ameblock> listAMEBlocksEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<ameblock>>()
                .GetQuery().Where(item => item.Blocks_IsDeleted == false)
                .OrderBy(order => order.Blocks_Name).ToList();

            AutoMapper.Mapper.Map(listAMEBlocksEntity, listAMEBlock);
            return listAMEBlock;
        }

        /// <summary>
        /// To get customer belong to a Perticular Block
        /// </summary>
        /// <param name="BlocksId"></param>
        /// <returns></returns>
        public IList<CustomerDTO> GetCustomerAssociatedForAmeBlock(int blockId)
        {
            List<CustomerDTO> listCustomer = new List<CustomerDTO>();
            List<customer> listCustomersEntity = ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>().GetQuery()
                .Where(item => item.Cust_IsDeleted == false && item.Cust_AMEBlockId == blockId)
                .OrderBy(order => order.Cust_FirmName).ToList();

            AutoMapper.Mapper.Map(listCustomersEntity, listCustomer);
            return listCustomer;
        }

        /// <summary>
        /// Update customer in AmeBlock
        /// </summary>
        /// <param name="customerDetails"></param>
        public int UpdateCustomerAmeBlock(CustomerDTO customerDetails)
        {
            if (customerDetails != null)
            {
                customer customerEntity = new customer();
                AutoMapper.Mapper.Map(customerDetails, customerEntity);
                ESalesUnityContainer.Container.Resolve<IGenericRepository<customer>>().Update(customerEntity);
            }
            return customerDetails.Cust_Id;
        }

        /// <summary>
        /// Save and Update AmeBlock
        /// </summary>
        /// <param name="ameBlockDetail"></param>
        /// <returns></returns>
        public int SaveAndUpdateAmeBlock(AmeBlockDTO ameBlockDetail)
        {
            ameblock ameblockEntity = new ameblock();
            AutoMapper.Mapper.Map(ameBlockDetail, ameblockEntity);

            if (ameBlockDetail.Blocks_Id > 0)
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<ameblock>>().Update(ameblockEntity);
            }
            else
            {
                ESalesUnityContainer.Container.Resolve<IGenericRepository<ameblock>>().Save(ameblockEntity);
            }
            return ameblockEntity.Blocks_Id;
        }

        /// <summary>
        /// Get AmeBlock list by BlockId
        /// </summary>
        /// <param name="blockId"></param>
        /// <returns></returns>
        public AmeBlockDTO GetAmeBlockListByBlockId(int blockId)
        {
            AmeBlockDTO objAMEBlockDTO = new AmeBlockDTO();
            AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<ameblock>>()
                .GetSingle(item => item.Blocks_Id == blockId), objAMEBlockDTO);
            return objAMEBlockDTO;
        }

        /// <summary>
        /// To check wheather a block exists or not
        /// </summary>
        /// <param name="blockId"></param>
        /// <param name="blockName"></param>
        /// <returns></returns>
        public bool AmeBlockExists(int blockId, string blockName)
        {
            AmeBlockDTO ameBlockDetails = new AmeBlockDTO();
            bool result = false;

            if (blockId == 0)
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<ameblock>>()
                    .GetSingle(item => item.Blocks_Name == blockName && item.Blocks_IsDeleted == false), ameBlockDetails);
            }
            else
            {
                AutoMapper.Mapper.Map(ESalesUnityContainer.Container.Resolve<IGenericRepository<ameblock>>()
                    .GetSingle(item => item.Blocks_Name == blockName && item.Blocks_IsDeleted == false
                        && item.Blocks_Id != blockId), ameBlockDetails);
            }

            if (ameBlockDetails.Blocks_Id > 0)
            {
                result = true;
            }
            return result;
        }
    }
}