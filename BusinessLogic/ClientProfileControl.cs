﻿using System;
using Mehdime.Entity;
using System.Collections.Generic;
// Used modules and Interfacesfrom the project
using BusinessObjects;
using DataAccess.Interfaces;
using BusinessLogic.Interfaces;

namespace BusinessLogic
{
    public class ClientProfileControl : IClientProfileControl
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IClientRepository _clientRepository;

        public ClientProfileControl(IDbContextScopeFactory dbContextScopeFactory, IClientRepository clientRepository)
        {
            _dbContextScopeFactory = dbContextScopeFactory ?? throw new ArgumentNullException("ClientProileControl broke");
            _clientRepository = clientRepository ?? throw new ArgumentNullException("ClientProileControl broke");
        }

        public Client ConnectClient(Client client)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                // get clients by email, we dont have Ids here
                var foundClientObject = _clientRepository.GetClient(client.Email);

                if (foundClientObject != null && foundClientObject.DoEncyptedPassowrdsMacth(client.Password) && foundClientObject.IsBlocked)
                    return foundClientObject;
                else
                    return null;
            }
        }

        public Client GetClient(int id)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return _clientRepository.GetClient(id);
            }
        }

        public Client Client(string email)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return _clientRepository.GetClient(email);
            }
        }

        public List<Client> GetClientsList()
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return _clientRepository.GetAllClients();
            }
        }

        public void CreateClient(Client client)
        {
            if (client == null)
                throw new ArgumentNullException("When creating client, its object became null");

            if (!client.ConfirmClientPasswordsMatch())
                throw new Exception("The passwords do not match");

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var foundClientObject = _clientRepository.GetClient(client.Email);

                if (foundClientObject != null)
                    throw new Exception("The username is already registered, sorry");

                client.InitializeClientPasswordEncryption();
                client.IsBlocked = true;

                _clientRepository.CreateClient(client);
                dbContextScope.SaveChanges();
            }
        }

        public void EditProfile(Client client)
        {
            if (client == null)
                throw new ArgumentNullException("there was a problem when renewing a client");

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {

                var foundClientObject = _clientRepository.GetClient(client.Id);
                if (foundClientObject == null)
                {
                    throw new Exception("this specific client was not found");
                }

                foundClientObject.Email = client.Email;
                foundClientObject.FirstName = client.FirstName;
                foundClientObject.LastName = client.LastName;
                foundClientObject.Card = client.Card;
                foundClientObject.DeliveryAddress = client.DeliveryAddress;

                _clientRepository.EditClient(foundClientObject);
                dbContextScope.SaveChanges();
            }
        }

        public void EditPassword(int id, string newPassword)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {

                var foundClientObject = _clientRepository.GetClient(id);
                if (foundClientObject == null)
                {
                    throw new Exception("A client with this certain "+ id + " was not found");
                }

                foundClientObject.Password = newPassword;

                _clientRepository.EditClient(foundClientObject);
                dbContextScope.SaveChanges();
            }
        }

        public void EditStatus(Client client)
        {
            if (client == null)
                throw new ArgumentNullException("There is no such a profile, sorry");

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var foundClientObject= _clientRepository.GetClient(client.Id);
                foundClientObject.IsBlocked = !client.IsBlocked;
                _clientRepository.EditClient(foundClientObject);
                dbContextScope.SaveChanges();
            }
        }
    }
}