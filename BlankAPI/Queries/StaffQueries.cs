using BlankAPI.Models.DTO;
using BlankAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlankAPI.Queries
{
    public class StaffQueries
    {
        private blankdbEntities _db;
        public StaffQueries()
        {
            _db = new blankdbEntities();
        }

        public IEnumerable<StaffDTO> GetAllStaff() //virkar
        {
            var staff = from s in _db.Staff
                          select new StaffDTO()
                          {
                              Id = s.Id,
                              Name = s.Name,
                              JobTitle = s.JobTitle,
                              ImageUrl = s.ImageUrl,

                          };
            return staff;
        }

        public IQueryable<StaffDTO> GetStaffById(int id) //virkar
        {

            var staff = from s in _db.Staff
                        where s.Id == id
                        select new StaffDTO()
                        {
                            Id = s.Id,
                            Name = s.Name,
                            JobTitle = s.JobTitle,
                            Age = s.Age,
                            PersonalQuote = s.PersonalQuote,
                            ImageUrl = s.ImageUrl,

                        };
            return staff;
            
        }

        public void RemoveStaff(int id) //virkar 
        {
            var staff = (from x in _db.Staff
                           where x.Id == id
                           select x).FirstOrDefault();
       
                
                _db.Staff.Remove(staff);
                _db.SaveChanges();
            
        }

        public Staff EditStaff(Staff staff) //virkar
        {
            var dbStaff = (from x in _db.Staff
                             where x.Id == staff.Id
                             select x).FirstOrDefault();
            dbStaff.Name = staff.Name;
            dbStaff.JobTitle= staff.JobTitle;
            dbStaff.Age = staff.Age;
            dbStaff.PersonalQuote = staff.PersonalQuote;
            dbStaff.ImageUrl = staff.ImageUrl;
            _db.SaveChanges();

            return staff;
        }

        public void AddStaff(Staff staff) //virkar 
        {
            if (staff == null)
            {
                return;
            }
            var staf = (from x in _db.Staff
                            where x.Name == staff.Name
                            select x).FirstOrDefault();
            if (staf != null)
            {
                staff = staf;
            }

            _db.Staff.Add(staff);
            _db.SaveChanges();
        }
    }
}