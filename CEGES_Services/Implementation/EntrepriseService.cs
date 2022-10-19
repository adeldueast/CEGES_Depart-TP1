﻿using CEGES_DataAccess.Repository.IRepository;
using CEGES_Models;
using CEGES_Models.Exceptions;
using CEGES_Services.Interfaces;
using CEGES_Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_Services.Implementation
{
    public class EntrepriseService : IEntrepriseService
    {
        private readonly IUnitOfWork _uow;

        public EntrepriseService(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<Entreprise>> GetAllSummaries() => await _uow.Entreprises.GetAllIncludeGroupesAndEquipements();

        public async Task<Entreprise> GetById(int id)
        {

            var entreprise = await _uow.Entreprises.GetByIdIncludeGroupesAndEquipements(id);

            if (entreprise == null)
            {
                throw new NotFoundException("Entreprise", id);
            }


            return entreprise;
        }

        public async Task<int> Add(string nom)
        {

            Entreprise entreprise = new()
            {
                Nom = nom
            };

            _uow.Entreprises.Add(entreprise);

            await _uow.SaveChangesAsync();

            return entreprise.Id;
        }

        public async Task<int> Update(int id, string nom)
        {

            var entreprise = await _uow.Entreprises.GetAsync(id);


            entreprise.Nom = nom;

            _uow.Entreprises.Update(entreprise);

            await _uow.SaveChangesAsync();

            return entreprise.Id;
        }


    }
}