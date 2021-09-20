using KlickHealthApp.Models;
using KlickHealthApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace KlickHealthApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly KlickHealthDataContext _dbContext;
        public UsersController(ILogger<UsersController> logger, KlickHealthDataContext dataContext)
        {
            _logger = logger;
            _dbContext = dataContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to Klick Health User API (service is up and running)");
        }

        [HttpGet("{id:int}")]
        public IActionResult GetUserById(int id)
        {
            if (id == 0)
                return BadRequest();

            var existingUser = (from user in _dbContext.UserInformations
                                where user.Id == id
                                select new
                                {
                                    user.Id,
                                    user.Name,
                                    user.EmailAddress,
                                    user.ProvinceCode,
                                    user.ProvinceCodeNavigation.ProvinceName,
                                    user.IsActive,
                                    Medicines = (from c in _dbContext.MedicationDetails
                                                 where c.UserId == user.Id
                                                 select new
                                                 {
                                                     c.MedicineId,
                                                     c.Medicine.MedicineName
                                                 }).ToList()
                                }).FirstOrDefault();

            if (existingUser == null)
                return NoContent();
            else
                return Ok(existingUser);
        }

        [HttpGet("status/{isActive}")]
        public IActionResult GetUsersByStatus(string isActive)
        {
            bool _isActive = false;
            if (!string.IsNullOrEmpty(isActive))
            {
                _isActive = isActive.Trim().ToLower() == "true";
            }
            var existingUsers = (from user in _dbContext.UserInformations
                                 where user.IsActive == _isActive
                                 select new
                                 {
                                     user.Id,
                                     user.Name,
                                     user.EmailAddress,
                                     user.ProvinceCode,
                                     user.ProvinceCodeNavigation.ProvinceName,
                                     user.IsActive,
                                     Medicines = (from c in _dbContext.MedicationDetails
                                                  where c.UserId == user.Id
                                                  select new
                                                  {
                                                      c.MedicineId,
                                                      c.Medicine.MedicineName
                                                  }).ToList()
                                 }).ToList();

            if (existingUsers == null)
                return NoContent();
            else
                return Ok(existingUsers);
        }



        [HttpPost("create")]        
        public IActionResult CreateUser(UserDetails userDetails)
        {
            //Check user details is not null
            if (userDetails == null)
                return BadRequest();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    //Create User information data
                    UserInformation user = new()
                    {
                        Name = userDetails.Name,
                        EmailAddress = userDetails.EmailAddress,
                        ProvinceCode = userDetails.ProvinceCode,
                        CreatedOn = DateTime.Now,
                        CreatedBy = userDetails.CreatedBy
                    };

                    //Save into database and retrive newly created Id
                    _dbContext.UserInformations.Add(user);
                    _dbContext.SaveChanges();
                    int userId = user.Id;

                    //Create and save medication information
                    if (userDetails.Medicines.Count > 0)
                    {
                        foreach (int medicineId in userDetails.Medicines)
                        {
                            MedicationDetail medicationDetail = new MedicationDetail()
                            {
                                UserId = userId,
                                MedicineId = medicineId

                            };
                            _dbContext.MedicationDetails.Add(medicationDetail);
                        }
                        _dbContext.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError(ex.Message);
                    return BadRequest();
                }
            }
            return Ok("User created successful");
        }

       
        [HttpPost("update")]
        public IActionResult UpdateUser(UserDetails userDetails)
        {
            if (userDetails == null || userDetails.Id == 0)
                return BadRequest();

            UserInformation existingUser = _dbContext.UserInformations.Where(x => x.Id == userDetails.Id).SingleOrDefault();

            if (existingUser == null)
                return NoContent();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    existingUser.Name = userDetails.Name;
                    existingUser.EmailAddress = userDetails.EmailAddress;
                    existingUser.ProvinceCode = userDetails.ProvinceCode;
                    existingUser.ModifiedOn = DateTime.Now;
                    existingUser.ModifiedBy = userDetails.CreatedBy;
                    _dbContext.UserInformations.Update(existingUser);
                    _dbContext.SaveChanges();

                    var medicationList = _dbContext.MedicationDetails.Where(c => c.UserId == userDetails.Id).ToList();
                    if (medicationList != null && medicationList.Count > 0)
                    {
                        _dbContext.MedicationDetails.RemoveRange(medicationList);
                        _dbContext.SaveChanges();
                    }

                    if (userDetails.Medicines.Count > 0)
                    {
                        foreach (int medicineId in userDetails.Medicines)
                        {
                            MedicationDetail medicationDetail = new MedicationDetail()
                            {
                                UserId = userDetails.Id,
                                MedicineId = medicineId

                            };
                            _dbContext.MedicationDetails.Add(medicationDetail);
                        }
                        _dbContext.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError(ex.Message);
                    return BadRequest();
                }
            }
            return Ok("User updated successful");

        }

        [HttpPost("deactivate/{id:int}")]
        public IActionResult Deactivate(int id)
        {
            if (id == 0)
                return BadRequest();

            UserInformation existingUser = _dbContext.UserInformations.Where(x => x.Id == id).SingleOrDefault();

            if (existingUser == null)
                return NoContent();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    existingUser.IsActive = false;
                    _dbContext.UserInformations.Update(existingUser);
                    _dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError(ex.Message);
                    return BadRequest();
                }
            }
            return Ok("User deactivated successful");
        }

        
        [HttpPost("delete/{id:int}")]        
        public IActionResult DeleteUser(int id)
        {
            if (id == 0)
                return BadRequest();

            UserInformation existingUser = _dbContext.UserInformations.Where(x => x.Id == id).SingleOrDefault();

            if (existingUser == null)
                return NoContent();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var medicationList = _dbContext.MedicationDetails.Where(c => c.UserId == id).ToList();
                    if (medicationList != null && medicationList.Count > 0)
                    {
                        _dbContext.MedicationDetails.RemoveRange(medicationList);
                        _dbContext.SaveChanges();
                    }
                    _dbContext.UserInformations.Remove(existingUser);
                    _dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError(ex.Message);
                    return BadRequest();
                }
            }
            return Ok("User deleted successful");
        }

        [HttpGet("filter")]        
        public IActionResult FilterUsers([FromBody] SearchCriteria searchCriteria)
        {
            if (searchCriteria == null)
                return BadRequest();

            var existingUsers = (from user in _dbContext.UserInformations
                                 where user.IsActive == (searchCriteria.IsActive.HasValue ? searchCriteria.IsActive.Value : user.IsActive)
                                 && user.ProvinceCode == (searchCriteria.ProvinceCode != null ? searchCriteria.ProvinceCode : user.ProvinceCode)
                                 && (from c in _dbContext.MedicationDetails
                                     where c.UserId == user.Id
                                     && (searchCriteria.Medicines == null || searchCriteria.Medicines.Count <= 0 || searchCriteria.Medicines.Contains(c.MedicineId))
                                     select new
                                     {
                                         c.MedicineId
                                     }
                                 ).Any()
                                 select new
                                 {
                                     user.Id,
                                     user.Name,
                                     user.EmailAddress,
                                     user.ProvinceCode,
                                     user.ProvinceCodeNavigation.ProvinceName,
                                     user.IsActive,
                                     Medicines = (from c in _dbContext.MedicationDetails
                                                  where c.UserId == user.Id
                                                  select new
                                                  {
                                                      c.MedicineId,
                                                      c.Medicine.MedicineName
                                                  }).ToList()
                                 }).ToList();

            if (existingUsers == null)
                return NoContent();
            else
                return Ok(existingUsers);
        }

    }
}
