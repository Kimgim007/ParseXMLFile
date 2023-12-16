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
          
            ModuleCategoreIDModeleState moduleCategoreIDModeleState = new ModuleCategoreIDModeleState();

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
            //E:\Не брак

            Console.WriteLine(massage);
            XMLFileModel xMLFileModel = JsonConvert.DeserializeObject<XMLFileModel>(massage);

            if (xMLFileModel.Elements != null)
            {
                List<XMLFileModel> listDiviceStatus = new List<XMLFileModel>();
                listDiviceStatus = xMLFileModel.Elements.Where(q => q.ElementTagName == "DeviceStatus").ToList();

                if (listDiviceStatus != null)
                {

                    foreach (var item in listDiviceStatus)
                    {
                        Gfghhf gfghhf = new Gfghhf();
                        GetModelIdModelStatus(item, gfghhf);

                        if (gfghhf.ModuleCategoryID != null && gfghhf.ModuleState != null)
                        {
                            AddModuleGategoryIDModuleState(gfghhf.ModuleCategoryID, gfghhf.ModuleState);
                        }
                    }
                }
            }
        }

        public void GetModelIdModelStatus(XMLFileModel xMLFileModel, Gfghhf gfghhf)
        {
            if (xMLFileModel.ElementTagName == "ModuleCategoryID")
            {
                gfghhf.ModuleCategoryID = xMLFileModel.ElementValue;
            }
            if (xMLFileModel.ElementTagName == "ModuleState")
            {
                gfghhf.ModuleState = xMLFileModel.ElementValue;
            }

            if (xMLFileModel.Elements != null)
            {
                foreach (var item in xMLFileModel.Elements)
                {
                    GetModelIdModelStatus(item, gfghhf);
                }
            }
        }
    }
}
