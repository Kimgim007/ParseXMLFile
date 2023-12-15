﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using DataProcessorService.MyDbContext;
using DataProcessorService.Entitys.EntityForDataBase;
namespace DataProcessorService.Repository
{
    public class ModuleCatIDModuleStateRepository
    {
        private SQLiteDataBase _sQLiteDataBase { get; set; }
        public ModuleCatIDModuleStateRepository() { }
        public ModuleCatIDModuleStateRepository(SQLiteDataBase sQLiteDataBase)
        {
            this._sQLiteDataBase = sQLiteDataBase;
        }

        public void Add(ModuleCategoreIDModeleState moduleCatIDModuleStateRepository)
        {
            _sQLiteDataBase.Add(moduleCatIDModuleStateRepository);
            _sQLiteDataBase.SaveChanges();
        }
        public bool Get(string moduleCategoryID)
        {
            var moduleCategoryIDTrueOrFolse = _sQLiteDataBase.ModuleCategoreIDModeleStates.First(q => q.ModuleCategoreID == moduleCategoryID);
            if (moduleCategoryIDTrueOrFolse != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void Update(ModuleCategoreIDModeleState moduleCategoreIDModeleState)
        {
            var modulCategoryIDModulState = _sQLiteDataBase.ModuleCategoreIDModeleStates.First(q => q.ModuleCategoreID == moduleCategoreIDModeleState.ModuleCategoreID);
            if (modulCategoryIDModulState != null)
            {
                modulCategoryIDModulState.ModuleCategoreID = modulCategoryIDModulState.ModuleCategoreID;
                modulCategoryIDModulState.ModeleState = moduleCategoreIDModeleState.ModeleState;
                _sQLiteDataBase.SaveChanges();
            }
        }
    }
}