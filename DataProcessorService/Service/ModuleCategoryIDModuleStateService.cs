using DataProcessorService.Entitys.Entity;
using DataProcessorService.Entitys.EntityForDataBase;
using DataProcessorService.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace DataProcessorService.Service
{
    public class ModuleCategoryIDModuleStateService
    {
        private ModuleCatIDModuleStateRepository _moduleCatIDModuleStateRepository;
        public ModuleCategoryIDModuleStateService(ModuleCatIDModuleStateRepository moduleCatIDModuleStateRepository)
        {
            this._moduleCatIDModuleStateRepository = moduleCatIDModuleStateRepository;
        }           
        public void AddModuleGategoryIDModuleState(string ModuleCategoryId, string ModuleState)
        {      
            ModuleCategoreIDModeleStateEntity moduleCategoreIDModeleState = new ModuleCategoreIDModeleStateEntity();

            moduleCategoreIDModeleState.ModuleCategoreID = ModuleCategoryId;
            moduleCategoreIDModeleState.ModeleState = ModuleState;

            bool answer = _moduleCatIDModuleStateRepository.GetBoolAnswer(moduleCategoreIDModeleState.ModuleCategoreID);

            if (answer)
            {
                _moduleCatIDModuleStateRepository.Update(moduleCategoreIDModeleState);
            }
            else
            {
                _moduleCatIDModuleStateRepository.Add(moduleCategoreIDModeleState);
            }
        }

        public void StringProcessing(string massage)
        {
            Console.WriteLine(massage);
            XMLFileModelEntity xMLFileModel = JsonConvert.DeserializeObject<XMLFileModelEntity>(massage);

            if (xMLFileModel.Elements != null)
            {
                List<XMLFileModelEntity> listDiviceStatus = new List<XMLFileModelEntity>();
                listDiviceStatus = xMLFileModel.Elements.Where(q => q.ElementTagName == "DeviceStatus").ToList();

                if (listDiviceStatus != null)
                {

                    foreach (var item in listDiviceStatus)
                    {
                        IntermediateEntity gfghhf = new IntermediateEntity();
                        GetModelIdModelStatus(item, gfghhf);

                        if (gfghhf.ModuleCategoryID != null && gfghhf.ModuleState != null)
                        {
                            AddModuleGategoryIDModuleState(gfghhf.ModuleCategoryID, gfghhf.ModuleState);
                        }
                    }
                }
            }
        }

        public void GetModelIdModelStatus(XMLFileModelEntity xMLFileModel, IntermediateEntity intermediateEntity)
        {
            if (xMLFileModel.ElementTagName == "ModuleCategoryID")
            {
                intermediateEntity.ModuleCategoryID = xMLFileModel.ElementValue;
            }
            if (xMLFileModel.ElementTagName == "ModuleState")
            {
                intermediateEntity.ModuleState = xMLFileModel.ElementValue;
            }

            if (xMLFileModel.Elements != null)
            {
                foreach (var item in xMLFileModel.Elements)
                {
                    GetModelIdModelStatus(item, intermediateEntity);
                }
            }
        }
    }
}
