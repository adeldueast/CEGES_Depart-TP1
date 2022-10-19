﻿using CEGES_Models;

namespace CEGES_Services.Interfaces
{
    public interface IGroupeService
    {


        Task<Groupe> GetById(int id);

        Task<int> Add(int entrepriseId, string nom);

        Task<int> Update(int id, string nom);
   
    }
}
