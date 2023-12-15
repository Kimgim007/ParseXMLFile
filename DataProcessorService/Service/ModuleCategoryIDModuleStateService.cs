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
        public ModuleCategoryIDModuleStateService()
        {

        }
        public ModuleCategoryIDModuleStateService(ModuleCatIDModuleStateRepository moduleCatIDModuleStateRepository)
        {
            this._moduleCatIDModuleStateRepository = moduleCatIDModuleStateRepository;
        }
        public void AddModuleGategoryIDModuleState(string ModuleCategoryId, string ModuleState)
        {
            ModuleCategoreIDModeleState moduleCategoreIDModeleState = new ModuleCategoreIDModeleState();

            moduleCategoreIDModeleState.ModuleCategoreID = ModuleCategoryId;
            moduleCategoreIDModeleState.ModeleState = ModuleState;

            if (_moduleCatIDModuleStateRepository.Get(moduleCategoreIDModeleState.ModuleCategoreID))
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
            XMLFileModel xMLFileModel = JsonContent.DeserializeObject<XMLFileModel>(massage);

           
        }
    }
}
