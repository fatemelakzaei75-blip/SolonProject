using DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.Interfaces
{
    public interface ISalonServiceService
    {
        List<Tbl_SalonService> GetServices();

        Tbl_SalonService GetServiceByTC(string tc);

        List<Tbl_SalonService> GetServicesByCategoryTC(string categoryTC);

        List<Tbl_SalonService> SearchServices(string search);

        bool AddService(Tbl_SalonService service);

        bool EditService(Tbl_SalonService service);

        bool DeleteService(string tc);
    }
}
