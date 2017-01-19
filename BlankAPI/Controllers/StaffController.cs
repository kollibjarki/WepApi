using BlankAPI.Models.DTO;
using BlankAPI.Models.EF;
using BlankAPI.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BlankAPI.Controllers
{
    [RoutePrefix("api/staff")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class StaffController : ApiController
    {
        readonly private StaffQueries sq;
        public StaffController()
        {
            sq = new StaffQueries();
        }
        [HttpGet]
        [Route("allstaff")]
        public IEnumerable<StaffDTO> GetAllStaff()
        {
            return sq.GetAllStaff();
        }
        [HttpGet]
        [Route("item/{id}")]
        public IQueryable<StaffDTO> GetStaffById(int id)
        {
            return sq.GetStaffById(id);
        }
        [HttpPost]
        [Route("edit")]
        public void Edit(Staff staff)
        {
            sq.EditStaff(staff);
        }
        [HttpPost]
        [Route("create")]
        public void Create(Staff staff)
        {
            sq.AddStaff(staff);

        }
        [HttpPost]
        [Route("remove/{id}")]
        public void RemoveProduct(int id)
        {
            sq.RemoveStaff(id);
        }

    }

}
