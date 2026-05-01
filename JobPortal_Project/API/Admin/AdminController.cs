using AutoMapper;
using Domain.Models;
using Domain.Service.Admin.DTOs;
using Domain.Service.Admin.Interfaces;
using JobPortal_Project.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal_Project.API.Admin
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="ADMIN")]
    public class AdminController :BaseApiController<AdminController>
    {
        private readonly IAdminService _adminService;
        public readonly IMapper mapper;
        public AdminController(IAdminService adminService,IMapper mapper)
        {
            _adminService = adminService;
            this.mapper = mapper;
        }
        
        
        [HttpGet]
        [AllowAnonymous]
        [Route("admin/Getlocations")]
        public async Task<IActionResult> GetLocationDetails()
        {
            try
            {
                List<Location> locations= await _adminService.GetLocations();
                return Ok(mapper.Map<List<LocationDTO>>(locations));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("admin/GetIndustries")]
        public async Task<IActionResult> GetIndustryDetails()
        {
            try
            {
                List<Industry> industries= await _adminService.GetIndustries();
                return Ok(mapper.Map<List<IndustryDTOs>>(industries));


            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("admin/GetCategories")]
        public async Task<IActionResult> GetJobCategoryDetails()
        {
            try
            {
                List<JobCategory> categories = await _adminService.GetJobCategories();
                return Ok(mapper.Map<List<CategoryDTOs>>(categories));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("admin/GetSkills")]
        public async Task<IActionResult> GetSkillDetails()
        {
            try
            {
                List<Skill> skills = await _adminService.GetSkills();
                return Ok(mapper.Map<List<SkillDTOs>>(skills));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("admin/GetQualifications")]
        public async Task<IActionResult> GetQualificationDetails()
        {
            try
            {
                List<Qualification> qualifications = await _adminService.GetQualifications();
                return Ok(mapper.Map<List<QualificationDTOs>>(qualifications));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
