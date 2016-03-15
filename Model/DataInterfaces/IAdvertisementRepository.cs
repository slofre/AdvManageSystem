using System;
using System.Collections.Generic;
using Model.PocoEntity;

namespace Model.DataInterfaces
{
    /// <summary>
    /// This interface describes the basic method of interaction whith the repository
    /// </summary>
    
    public interface IAdvertisementRepository
    {        
        List<Advertisement> GetAll();

        Advertisement Add (Advertisement adv);

        void Update (Advertisement adv);

        void Remove (int id);        
    }
}
